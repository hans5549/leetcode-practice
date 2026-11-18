namespace LeetCodePractice.Problems.P0003;

public class Solution {
    public int LengthOfLongestSubstring(string s) {
        int maxLength = 0;
        for(int i = 0; i < s.Length; i++)
        {
            HashSet<char> temp = [];
            for(int j = i; j < s.Length; j++)
            {
                if (temp.Contains(s[j]))
                {
                    break;
                }

                temp.Add(s[j]);
                maxLength = Math.Max(maxLength, j - i + 1);
            }
        }

        return maxLength;
    }
}
