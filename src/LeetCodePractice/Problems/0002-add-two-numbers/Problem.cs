using System.Text.Json;
using LeetCodePractice.Core;

namespace LeetCodePractice.Problems.P0002;

// 本機執行用的配接：把案例 JSON 的 input 轉成對 Solution 的呼叫。不需要複製到 LeetCode。
// input 的屬性名稱 = AddTwoNumbers 的參數名稱：l1、l2（JSON 寫成整數陣列，由 Json 轉成 ListNode）。
public sealed class Problem : IProblem
{
    public int Id => 2;

    public string Title => "Add Two Numbers";

    private sealed record Input(ListNode L1, ListNode L2);

    public string Run(JsonElement input)
    {
        Input args = Json.Read<Input>(input);

        ListNode result = new Solution().AddTwoNumbers(args.L1, args.L2);
        return Json.Write(result);
    }
}
