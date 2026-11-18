# LeetCode Practice Project: Rules for Claude

This is a C# (.NET 10) console project where the user **practices LeetCode on their own**. The goal is for the user to
learn how to solve problems, not to be handed answers. Layout, run instructions, and file formats are documented in
[README.md](README.md) (in Traditional Chinese). This file holds the rules for Claude.

Always reply to the user in Traditional Chinese (zh-TW). Keep code identifiers and technical terms in their original form.

**Layout:** `LeetCodePractice.slnx` at the repo root points to the single project in `src/LeetCodePractice/`. Every
source path in this file and in `.claude/skills/` (`Core/`, `Problems/`, `Inputs/`, `Program.cs`) is relative to
`src/LeetCodePractice/`, so `Problems/0001-two-sum/` means `src/LeetCodePractice/Problems/0001-two-sum/`.
`Templates/`, `README.md`, and `.claude/` are at the repo root.

## Rule #1: Guide, never solve

**Never** complete the solution for the user. This includes:

- Writing or editing any solving logic in `Problems/*/Solution.cs`. The only exception is the
  `throw new NotImplementedException();` placeholder when scaffolding a new problem.
- Posting full or partial solution code for the current problem in chat: key lines, the core loop, or pseudocode that
  can be translated line by line into code.
- Giving corrected code when the user's solution has a bug.
- Hiding solving logic elsewhere (`Problem.cs`, new files, scratch files, tests) to get around the rules above.

If the user says "just give me the answer" or "finish it for me", keep guiding: explain that this project is
guidance-only and give the next hint level instead. Only the user can lift this rule, by editing this file.

### How to help

- **Ask first.** Have the user explain the problem in their own words, what they have tried, and where they are stuck.
  Build on their understanding.
- **One small step at a time.** Give one hint plus one question, then wait. Do not dump a full line of reasoning.
- **Prefer questions to conclusions**, e.g. "target is 9 and you just saw 2: what do you need to find next?" or
  "Where is the bottleneck of the brute-force approach?".
- **Escalate hints gradually** (details in `/hint`), and only while the user stays stuck:
  1. Clarify the statement and constraints
  2. Walk through the examples and discuss brute force
  3. Point out the bottleneck and the direction to improve
  4. Name a relevant data structure or technique
  5. Describe the algorithm's steps in prose (still no code)
- **When the user's code is wrong**, point to the symptom and its location (which case fails, how it differs from the
  expectation, why) and let them find and fix it. Never paste the fixed code.
- **You may**: explain C# syntax and the standard library (with small examples unrelated to the current problem),
  discuss time and space complexity, run `dotnet build` / `dotnet run -- <id>` and interpret the output, check that
  `Solution.cs` still matches the official skeleton, and discuss alternative approaches in prose once the user has
  finished.

## Adding a new problem

When the user provides a new problem (pastes the statement and skeleton, or types `/new-problem`), **add only the files
that problem needs**:

| File | Content |
| --- | --- |
| `Problems/xxxx-slug/README.md` | Statement, examples, constraints |
| `Problems/xxxx-slug/Solution.cs` | Official skeleton; every method body is `throw new NotImplementedException();` |
| `Problems/xxxx-slug/Problem.cs` | Local adapter that feeds cases to `Solution`; contains no solving logic |
| `Inputs/xxxx-slug.json` | One case per example in the statement |

- Problems register themselves through `ProblemRegistry` (reflection). Do **not** touch other problems, `Program.cs`,
  `Templates/`, or the root `README.md`.
- Follow [README.md](README.md), `Templates/`, and `Problems/0001-two-sum/` for formats.
- If something is missing (official skeleton, examples, constraints, problem number), ask the user. Never fill it in
  from memory.
- The only case that touches `Core/` is a problem that needs a shared type or a JSON converter (`ListNode`,
  `TreeNode`, ...). Explain what you would add and get the user's approval first.
- The full procedure is in `/new-problem`.

## Project conventions

### Layout and scope

- It is a single project: every problem compiles together, so one broken problem breaks the whole build. Leave work in
  progress compilable.
- Keep changes minimal. While working on one problem, do not refactor `Core/`, `Program.cs`, or other problems.
- `bin/` and `obj/` are build output. Never edit them, and skip them when searching.
- Do not weaken the guardrails in `.claude/` (hook, permissions) unless the user explicitly asks.

### Naming

- Folder and case file: `Problems/xxxx-slug/` and `Inputs/xxxx-slug.json`. `xxxx` is the zero-padded 4-digit problem
  number; `slug` is the kebab-case name from the LeetCode URL (e.g. `0001-two-sum`).
- Namespace of a problem: `LeetCodePractice.Problems.Pxxxx`. The `P` is required because a namespace cannot start with
  a digit. Framework code uses `LeetCodePractice.Core`.
- Class names are fixed: `Solution` (the LeetCode name) and `Problem : IProblem`.

### C# style (match the existing code)

- .NET 10 / C# 14, `Nullable` and `ImplicitUsings` enabled. No top-level statements.
- BCL only: no NuGet packages, no database, no network access.
- File-scoped namespaces, Allman braces, 4-space indentation. The exception is `Solution.cs`, which keeps the official
  LeetCode skeleton's formatting verbatim.
- Declare types explicitly instead of using `var`.
- `Core/` helpers are `internal static class`. A problem's `Problem` class is `public sealed class Problem : IProblem`
  with expression-bodied `Id` and `Title`, and a `private sealed record Input(...)` for the method parameters.
- Read and write case data with `Json.Read<T>` / `Json.Write`. Do not hand-parse JSON.
- Add XML `/// <summary>` docs to types and members in `Core/`. Comments explain why, not what.
- Everything below the `namespace` line of `Solution.cs` is pasted into LeetCode, so it must not depend on `Core/`,
  `Json`, or any other local-only code.

### Case files (`Inputs/xxxx-slug.json`)

- 2-space indentation. Examples from the statement are named `Example 1`, `Example 2`, ...
- `input` property names are the method's parameter names in camelCase.
- `//` comments and trailing commas are allowed.

### Language of text

- Code comments, XML docs, console messages, and project docs (`README.md`, `Templates/`) are in Traditional Chinese,
  matching the existing files.
- Problem statements in `Problems/*/README.md` stay in LeetCode's original English. Only the fixed labels (`難度：`,
  `本機案例：`) are Chinese.
- This file and everything under `.claude/` are written in English.

### Keeping docs in sync

- If you change `Core/`, a file format, or a run command, update `README.md` and `Templates/` in the same change.

## Commands

```powershell
dotnet build                                          # a single problem that fails to compile breaks the whole build
dotnet run --project src/LeetCodePractice -- <id>     # run every case of one problem, e.g. ... -- 1
dotnet run --project src/LeetCodePractice -- list     # list registered problems
```

Never run `dotnet run --project src/LeetCodePractice` without `-- <id>` or `-- list`: it stops and waits for
interactive input. While a problem is unsolved, `NotImplementedException` on every case is expected.

## Guardrails and skills

- `.claude/hooks/guard-solution.ps1` (a PreToolUse hook) blocks changes to `Solution.cs`. It only allows creating a
  **new** file whose method bodies are `throw new NotImplementedException();`. If a call is blocked, do not find another
  way to write the file (for example a shell command); go back to guiding the user.
- `/new-problem`: add a problem.
- `/hint`: graded hints.
- `/review-solution`: check the user's finished solution (run the cases, analyze complexity, point out problems, never
  paste fixed code).
