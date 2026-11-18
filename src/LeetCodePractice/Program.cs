using System.Text;
using LeetCodePractice.Core;

namespace LeetCodePractice;

internal static class Program
{
    private static int Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;

        IReadOnlyList<IProblem> problems;
        try
        {
            problems = ProblemRegistry.LoadAll();
        }
        catch (InvalidOperationException ex)
        {
            Console.Error.WriteLine(ex.Message);
            return 1;
        }

        if (args.Length == 0)
        {
            return RunInteractive(problems);
        }

        if (args[0].Equals("list", StringComparison.OrdinalIgnoreCase))
        {
            PrintList(problems);
            return 0;
        }

        return RunById(problems, args[0]);
    }

    private static int RunInteractive(IReadOnlyList<IProblem> problems)
    {
        PrintList(problems);
        Console.Write("請輸入題號：");
        string? line = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(line))
        {
            Console.Error.WriteLine("未輸入題號。");
            return 1;
        }

        return RunById(problems, line);
    }

    private static int RunById(IReadOnlyList<IProblem> problems, string idText)
    {
        if (!int.TryParse(idText.Trim(), out int id))
        {
            Console.Error.WriteLine($"無法辨識的參數：{idText}（請輸入題號，或使用 list）");
            return 1;
        }

        IProblem? problem = problems.FirstOrDefault(p => p.Id == id);
        if (problem is null)
        {
            Console.Error.WriteLine($"找不到題號 {id} 對應的題目。");
            return 1;
        }

        return ProblemRunner.Run(problem);
    }

    private static void PrintList(IReadOnlyList<IProblem> problems)
    {
        Console.WriteLine("已註冊的題目：");
        foreach (IProblem problem in problems)
        {
            Console.WriteLine($"  {problem.Id:D4}  {problem.Title}");
        }
    }
}
