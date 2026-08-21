export interface AccusationResult {
  isCorrect: boolean;
  actualKillerName: string;
  solutionText: string;
  accusedSuspectName: string | null;
}
