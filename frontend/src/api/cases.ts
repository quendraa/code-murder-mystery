import axios from "axios";
import type { Case, CaseSummary } from "../types/case";
import { API_BASE_URL } from "./config";

export async function getAllCases(): Promise<CaseSummary[]> {
  const response = await axios.get<CaseSummary[]>(`${API_BASE_URL}/cases`);
  return response.data;
}

export async function getCase(caseId: string): Promise<Case> {
  const response = await axios.get<Case>(`${API_BASE_URL}/cases/${caseId}`);
  return response.data;
}
