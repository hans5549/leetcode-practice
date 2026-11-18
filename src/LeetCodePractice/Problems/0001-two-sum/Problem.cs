using System.Text.Json;
using LeetCodePractice.Core;

namespace LeetCodePractice.Problems.P0001;

// 本機執行用的配接：把案例 JSON 的 input 轉成對 Solution 的呼叫。不需要複製到 LeetCode。
// input 的屬性名稱 = TwoSum 的參數名稱：nums、target。
public sealed class Problem : IProblem
{
    public int Id => 1;

    public string Title => "Two Sum";

    private sealed record Input(int[] Nums, int Target);

    public string Run(JsonElement input)
    {
        Input args = Json.Read<Input>(input);

        int[] result = new Solution().TwoSum(args.Nums, args.Target);
        return Json.Write(result);
    }
}
