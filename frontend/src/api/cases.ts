import axios from "axios";
import type { Case } from "../types/case";
import { API_BASE_URL } from "./config";

export async function getCase(caseId: string): Promise<Case> {
  const response = await axios.get<Case>(`${API_BASE_URL}/cases/${caseId}`);
  return response.data;
}
