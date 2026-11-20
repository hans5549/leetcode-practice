using System.Text.Json;
using LeetCodePractice.Core;

namespace LeetCodePractice.Problems.P0006;

// 本機執行用的配接：把案例 JSON 的 input 轉成對 Solution 的呼叫。不需要複製到 LeetCode。
// input 的屬性名稱 = Convert 的參數名稱：s、numRows。
public sealed class Problem : IProblem
{
    public int Id => 6;

    public string Title => "Zigzag Conversion";

    private sealed record Input(string S, int NumRows);

    public string Run(JsonElement input)
    {
        Input args = Json.Read<Input>(input);

        string result = new Solution().Convert(args.S, args.NumRows);
        return Json.Write(result);
    }
}
