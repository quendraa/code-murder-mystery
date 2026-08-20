export interface ClueSummary {
  id: string;
  orderIndex: string;
  sourceLabel: string;
  puzzleType: string;
}

export interface ClueDetail {
  id: string;
  orderIndex: number;
  sourceLabel: string;
  puzzleType: string;
  promptText: string;
  starterCode: string;
  language: string;
  visibleTestCases: { input: string; expectedOutput: string }[];
}
