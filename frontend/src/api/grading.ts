import axios from "axios";
import { API_BASE_URL } from "./config";

export interface TestCaseResult {
  passed: boolean;
  isHidden: boolean;
  input: string | null;
  expectedOutput: string | null;
  actualOutput: string | null;
  errorMessage: string | null;
}

export interface SubmitSolutionResponse {
  allTestsPassed: boolean;
  results: TestCaseResult[];
  unlockedEvidenceTitle: string | null;
}

export async function submitSolution(
  clueId: string,
  code: string,
  sessionId: string,
): Promise<SubmitSolutionResponse> {
  const response = await axios.post<SubmitSolutionResponse>(
    `${API_BASE_URL}/clues/${clueId}/submit`,
    { code, sessionId },
  );
  return response.data;
}
