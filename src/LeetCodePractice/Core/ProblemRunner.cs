using System.Diagnostics;
using System.Reflection;
using System.Text.Json;

namespace LeetCodePractice.Core;

/// <summary>
/// 讀取題目對應的案例 JSON，逐一執行每個案例並輸出結果與耗時。
/// </summary>
internal static class ProblemRunner
{
    private const string InputsFolderName = "Inputs";

    // 案例檔是手動編輯的，允許 // 註解與結尾逗號。
    private static readonly JsonDocumentOptions DocumentOptions = new()
    {
        CommentHandling = JsonCommentHandling.Skip,
        AllowTrailingCommas = true,
    };

    /// <returns>0 表示全部案例執行成功；非 0 表示找不到或讀不了案例檔，或有案例發生例外。</returns>
    public static int Run(IProblem problem)
    {
        Console.WriteLine($"=== [{problem.Id:D4}] {problem.Title} ===");

        string? casesFile = FindCasesFile(problem.Id);
        if (casesFile is null)
        {
            Console.Error.WriteLine($"找不到案例檔：{InputsFolderName}/{problem.Id:D4}-*.json");
            return 1;
        }

        JsonDocument document;
        try
        {
            document = JsonDocument.Parse(File.ReadAllText(casesFile), DocumentOptions);
        }
        catch (JsonException ex)
        {
            Console.Error.WriteLine($"案例檔不是合法的 JSON：{Path.GetFileName(casesFile)}（{ex.Message}）");
            return 1;
        }

        using (document)
        {
            JsonElement root = document.RootElement;
            if (root.ValueKind != JsonValueKind.Array || root.GetArrayLength() == 0)
            {
                Console.Error.WriteLine(
                    $"案例檔最外層必須是非空陣列：[ {{ \"name\": \"...\", \"input\": {{ ... }} }} ]（{Path.GetFileName(casesFile)}）");
                return 1;
            }

            int index = 0;
            int failedCount = 0;
            foreach (JsonElement caseElement in root.EnumerateArray())
            {
                index++;
                if (!RunCase(problem, caseElement, index))
                {
                    failedCount++;
                }
            }

            Console.WriteLine();
            Console.WriteLine($"共 {index} 個案例，失敗 {failedCount} 個。");
            return failedCount == 0 ? 0 : 1;
        }
    }

    private static bool RunCase(IProblem problem, JsonElement caseElement, int index)
    {
        Console.WriteLine();
        Console.WriteLine($"--- {GetCaseName(caseElement, index)} ---");

        if (caseElement.ValueKind != JsonValueKind.Object || !caseElement.TryGetProperty("input", out JsonElement input))
        {
            Console.Error.WriteLine("案例缺少 input 物件。");
            return false;
        }

        Console.WriteLine($"Input: {Json.Write(input)}");

        long startTimestamp = Stopwatch.GetTimestamp();
        try
        {
            string output = problem.Run(input);
            TimeSpan elapsed = Stopwatch.GetElapsedTime(startTimestamp);
            Console.WriteLine($"Output: {output}");
            Console.WriteLine($"Time: {elapsed.TotalMilliseconds:F3} ms");
            return true;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"案例執行失敗：{ex.GetType().Name}: {ex.Message}");
            return false;
        }
    }

    private static string GetCaseName(JsonElement caseElement, int index)
    {
        if (caseElement.ValueKind == JsonValueKind.Object
            && caseElement.TryGetProperty("name", out JsonElement name)
            && name.ValueKind == JsonValueKind.String)
        {
            return name.GetString()!;
        }

        return $"case{index}";
    }

    private static string? FindCasesFile(int problemId)
    {
        string? projectDirectory = typeof(ProblemRunner).Assembly
            .GetCustomAttributes<AssemblyMetadataAttribute>()
            .FirstOrDefault(attribute => attribute.Key == "ProjectDir")?.Value;
        if (projectDirectory is null)
        {
            return null;
        }

        string inputsRoot = Path.Combine(projectDirectory, InputsFolderName);
        if (!Directory.Exists(inputsRoot))
        {
            return null;
        }

        return Directory
            .GetFiles(inputsRoot, $"{problemId:D4}-*.json")
            .OrderBy(path => path, StringComparer.OrdinalIgnoreCase)
            .FirstOrDefault();
    }
}
