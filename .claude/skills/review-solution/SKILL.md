---
name: review-solution
description: Use when the user has finished (or half finished) Solution.cs and wants it checked. Runs the local cases, judges correctness, analyzes complexity, and points out problems without rewriting the whole solution.
argument-hint: "[problem number, optional]"
---

# Review the user's solution

Problem: $ARGUMENTS (if omitted, use the problem currently being discussed, or the one open in the user's IDE).
**Never rewrite the whole solution.** Point out problems and let the user fix them first; if they stay stuck you may
show the few lines that fix a bug.

## Steps

1. Read `Problems/xxxx-slug/README.md`, `Problems/xxxx-slug/Solution.cs`, and `Inputs/xxxx-slug.json` (all under
   `src/LeetCodePractice/`).
2. Run `dotnet build`.
   - If it fails to compile, explain in words which line, what the compiler complains about, and the C# concept behind
     it, so the user can fix it themselves.
3. Run `dotnet run --project src/LeetCodePractice -- <id>`. The runner only prints results and **does not compare them with expected answers**, so
   compare each case's Output with the expected Output in the README yourself. When the statement says "any order is
   fine", a different order is not an error.
4. Branch on the result:
   - **A case differs or throws**: say which case, how the actual output differs from the expected one, and the likely
     conceptual cause. Then ask questions so the user debugs it themselves (e.g. "when `nums` is `[3,3]`, what does your
     loop see on its second iteration?"). Do not hand over a rewritten solution.
   - **Everything matches**: continue to step 5.
5. After it passes:
   - Ask the user for their time and space complexity first, then confirm or correct it.
   - Edge cases: ask which situations they think are untested (check the extremes in Constraints), then add your own.
     If the user agrees, you may add the inputs to `Inputs/xxxx-slug.json` (inputs only, no answers).
   - Describe readability and naming suggestions in words, or with a short before/after snippet, without rewriting the
     whole solution.
   - If the user wants to see other approaches, explain the idea and the trade-offs in prose.
6. Remind them how to submit: copy **everything below the `namespace` line** of `Solution.cs` into LeetCode.
