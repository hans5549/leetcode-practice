# PreToolUse hook: stops Claude from writing LeetCode solutions for the user.
#   1. Write / Edit targeting Problems/*/Solution.cs is allowed only when it creates a NEW file whose method bodies
#      are `throw new NotImplementedException();` (a fresh skeleton). Everything else is denied.
#   2. Bash / PowerShell commands that mention Solution.cs together with a write operation are denied
#      (plain string matching: it catches accidents, it is not a security boundary).
# Input: hook JSON on stdin. Output: a permissionDecision=deny JSON when blocking; nothing when allowing.

[Console]::InputEncoding = [Text.Encoding]::UTF8
[Console]::OutputEncoding = [Text.Encoding]::UTF8

function Deny([string]$reason) {
    @{
        hookSpecificOutput = @{
            hookEventName            = 'PreToolUse'
            permissionDecision       = 'deny'
            permissionDecisionReason = $reason
        }
    } | ConvertTo-Json -Compress
    exit 0
}

try {
    $data = [Console]::In.ReadToEnd() | ConvertFrom-Json
}
catch {
    exit 0
}

$toolInput = $data.tool_input

if ($data.tool_name -in 'Write', 'Edit') {
    $filePath = [string]$toolInput.file_path
    if ($filePath.Replace('\', '/') -notmatch '(?i)/Problems/[^/]+/Solution\.cs$') {
        exit 0
    }

    $isNewSkeleton = $data.tool_name -eq 'Write' `
        -and -not (Test-Path -LiteralPath $filePath) `
        -and ([string]$toolInput.content) -match 'throw\s+new\s+NotImplementedException\(\)\s*;'
    if ($isNewSkeleton) {
        exit 0
    }

    Deny 'Solution.cs is where the user writes their own solution: do not write or modify it. A new problem''s skeleton may only be created as a NEW file whose method bodies are `throw new NotImplementedException();`. Guide the user instead (see /hint) and do not write or paste solution code.'
}

if ($data.tool_name -in 'Bash', 'PowerShell') {
    $command = [string]$toolInput.command
    $writeVerbs = '>|\b(Set-Content|Add-Content|Out-File|tee|sed|awk|perl|python3?|WriteAll\w+|Copy-Item|cp|Move-Item|mv|Remove-Item|rm|New-Item|truncate)\b'
    if ($command -match 'Solution\.cs' -and $command -match $writeVerbs) {
        Deny 'This command appears to modify Solution.cs, which is where the user writes their own solution: do not write or modify it. Guide the user instead (see /hint). To read the file use the Read tool; to run the cases use dotnet run.'
    }
}

exit 0
