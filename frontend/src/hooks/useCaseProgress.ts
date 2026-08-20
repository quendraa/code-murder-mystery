import { useEffect, useState } from "react";
import type { PlayerProgress } from "../types/progress";
import { getSessionId } from "../utils/session";
import { getProgress, startProgress } from "../api/progress";

export function useCaseProgress(caseId: string | undefined) {
  const [progress, setProgress] = useState<PlayerProgress | null>(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    if (!caseId) return;

    const sessionId = getSessionId();

    getProgress(caseId, sessionId)
      .then((data) => setProgress(data))
      .catch(async () => {
        const created = await startProgress(caseId, sessionId);
        setProgress(created);
      })
      .catch((err) =>
        setError(
          err instanceof Error ? err.message : "Failed to load progress",
        ),
      )
      .finally(() => setLoading(false));
  }, [caseId]);

  return { progress, loading, error };
}
