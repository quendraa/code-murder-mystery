import axios from "axios";
import type { AccusationResult } from "../types/accusation";
import { API_BASE_URL } from "./config";

export async function submitAccusation(
  caseId: string,
  sessionId: string,
  suspectId: string,
): Promise<AccusationResult> {
  const response = await axios.post<AccusationResult>(
    `${API_BASE_URL}/cases/${caseId}/accuse`,
    {
      sessionId,
      suspectId,
    },
  );

  return response.data;
}
