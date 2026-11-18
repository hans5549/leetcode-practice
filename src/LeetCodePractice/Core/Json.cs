using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LeetCodePractice.Core;

/// <summary>
/// 案例 JSON 的讀寫工具。
/// 讀：JSON 的 camelCase 屬性（與 LeetCode 參數同名）對應 C# 的 PascalCase 屬性，大小寫不敏感。
/// 寫：輸出緊湊的 JSON，且不跳脫 + &lt; &gt; &amp; 與中文等字元，字串結果才好讀。
/// ListNode 已內建 converter（整數陣列 &lt;-&gt; 串列）；遇到 TreeNode 等其他需要特殊轉換的型別時，在這裡新增 converter。
/// </summary>
public static class Json
{
    // 案例檔是手動編輯的，允許 // 註解與結尾逗號（JsonElement 轉型時會重新解析原始文字，所以這裡也要設定）。
    private static readonly JsonSerializerOptions Options = new(JsonSerializerDefaults.Web)
    {
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
        ReadCommentHandling = JsonCommentHandling.Skip,
        AllowTrailingCommas = true,
        Converters = { new ListNodeConverter() },
    };

    /// <summary>把案例的 input 物件轉成指定型別。</summary>
    public static T Read<T>(JsonElement element)
    {
        return element.Deserialize<T>(Options)
            ?? throw new JsonException($"無法把 input 轉成 {typeof(T).Name}（結果為 null）。");
    }

    /// <summary>把解答結果轉成緊湊的 JSON 字串，例如 [0,1]。</summary>
    public static string Write<T>(T value)
    {
        return JsonSerializer.Serialize(value, Options);
    }

    /// <summary>
    /// ListNode 與整數陣列互轉：[2,4,3] &lt;-&gt; 2 -> 4 -> 3。
    /// 空陣列與 null 都代表空串列；寫出時空串列統一輸出 []，和 LeetCode 的顯示一致。
    /// </summary>
    private sealed class ListNodeConverter : JsonConverter<ListNode>
    {
        // 預設遇到 null 不會呼叫 converter（會直接輸出 null）；開啟後空串列才能輸出成 []。
        public override bool HandleNull => true;

        public override ListNode? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Null)
            {
                return null;
            }

            if (reader.TokenType != JsonTokenType.StartArray)
            {
                throw new JsonException("ListNode 必須寫成整數陣列，例如 [2,4,3]。");
            }

            List<int> values = [];
            while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
            {
                values.Add(reader.GetInt32());
            }

            // 從尾端往前接，才不需要額外記住尾節點。
            ListNode? head = null;
            for (int i = values.Count - 1; i >= 0; i--)
            {
                head = new ListNode(values[i], head);
            }

            return head;
        }

        public override void Write(Utf8JsonWriter writer, ListNode? value, JsonSerializerOptions options)
        {
            writer.WriteStartArray();
            for (ListNode? node = value; node != null; node = node.next)
            {
                writer.WriteNumberValue(node.val);
            }

            writer.WriteEndArray();
        }
    }
}
