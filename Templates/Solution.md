# Solution.cs 範本

複製到 `Problems/xxxx-slug/Solution.cs`，把 `P0000` 換成 4 碼題號，並貼上 LeetCode 官方提供的方法簽章。

```csharp
namespace LeetCodePractice.Problems.P0000;

public class Solution {
    // TODO: 貼上官方提供的方法簽章
}
```

## 規則

- 這個檔案只放 LeetCode 官方提供的 `Solution` 骨架，內容與官方一模一樣。
- 第 1 行 `namespace` 只給本機用；它以下的**全部內容（含 `using`）**整段複製到 LeetCode。
- 需要的 `using` 寫在 `namespace` 那一行的下面、`public class Solution` 的上面。
- 官方的空方法本體無法編譯，會讓整個專案無法建置，所以方法本體預設放一行
  `throw new NotImplementedException();` 佔位，編輯時直接取代即可。
