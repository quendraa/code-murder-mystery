import axios from "axios";
import type { Evidence } from "../types/evidence";
import { API_BASE_URL } from "./config";

export async function getEvidence(
  caseId: string,
  sessionId: string,
): Promise<Evidence[]> {
  const response = await axios.get<Evidence[]>(
    `${API_BASE_URL}/evidence/${caseId}`,
    {
      params: { sessionId },
    },
  );

  return response.data;
}
