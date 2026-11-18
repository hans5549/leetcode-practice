# LeetCode 練習專案

純 console 的 C# (.NET 10) 專案，用來練習 LeetCode。不使用 top-level statements，也沒有資料庫連線。

## 資料夾說明

| 路徑 | 用途 |
| --- | --- |
| `Problems/xxxx-slug/` | **各題題目**。一題一個資料夾，內含三個檔案（見下表） |
| `Inputs/xxxx-slug.json` | **各題輸入內容**。一題一個 JSON，整理該題所有案例（格式見下方） |
| `Core/` | 框架本身：題目介面、自動註冊、執行器、JSON 讀寫工具，以及共用型別（`ListNode`） |
| `Templates/` | 手動新增題目用的範本（Markdown，程式碼放在區塊內，不會被編譯） |
| `Program.cs` | 進入點 |

`xxxx` 是 4 碼題號、`slug` 是 LeetCode 網址上的題目名稱（例如 `0001-two-sum`）。
`Problems/` 與 `Inputs/` 使用**同一個名稱**，一眼就能對應。

每題資料夾的三個檔案：

| 檔案 | 內容 |
| --- | --- |
| `Solution.cs` | LeetCode 官方提供的 `Solution` 骨架，**編輯完後把 `namespace` 以下的內容複製到 LeetCode** |
| `Problem.cs` | 本機執行用的配接（讀案例 JSON、呼叫 `Solution`、輸出結果），不用複製 |
| `README.md` | 題目敘述（Markdown），包含範例與限制，並標明每個 Example 對應的案例名稱 |

## 輸入案例格式（`Inputs/xxxx-slug.json`）

最外層是陣列，每個元素是一個案例：

```json
[
  {
    "name": "Example 1",
    "input": { "nums": [2, 7, 11, 15], "target": 9 }
  }
]
```

- `name`：案例名稱，可省略（預設 `case1`、`case2`…）。題目的 Example 建議命名為 `Example 1`、`Example 2`…。
- `input`：物件，**屬性名稱 = LeetCode 方法的參數名稱**（camelCase）。`Problem.cs` 內用 `Json.Read<Input>(input)`
  轉成型別化的 `Input` 物件，所以陣列、二維陣列、字串、數字都不需要自己解析。
- 允許 `//` 註解與結尾逗號，方便手動加註。
- 新增測資只要加一個元素，不必改程式。

## 執行方式

```powershell
dotnet run                 # 列出題目並詢問題號
dotnet run -- 1            # 執行第 1 題的所有案例
dotnet run -- list         # 只列出已註冊的題目
```

## 日常流程

1. 提供題目敘述與範例數據 → 建立該題的 `README.md`（Markdown）與 `Inputs/xxxx-slug.json`。
2. 提供官方 `Solution` 骨架 → 建立 `Solution.cs`（與官方一模一樣）與 `Problem.cs`。
3. 編輯 `Solution.cs`，把 `namespace` 那一行**以下的全部內容**複製到 LeetCode。
4. 執行 `dotnet run -- <題號>` 在本機檢查。

題目會由 `ProblemRegistry` 反射自動註冊，不需要手動登記。

## 注意事項

- **`Solution.cs` 與官方骨架唯一的差異**：官方的空方法本體無法編譯，所以預設放一行
  `throw new NotImplementedException();` 佔位，編輯時直接取代即可。
- 所有題目在同一個專案，**任何一題無法編譯，整個專案就無法建置**。寫到一半的題目請保持可編譯。
- 每題有自己的 namespace（`LeetCodePractice.Problems.Pxxxx`，寫在 `Solution.cs` 與 `Problem.cs` 的第一行；
  C# 的 namespace 不能以數字開頭，所以加 `P`），所以各題都可以直接使用 LeetCode 預設的 `Solution` 類別名稱。
  複製到 LeetCode 時不要帶上 namespace 那一行。
- 需要額外的 `using`（例如 `using System.Text;`）時，請寫在 `namespace` 那一行的**下面**、`Solution` 的上面，
  這樣「namespace 以下全部複製」的規則就一定包含到。
- 案例檔直接從 `Inputs/` 讀取（不會複製到 `bin/`），改測資後不需要重新建置。
- `ListNode` 已內建（`Core/ListNode.cs`，位於 `LeetCodePractice` namespace，`Solution.cs` 不需要 `using`）。
  案例 JSON 把它寫成整數陣列（例如 `"l1": [2, 4, 3]`），輸出時也會轉回陣列；空串列輸出為 `[]`。
- 遇到 `TreeNode` 等其他無法直接由 JSON 轉換的型別時，在 `Core/` 新增型別，並在 `Core/Json.cs` 新增 converter。
- 本機用 .NET 10 / C# 14，與 LeetCode 目前的 C# 環境一致（2026-11 查詢官方說明中心的結果；LeetCode 日後升級時需重新確認）。
