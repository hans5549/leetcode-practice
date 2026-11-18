using System.Text.Json;
using LeetCodePractice.Core;

namespace LeetCodePractice.Problems.P0004;

// 本機執行用的配接：把案例 JSON 的 input 轉成對 Solution 的呼叫。不需要複製到 LeetCode。
// input 的屬性名稱 = FindMedianSortedArrays 的參數名稱：nums1、nums2。
public sealed class Problem : IProblem
{
    public int Id => 4;

    public string Title => "Median of Two Sorted Arrays";

    private sealed record Input(int[] Nums1, int[] Nums2);

    public string Run(JsonElement input)
    {
        Input args = Json.Read<Input>(input);

        double result = new Solution().FindMedianSortedArrays(args.Nums1, args.Nums2);
        return Json.Write(result);
    }
}
