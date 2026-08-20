import { useEffect, useState } from "react";
import type { CaseSummary } from "../types/case";
import { getAllCases } from "../api/cases";
import { getPlayer, registerPlayer } from "../api/player";
import { getProgress } from "../api/progress";
import { getSessionId } from "../utils/session";
import { useNavigate } from "react-router-dom";
import { StatusScreen } from "../components/shared/StatusScreen";
import { PlayerNameForm } from "../components/PlayerNameForm";
import { CaseCard } from "../components/CaseCard";

export function CaseSelect() {
  const [cases, setCases] = useState<CaseSummary[] | null>(null);
  const [detectiveName, setDetectiveName] = useState<string | null>(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
  const [registering, setRegistering] = useState(false);

  const navigate = useNavigate();
  const sessionId = getSessionId();

  useEffect(() => {
    Promise.all([getAllCases(), getPlayer(sessionId)])
      .then(([casesResult, playerResult]) => {
        setCases(casesResult);
        if (playerResult) {
          setDetectiveName(playerResult.detectiveName);
        }
      })
      .catch((err) => setError(err.message))
      .finally(() => setLoading(false));
  }, [sessionId]);

  async function handleRegister(name: string) {
    setRegistering(true);
    try {
      const player = await registerPlayer(sessionId, name);
      setDetectiveName(player.detectiveName);
    } catch (err) {
      setError(err instanceof Error ? err.message : "Failed to register");
    } finally {
      setRegistering(false);
    }
  }

  async function handleSelectCase(c: CaseSummary) {
    try {
      const existingProgress = await getProgress(c.id, sessionId);
      if (existingProgress) {
        navigate(`/cases/${c.id}`);
        return;
      }
    } catch {
      // no progress yet - CaseHub will create it on load
    }

    navigate(`/cases/${c.id}`);
  }

  if (loading) {
    return <StatusScreen variant="loading" message="Loading..." />;
  }

  if (error || !cases) {
    return <StatusScreen variant="error" message={`Error: ${error}`} />;
  }

  if (!detectiveName) {
    return (
      <PlayerNameForm onSubmit={handleRegister} submitting={registering} />
    );
  }

  return (
    <div className="min-h-screen bg-[#12141c] flex flex-col items-center justify-center p-6 gap-6">
      <h1
        className="text-[#f0ede3] text-2xl mb-2"
        style={{ fontFamily: "'Special Elite', cursive" }}
      >
        Choose a Case
      </h1>
      <p className="text-[#8b91a7] text-xs mb-4">
        Welcome back, Detective {detectiveName}
      </p>

      <div className="flex flex-col gap-4 w-full max-w-md">
        {cases.map((c) => (
          <CaseCard
            key={c.id}
            title={c.title}
            victimName={c.victimName}
            onClick={() => handleSelectCase(c)}
          />
        ))}
      </div>
    </div>
  );
}
