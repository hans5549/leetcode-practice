namespace LeetCodePractice.Core;

/// <summary>
/// 以反射掃描組件內所有 <see cref="IProblem"/> 實作並依題號排序，新增題目不需要手動註冊。
/// </summary>
internal static class ProblemRegistry
{
    public static IReadOnlyList<IProblem> LoadAll()
    {
        List<IProblem> problems = typeof(ProblemRegistry).Assembly
            .GetTypes()
            .Where(type => type is { IsClass: true, IsAbstract: false } && typeof(IProblem).IsAssignableFrom(type))
            .Select(type => (IProblem)Activator.CreateInstance(type)!)
            .OrderBy(problem => problem.Id)
            .ToList();

        IGrouping<int, IProblem>? duplicate = problems
            .GroupBy(problem => problem.Id)
            .FirstOrDefault(group => group.Count() > 1);

        if (duplicate is not null)
        {
            string names = string.Join("、", duplicate.Select(problem => problem.GetType().FullName));
            throw new InvalidOperationException($"題號 {duplicate.Key} 重複註冊：{names}");
        }

        return problems;
    }
}
