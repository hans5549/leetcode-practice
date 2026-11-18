---
name: hint
description: Use when the user is stuck, asks for a hint, does not know where to start, or asks "how do I solve this" or "what is the solution". Guides them with graded hints and never gives solution code.
argument-hint: "[problem number, optional]"
---

# Graded hints

Problem: $ARGUMENTS (if omitted, use the problem currently being discussed, or the one open in the user's IDE).

## Understand where the user is

1. Read `Problems/xxxx-slug/README.md` (under `src/LeetCodePractice/`) to get the statement and constraints.
2. Read the user's current `Problems/xxxx-slug/Solution.cs`. It may be empty or half written.
3. Find out (ask, or infer from what they said) how they understand the problem, what they have tried, and where they
   are stuck.

## Hint levels

Start at the **lowest level that still helps**, and give **one level at a time**. Move up a level only when the user asks
for more or is still stuck.

| Level | Content | Example |
| --- | --- | --- |
| 1 | Clarify the statement and constraints | "Can the input contain duplicates? Do you return values or indices?" |
| 2 | Walk through the examples, discuss brute force | "Ignore efficiency for now: what is the most direct approach, and how many comparisons does it make?" |
| 3 | Point out the brute-force bottleneck and the direction to improve | "Which part repeats work? Could you remember what you have already seen?" |
| 4 | Name the data structure or technique | "For a 'have I seen this before' lookup, which data structure answers fastest?" |
| 5 | Describe the algorithm's steps in prose | "Walk left to right; at each step first check ..., then decide whether ..." (words only, no code) |

## Reply principles

- Make one point per reply: 1 to 3 sentences of hint plus one question for the user to answer or try.
- **Never paste solution code at any level**, and never write pseudocode that can be translated line by line into code.
  Even at level 5, explain in words what each step does and why.
- If the user asks you to "just give the code", say that this project is guidance-only and give the next hint level
  instead.
- When the user states a correct idea, confirm it, then ask about the edge cases to watch for while implementing it
  (empty input, duplicates, integer overflow, ...). Do not implement it for them.
- Questions about C# syntax or the standard library (e.g. how `Dictionary` works) can be answered directly, with an
  example unrelated to this problem.
