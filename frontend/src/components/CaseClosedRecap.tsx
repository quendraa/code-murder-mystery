import type { AccusationResult } from "../types/accusation";

interface CaseClosedRecapProps {
  result: AccusationResult;
  onReplay: () => void;
  replaying: boolean;
}

export function CaseClosedRecap({
  result,
  onReplay,
  replaying,
}: CaseClosedRecapProps) {
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
        CASE ALREADY CLOSED
      </div>

      <h1
        className="text-[#f0ede3] text-2xl max-w-lg"
        style={{ fontFamily: "'Special Elite', cursive" }}
      >
        {result.isCorrect
          ? `You correctly named ${result.actualKillerName}.`
          : `You accused ${result.accusedSuspectName ?? "someone"} - but the killer was ${result.actualKillerName}.`}
      </h1>

      <p className="text-[#8b91a7] text-sm max-w-md leading-relaxed">
        {result.solutionText}
      </p>

      <button
        onClick={onReplay}
        disabled={replaying}
        className="px-6 py-3 bg-[#e8a33d] text-[#12141c] font-bold disabled:opacity-40"
        style={{ fontFamily: "'IBM Plex Mono', monospace" }}
      >
        {replaying ? "Resetting..." : "Replay this case"}
      </button>
    </div>
  );
}
