using System.Diagnostics.SymbolStore;

namespace LeetCodePractice.Problems.P0002;

/**
 * Definition for singly-linked list.
 * public class ListNode {
 *     public int val;
 *     public ListNode next;
 *     public ListNode(int val=0, ListNode next=null) {
 *         this.val = val;
 *         this.next = next;
 *     }
 * }
 */
public class Solution {
    public ListNode AddTwoNumbers(ListNode l1, ListNode l2) {
       ListNode newNode = new();
       ListNode result = newNode;
       int sum = 0;

       while(l1 != null || l2 != null || sum > 0)
        {
            if (l1 != null)
            {
                sum = sum + l1.val;
                l1 = l1.next;
            }

            if (l2 != null)
            {
                sum = sum + l2.val;
                l2 = l2.next;
            }
            
            newNode.next = new ListNode(sum % 10);
            sum = sum / 10;
            newNode = newNode.next;
        }

        return result.next;
    }
}
