# Problem.cs 範本

複製到 `Problems/xxxx-slug/Problem.cs`，修改 namespace（例如 `P0000` → `P0015`）、`Id`、`Title`，
以及 `Input` 與 `Run` 的內容。

```csharp
using System.Text.Json;
using LeetCodePractice.Core;

namespace LeetCodePractice.Problems.P0000;

// 本機執行用的配接：把案例 JSON 的 input 轉成對 Solution 的呼叫。不需要複製到 LeetCode。
// input 的屬性名稱 = LeetCode 方法的參數名稱（JSON 用 camelCase），例如 nums、target。
public sealed class Problem : IProblem
{
    public int Id => 0;

    public string Title => "Name";

    // TODO: 依 LeetCode 方法的參數定義屬性，名稱用 PascalCase（對應 JSON 的 camelCase）
    private sealed record Input(int[] Nums, int Target);

    public string Run(JsonElement input)
    {
        Input args = Json.Read<Input>(input);

        // TODO: 呼叫 new Solution().XXX(...)，用 Json.Write(...) 把結果轉成字串回傳
        throw new NotImplementedException();
    }
}
```

## 規則

- 這是本機執行用的配接，**不需要複製到 LeetCode**。
- `Input` 的屬性對應 LeetCode 方法的參數，名稱用 PascalCase，對應案例 JSON 的 camelCase（`Nums` ↔ `nums`）。
- 對應的案例檔是 `Inputs/xxxx-slug.json`，格式見根目錄的 `README.md`。
