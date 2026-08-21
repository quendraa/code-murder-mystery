export interface PlayerProgress {
  caseId: string;
  sessionId: string;
  currentClueIndex: number;
  solvedClueIds: string[];
  startedAt: string;
  completedAt: string | null;
  accusedCorrectly: boolean | null;
  accusedSuspectId: string | null;
}
