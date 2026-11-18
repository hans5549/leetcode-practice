namespace LeetCodePractice;

/// <summary>
/// LeetCode 的單向鏈結串列節點，欄位與建構子和題目註解裡的官方定義一致。
/// 放在 LeetCodePractice namespace，讓各題的 Solution.cs 不需要 using 就能直接使用。
/// 因為會出現在 public 的 Solution 方法簽章裡，所以必須是 public。
/// </summary>
public class ListNode
{
    /// <summary>節點的值。</summary>
    public int val;

    /// <summary>下一個節點；null 代表串列結尾。</summary>
    public ListNode? next;

    /// <summary>建立節點，參數預設值與官方定義相同。</summary>
    public ListNode(int val = 0, ListNode? next = null)
    {
        this.val = val;
        this.next = next;
    }
}
