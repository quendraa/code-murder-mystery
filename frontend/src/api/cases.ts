import axios from "axios";
import type { Case, CaseSummary } from "../types/case";
import { API_BASE_URL } from "./config";
import type { ClueDetail, ClueSummary } from "../types/clue";

export async function getAllCases(): Promise<CaseSummary[]> {
  const response = await axios.get<CaseSummary[]>(`${API_BASE_URL}/cases`);
  return response.data;
}

export async function getCase(caseId: string): Promise<Case> {
  const response = await axios.get<Case>(`${API_BASE_URL}/cases/${caseId}`);
  return response.data;
}

export async function getCluesByCase(caseId: string): Promise<ClueSummary[]> {
  const response = await axios.get<ClueSummary[]>(
    `${API_BASE_URL}/cases/${caseId}/clues`,
  );

  return response.data;
}

export async function getClue(clueId: string): Promise<ClueDetail> {
  const response = await axios.get<ClueDetail>(
    `${API_BASE_URL}/clues/${clueId}`,
  );
  return response.data;
}
