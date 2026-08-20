import axios from "axios";
import type { Player } from "../types/player";
import { API_BASE_URL } from "./config";

export async function registerPlayer(
  sessionId: string,
  detectiveName: string,
): Promise<Player> {
  const result = await axios.post<Player>(`${API_BASE_URL}/player/register`, {
    sessionId,
    detectiveName,
  });

  return result.data;
}

export async function getPlayer(sessionId: string): Promise<Player | null> {
  try {
    const result = await axios.get<Player>(`${API_BASE_URL}/player`, {
      params: { sessionId },
    });
    return result.data;
  } catch {
    return null;
  }
}
