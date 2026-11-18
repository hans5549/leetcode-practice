---
name: new-problem
description: Add a new LeetCode problem to the practice project. Use when the user provides a new problem (statement, examples, official C# skeleton, or a problem number or link) or asks to add one. Creates only the files that problem needs and never writes the solution.
argument-hint: "[problem number or title]"
---

# Add a new problem

What the user provided: $ARGUMENTS, plus any problem text pasted in the conversation.

Add only this problem's files. Do not touch other problems, `Program.cs`, `Templates/`, or the root `README.md`.
Problems register themselves through `ProblemRegistry`. Follow the conventions in `CLAUDE.md`.

## 1. Make sure you have everything

You need: problem number, title, difficulty, statement, examples (Input / Output / Explanation), constraints, and the
**official C# skeleton**.

- Ask for whatever is missing, all in one message. Never fill in statement text or the skeleton from memory.
- `slug` is the name in the LeetCode URL (kebab-case, e.g. `two-sum`). If the user gave no URL, derive it from the title
  and ask them to confirm it at the end.
- The folder name is `xxxx-slug`, where `xxxx` is the problem number zero-padded to 4 digits.
- Check that neither `Problems/xxxx-*` nor `Inputs/xxxx-*` exists. If one does, stop and ask the user. Never overwrite.

## 2. Create the files

Formats follow `Problems/0001-two-sum/`, `Templates/Solution.md`, and `Templates/Problem.md`. Keep the Traditional
Chinese comments and labels from those files as they are.

### `Problems/xxxx-slug/Solution.cs`

- Line 1 is `namespace LeetCodePractice.Problems.Pxxxx;`, followed by a blank line.
- Extra `using` directives go below the `namespace` line and above the `class`.
- Paste the official skeleton **verbatim** (keep its indentation and brace style). The only difference: replace every
  method body with `throw new NotImplementedException();`.
- Helper types that the official skeleton only shows in comments (`ListNode`, `TreeNode`, ...) do not go into this
  file. See section 3.
- This is the only situation in which writing `Solution.cs` is allowed. The hook only lets through a NEW file that
  contains `throw new NotImplementedException();`.

### `Problems/xxxx-slug/Problem.cs`

- Follow `Templates/Problem.md`: `namespace`, `Id`, `Title`, the `Input` record, and `Run`.
- `Input` properties mirror the official method's parameters: PascalCase names, same types as the official signature.
- `Run` calls `new Solution().Method(...)` and returns the result through `Json.Write(...)`. For a `void` method that
  modifies its argument in place, output the modified argument.
- This is only an adapter. Put no solving logic in it.

### `Inputs/xxxx-slug.json`

- One case per example in the statement, named `Example 1`, `Example 2`, ... The `input` property names are the method's
  parameter names in camelCase.
- Include only the examples from the statement. Extra edge cases are for the user to add; thinking them up is part of
  the practice.

### `Problems/xxxx-slug/README.md`

- Follow the format of `Problems/0001-two-sum/README.md`: `# N. Title`, `難度：`, the statement, each example (with the
  line `本機案例：` pointing at `Inputs/xxxx-slug.json` and the example name, the Input / Output block, and the
  Explanation), then Constraints.
- Keep the statement in LeetCode's original English. Do not translate or paraphrase it.
- Add no solution hints, topic tags, or related problems; they give the approach away. A Follow-up that is part of the
  original statement stays.

## 3. Stop and ask for special problem types

- **Needs a shared type or a JSON converter** (`ListNode`, `TreeNode`, `Node`, ...): this touches `Core/`. Explain what
  you would add and where, and wait for approval. Put the type in the `LeetCodePractice` namespace so that `Solution.cs`
  resolves it without a `using`.
- **Design problems** (`LRUCache`, `MinStack`, ... one class with several methods): the case format differs from a
  normal problem. Discuss the design of `Problem.cs` and the case JSON with the user first.

## 4. Verify

1. `dotnet build` must succeed.
2. `dotnet run -- <id>`: every case showing `NotImplementedException` is expected at this point (nothing is solved yet).
   What you are checking is that the problem is registered, the case JSON parses, and the `Input` conversion does not
   throw.

If any other error appears (case file not found, invalid JSON, conversion exception), fix it and run again.

## 5. Wrap up

- List the files you created and ask the user to confirm the slug and difficulty.
- **Do not start solving.** Hand over with an opening question, e.g. ask the user to explain the problem in their own
  words and say what approach comes to mind first. Mention `/hint` for when they are stuck and `/review-solution` for
  when they are done.
