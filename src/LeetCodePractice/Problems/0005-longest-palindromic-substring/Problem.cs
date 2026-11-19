using System.Text.Json;
using LeetCodePractice.Core;

namespace LeetCodePractice.Problems.P0005;

// 本機執行用的配接：把案例 JSON 的 input 轉成對 Solution 的呼叫。不需要複製到 LeetCode。
// input 的屬性名稱 = LongestPalindrome 的參數名稱：s。
public sealed class Problem : IProblem
{
    public int Id => 5;

    public string Title => "Longest Palindromic Substring";

    private sealed record Input(string S);

    public string Run(JsonElement input)
    {
        Input args = Json.Read<Input>(input);

        string result = new Solution().LongestPalindrome(args.S);
        return Json.Write(result);
    }
}
