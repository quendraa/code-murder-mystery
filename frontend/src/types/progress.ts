export interface PlayerProgress {
  caseId: string;
  sessionId: string;
  detectiveName: string | null;
  currentClueIndex: number;
  solvedClueIds: string[];
  startedAt: string;
  completedAt: string | null;
}
