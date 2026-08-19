import axios from "axios";
import type { PlayerProgress } from "../types/progress";
import { API_BASE_URL } from "./config";

export async function startProgress(
  caseId: string,
  sessionId: string,
  detectiveName: string,
): Promise<PlayerProgress> {
  const result = await axios.post(`${API_BASE_URL}/progress/${caseId}/start`, {
    sessionId,
    detectiveName,
  });

  return result.data;
}
