import axios from "axios";
import type { PlayerProgress } from "../types/progress";
import { API_BASE_URL } from "./config";

export async function startProgress(
  caseId: string,
  sessionId: string,
): Promise<PlayerProgress> {
  const result = await axios.post(`${API_BASE_URL}/progress/${caseId}/start`, {
    sessionId,
  });

  return result.data;
}

export async function getProgress(
  caseId: string,
  sessionId: string,
): Promise<PlayerProgress> {
  const response = await axios.get<PlayerProgress>(
    `${API_BASE_URL}/progress/${caseId}`,
    {
      params: { sessionId },
    },
  );

  return response.data;
}
