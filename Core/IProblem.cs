using System.Text.Json;

namespace LeetCodePractice.Core;

/// <summary>
/// 所有 LeetCode 題目的共同介面。
/// 每題一個 Problem 類別，放在 Problems/ 資料夾，會被 <see cref="ProblemRegistry"/> 自動註冊。
/// </summary>
public interface IProblem
{
    /// <summary>LeetCode 題號，例如 1。對應案例檔 Inputs/{Id:D4}-*.json。</summary>
    int Id { get; }

    /// <summary>題目名稱，例如 "Two Sum"。</summary>
    string Title { get; }

    /// <summary>
    /// 執行單一案例。
    /// </summary>
    /// <param name="input">案例 JSON 的 input 物件，屬性名稱對應 LeetCode 方法的參數名稱。</param>
    /// <returns>要顯示在 console 的結果字串。</returns>
    string Run(JsonElement input);
}
