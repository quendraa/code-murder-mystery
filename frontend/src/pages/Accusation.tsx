import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { getCase } from "../api/cases";
import type { Case } from "../types/case";
import { StatusScreen } from "../components/shared/StatusScreen";
import SuspectCard from "../components/SuspectCard";
import { getSessionId } from "../utils/session";
import type { AccusationResult } from "../types/accusation";
import { submitAccusation } from "../api/accusation";

export function Accusation() {
  const { id } = useParams<{ id: string }>();
  const navigate = useNavigate();

  const [caseData, setCaseData] = useState<Case | null>(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
  const [selectedSuspectId, setSelectedSuspectId] = useState<string | null>(
    null,
  );

  const [submitting, setSubmitting] = useState(false);
  const [result, setResult] = useState<AccusationResult | null>(null);

  useEffect(() => {
    if (!id) return;

    getCase(id)
      .then((data) => setCaseData(data))
      .catch((err) => setError(err))
      .finally(() => setLoading(false));
  }, [id]);

  async function handleSubmit() {
    if (!id || !selectedSuspectId) return;

    setSubmitting(true);

    try {
      const sessionId = getSessionId();
      const data = await submitAccusation(id, sessionId, selectedSuspectId);
      setResult(data);
    } catch (err) {
      setError(
        err instanceof Error ? err.message : "Failed to submit accusation",
      );
    } finally {
      setSubmitting(false);
    }
  }

  if (loading) {
    return <StatusScreen variant="loading" message="Loading case file..." />;
  }

  if (error || !caseData) {
    return <StatusScreen variant="error" message={`Error: ${error}`} />;
  }

  if (result) {
    return (
      <div className="min-h-screen bg-[#12141c] flex flex-col items-center justify-center p-6 gap-6 text-center">
        <div
          className="px-6 py-2"
          style={{
            border: `3px solid ${result.isCorrect ? "#6fcf97" : "#e2685c"}`,
            color: result.isCorrect ? "#6fcf97" : "#e2685c",
            fontFamily: "'IBM Plex Mono', monospace",
            fontWeight: 700,
            letterSpacing: 2,
            transform: "rotate(-4deg)",
          }}
        >
          {result.isCorrect ? "CASE CLOSED" : "WRONG SUSPECT"}
        </div>

        <h1
          className="text-[#f0ede3] text-2xl max-w-lg"
          style={{ fontFamily: "'Special Elite', cursive" }}
        >
          {result.isCorrect
            ? `Correct — it was ${result.actualKillerName}.`
            : `It wasn't them. The killer was ${result.actualKillerName}.`}
        </h1>

        <p className="text-[#8b91a7] text-sm max-w-md leading-relaxed">
          {result.solutionText}
        </p>

        <button
          onClick={() => navigate("/cases")}
          className="px-6 py-3 bg-[#e8a33d] text-[#12141c] font-bold"
          style={{ fontFamily: "'IBM Plex Mono', monospace" }}
        >
          Back to cases
        </button>
      </div>
    );
  }

  return (
    <div className="min-h-screen bg-[#12141c] p-6">
      <div className="max-w-2xl mx-auto flex flex-col items-center gap-8">
        <h1
          className="text-[#f0ede3] text-2xl"
          style={{ fontFamily: "'Special Elite', cursive" }}
        >
          Name the Killer
        </h1>

        <div className="flex gap-4 flex-wrap justify-center">
          {caseData.suspects.map((suspect) => (
            <SuspectCard
              key={suspect.id}
              name={suspect.name}
              role={suspect.bio}
              selected={selectedSuspectId === suspect.id}
              onClick={() => setSelectedSuspectId(suspect.id)}
            />
          ))}
        </div>

        <button
          onClick={handleSubmit}
          disabled={!selectedSuspectId || submitting}
          className="px-6 py-3 bg-[#b5432f] text-[#f0ede3] font-bold disabled:opacity-40"
          style={{ fontFamily: "'IBM Plex Mono', monospace" }}
        >
          {submitting ? "Filing..." : "File Accusation"}
        </button>
      </div>
    </div>
  );
}
