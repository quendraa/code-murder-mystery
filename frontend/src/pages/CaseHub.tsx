import { useEffect, useState } from "react";
import { useParams, useNavigate } from "react-router-dom";
import { getCase, getCluesByCase } from "../api/cases";
import { getEvidence } from "../api/evidence";
import { getSessionId } from "../utils/session";
import { useCaseProgress } from "../hooks/useCaseProgress";
import type { Case } from "../types/case";
import SuspectCard from "../components/SuspectCard";
import type { ClueSummary } from "../types/clue";
import type { Evidence } from "../types/evidence";
import { StatusScreen } from "../components/shared/StatusScreen";

export function CaseHub() {
  const { id } = useParams<{ id: string }>();
  const navigate = useNavigate();

  const [caseData, setCaseData] = useState<Case | null>(null);
  const [clues, setClues] = useState<ClueSummary[]>([]);
  const [evidence, setEvidence] = useState<Evidence[]>([]);

  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  const {
    progress,
    loading: progressLoading,
    error: progressError,
  } = useCaseProgress(id);

  useEffect(() => {
    if (!id) return;

    const sessionId = getSessionId();

    Promise.all([getCase(id), getCluesByCase(id), getEvidence(id, sessionId)])
      .then(([caseResult, cluesResult, evidenceResult]) => {
        setCaseData(caseResult);
        setClues(cluesResult);
        setEvidence(evidenceResult);
      })
      .catch((err) => setError(err.message))
      .finally(() => setLoading(false));
  }, [id]);

  if (loading || progressLoading) {
    return <StatusScreen variant="loading" message="Loading case file..." />;
  }

  if (error || progressError || !caseData || !progress) {
    return (
      <StatusScreen
        variant="error"
        message={`Error: ${error || progressError}`}
      />
    );
  }

  return (
    <div className="min-h-screen bg-[#12141c] p-6">
      <div className="max-w-5xl mx-auto">
        <div className="mb-8">
          <div
            className="text-[#a9762f] text-xs tracking-widest uppercase mb-2"
            style={{ fontFamily: "'IBM Plex Mono', monospace" }}
          >
            Case in progress
          </div>
          <h1
            className="text-[#f0ede3] text-3xl"
            style={{ fontFamily: "'Special Elite', cursive" }}
          >
            {caseData.title}
          </h1>
        </div>

        <div className="grid grid-cols-1 lg:grid-cols-3 gap-8">
          <div className="lg:col-span-1">
            <div
              className="text-[#a9762f] text-xs tracking-widest uppercase mb-3"
              style={{ fontFamily: "'IBM Plex Mono', monospace" }}
            >
              Investigation
            </div>
            <div className="flex flex-col gap-2">
              {clues.map((clue) => {
                const isSolved = progress.solvedClueIds.includes(clue.id);
                return (
                  <button
                    key={clue.id}
                    onClick={() => navigate(`/clues/${clue.id}`)}
                    className="px-3 py-3 border text-left w-full"
                    style={{
                      borderColor: isSolved ? "#6fcf97" : "#232739",
                      opacity: isSolved ? 0.6 : 1,
                    }}
                  >
                    <div
                      className="text-[#f0ede3] text-sm font-bold"
                      style={{
                        textDecoration: isSolved ? "line-through" : "none",
                      }}
                    >
                      {clue.sourceLabel}
                    </div>
                    <div
                      className="text-[#8b91a7] text-xs"
                      style={{ fontFamily: "'IBM Plex Mono', monospace" }}
                    >
                      {clue.puzzleType}
                    </div>
                  </button>
                );
              })}
            </div>
          </div>

          <div className="lg:col-span-2 flex flex-col gap-8">
            <div>
              <div
                className="text-[#a9762f] text-xs tracking-widest uppercase mb-3"
                style={{ fontFamily: "'IBM Plex Mono', monospace" }}
              >
                Suspects
              </div>
              <div className="flex gap-4 flex-wrap">
                {caseData.suspects.map((suspect) => (
                  <SuspectCard
                    key={suspect.id}
                    name={suspect.name}
                    role={suspect.bio}
                  />
                ))}
              </div>
            </div>

            <div>
              <div
                className="text-[#a9762f] text-xs tracking-widest uppercase mb-3"
                style={{ fontFamily: "'IBM Plex Mono', monospace" }}
              >
                Evidence Board
              </div>
              {evidence.length === 0 ? (
                <div className="text-[#8b91a7] text-sm">
                  No evidence uncovered yet.
                </div>
              ) : (
                <div className="grid grid-cols-1 sm:grid-cols-2 gap-3">
                  {evidence.map((e) => (
                    <div
                      key={e.id}
                      className="bg-[#ece6d6] px-4 py-4 rounded-sm"
                    >
                      <div className="text-[#12141c] font-bold text-sm">
                        {e.title}
                      </div>
                      <div className="text-[#3d3a34] text-xs mt-2 leading-relaxed">
                        {e.descriptionText}
                      </div>
                      {e.linkedSuspectName && (
                        <div
                          className="text-[#a9762f] text-xs mt-2"
                          style={{ fontFamily: "'IBM Plex Mono', monospace" }}
                        >
                          → {e.linkedSuspectName}
                        </div>
                      )}
                    </div>
                  ))}
                </div>
              )}
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}
