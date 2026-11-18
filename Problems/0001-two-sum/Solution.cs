namespace LeetCodePractice.Problems.P0001;

public class Solution {
    public int[] TwoSum(int[] nums, int target) {
        Dictionary<int, int> numDic = [];
        for (int i = 0; i < nums.Length; i++)
        {
            int completeNum = target - nums[i];
            if (numDic.ContainsKey(completeNum))
            {
                return [numDic[completeNum], i];
            }

            numDic[nums[i]] = i;
        }

        return [];
    }
}
