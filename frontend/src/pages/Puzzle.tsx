import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import type { ClueDetail } from "../types/clue";
import { submitSolution, type SubmitSolutionResponse } from "../api/grading";
import { getClue } from "../api/cases";
import { getSessionId } from "../utils/session";
import { StatusScreen } from "../components/shared/StatusScreen";
import { Editor } from "@monaco-editor/react";

export function Puzzle() {
  const { id } = useParams<{ id: string }>();
  const navigate = useNavigate();

  const [clue, setClue] = useState<ClueDetail | null>(null);
  const [code, setCode] = useState("");
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  const [submitting, setSubmitting] = useState(false);
  const [result, setResult] = useState<SubmitSolutionResponse | null>(null);

  useEffect(() => {
    if (!id) return;

    getClue(id)
      .then((data) => {
        setClue(data);
        setCode(data.starterCode);
      })
      .catch((err) => setError(err))
      .finally(() => setLoading(false));
  }, [id]);

  async function handleSubmit() {
    if (!id) return;

    setSubmitting(true);
    setResult(null);

    try {
      const sessionId = getSessionId();
      const response = await submitSolution(id, code, sessionId);
      setResult(response);
    } catch (err) {
      setError(err instanceof Error ? err.message : "Submission failed.");
    } finally {
      setSubmitting(false);
    }
  }

  if (loading) {
    return <StatusScreen variant="loading" message="Loading clue..." />;
  }

  if (error || !clue) {
    return <StatusScreen variant="error" message={`Error: ${error}`} />;
  }

  return (
    <div className="min-h-screen bg-[#12141c] p-6">
      <div className="max-w-6xl mx-auto">
        <button
          onClick={() => navigate(-1)}
          className="text-[#8b91a7] text-xs mb-6"
          style={{ fontFamily: "'IBM Plex Mono', monospace" }}
        >
          ← Back to case hub
        </button>

        <div className="grid grid-cols-1 lg:grid-cols-2 gap-8">
          {/* Left: narrative + prompt */}
          <div>
            <div
              className="text-[#a9762f] text-xs tracking-widest uppercase mb-3"
              style={{ fontFamily: "'IBM Plex Mono', monospace" }}
            >
              Evidence Source: {clue.sourceLabel}
            </div>
            <h1
              className="text-[#f0ede3] text-2xl mb-4"
              style={{ fontFamily: "'Special Elite', cursive" }}
            >
              Clue {clue.orderIndex}
            </h1>
            <div className="bg-[#ece6d6] px-4 py-4 mb-5 text-[#12141c] text-sm leading-relaxed">
              {clue.promptText}
            </div>
            <span
              className="text-[10px] px-2 py-1 border border-[#e8a33d] text-[#e8a33d] uppercase"
              style={{ fontFamily: "'IBM Plex Mono', monospace" }}
            >
              {clue.puzzleType}
            </span>
          </div>
          <div>
            <div className="border border-[#232739] rounded-sm overflow-hidden">
              <Editor
                height="300px"
                language={clue.language}
                value={code}
                onChange={(value) => setCode(value ?? "")}
                theme="vs-dark"
                options={{ minimap: { enabled: false }, fontSize: 13 }}
              />
            </div>

            <div className="mt-4">
              <div
                className="text-[#8b91a7] text-xs mb-2"
                style={{ fontFamily: "'IBM Plex Mono', monospace" }}
              >
                Visible test cases
              </div>
              <div className="flex flex-col gap-1">
                {clue.visibleTestCases.map((tc, i) => (
                  <div
                    key={i}
                    className="text-[#8b91a7] text-xs bg-[#1a1d29] px-3 py-2"
                    style={{ fontFamily: "'IBM Plex Mono', monospace" }}
                  >
                    input: {tc.input} → expected: {tc.expectedOutput}
                  </div>
                ))}
              </div>
            </div>

            <button
              onClick={handleSubmit}
              disabled={submitting}
              className="w-full mt-4 px-4 py-3 bg-[#e8a33d] text-[#12141c] font-bold disabled:opacity-40"
              style={{ fontFamily: "'IBM Plex Mono', monospace" }}
            >
              {submitting ? "Running..." : "SUBMIT"}
            </button>

            {result && (
              <div className="mt-4 flex flex-col gap-2">
                {result.results.map((r, i) => (
                  <div
                    key={i}
                    className="px-3 py-2 text-xs"
                    style={{
                      fontFamily: "'IBM Plex Mono', monospace",
                      background: r.passed ? "#1a2e22" : "#2e1a1a",
                      color: r.passed ? "#6fcf97" : "#e2685c",
                    }}
                  >
                    {r.isHidden ? "Hidden test" : `Test: ${r.input}`} -{" "}
                    {r.passed ? "PASS" : "FAIL"}
                    {!r.isHidden && !r.passed && r.errorMessage && (
                      <div className="mt-1 text-[#e2685c] opacity-80">
                        {r.errorMessage}
                      </div>
                    )}
                  </div>
                ))}

                {result.allTestsPassed && (
                  <div className="text-[#6fcf97] text-sm mt-2">
                    ✓ Evidence unlocked: {result.unlockedEvidenceTitle}
                  </div>
                )}
              </div>
            )}
          </div>
        </div>
      </div>
    </div>
  );
}
