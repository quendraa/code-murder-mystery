import { useEffect, useState } from "react";
import type { CaseSummary } from "../types/case";
import { getAllCases } from "../api/cases";
import { useNavigate } from "react-router-dom";
import { StatusScreen } from "../components/shared/StatusScreen";
import { getSessionId } from "../utils/session";
import { startProgress } from "../api/progress";

function CaseSelect() {
  const [cases, setCases] = useState<CaseSummary[] | null>(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  const [selectedCase, setSelectedCase] = useState<CaseSummary | null>(null);
  const [detectiveName, setDetectiveName] = useState("");
  const [starting, setStarting] = useState(false);

  const navigate = useNavigate();

  useEffect(() => {
    getAllCases()
      .then((data) => setCases(data))
      .catch((err) => setError(err.message))
      .finally(() => setLoading(false));
  }, []);

  async function handleBegin() {
    if (!selectedCase || !detectiveName.trim()) return;

    setStarting(true);
    const sessionId = getSessionId();

    try {
      await startProgress(selectedCase.id, sessionId, detectiveName.trim());
      navigate(`/cases/${selectedCase.id}`);
    } catch (err) {
      setError(err instanceof Error ? err.message : "Failed to start case");
      setStarting(false);
    }
  }

  if (loading) {
    return <StatusScreen variant="loading" message="Loading cases..." />;
  }

  if (error || !cases) {
    return <StatusScreen variant="error" message={`Error: ${error}`} />;
  }

  if (selectedCase) {
    return (
      <div className="min-h-screen bg-[#12141c] flex flex-col items-center justify-center p-6 gap-6">
        <h1
          className="text-[#f0ede3] text-2xl"
          style={{ fontFamily: "'Special Elite', cursive" }}
        >
          {selectedCase.title}
        </h1>
        <p className="text-[#8b91a7] text-sm">Who's investigating tonight?</p>

        <input
          type="text"
          value={detectiveName}
          onChange={(e) => setDetectiveName(e.target.value)}
          placeholder="Detective's name"
          className="bg-[#ece6d6] text-[#12141c] px-4 py-3 rounded-sm w-full max-w-xs text-center"
          style={{ fontFamily: "'IBM Plex Mono', monospace" }}
        />

        <button
          onClick={handleBegin}
          disabled={!detectiveName.trim() || starting}
          className="px-6 py-3 bg-[#e8a33d] text-[#12141c] font-bold disabled:opacity-40 disabled:cursor-not-allowed hover:scale-105 transition-transform"
          style={{ fontFamily: "'IBM Plex Mono', monospace" }}
        >
          {starting ? "Starting..." : "Begin Investigation"}
        </button>

        <button
          onClick={() => setSelectedCase(null)}
          className="text-[#8b91a7] text-xs underline"
        >
          Back to cases
        </button>
      </div>
    );
  }

  return (
    <div className="min-h-screen bg-[#12141c] flex flex-col items-center justify-center p-6 gap-6">
      <h1
        className="text-[#f0ede3] text-2xl mb-4"
        style={{ fontFamily: "'Special Elite', cursive" }}
      >
        Choose a Case
      </h1>

      <div className="flex flex-col gap-4 w-full max-w-md">
        {cases.map((c) => (
          <button
            key={c.id}
            onClick={() => setSelectedCase(c)}
            className="bg-[#ece6d6] px-5 py-4 text-left hover:scale-[1.02] transition-transform shadow-lg rounded-sm"
          >
            <div className="text-[#12141c] font-bold text-lg">{c.title}</div>
            <div
              className="text-[#a9762f] text-xs mt-1"
              style={{ fontFamily: "'IBM Plex Mono', monospace" }}
            >
              Victim: {c.victimName}
            </div>
          </button>
        ))}
      </div>
    </div>
  );
}

export default CaseSelect;
