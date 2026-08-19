import { useEffect, useState } from "react";
import type { CaseSummary } from "../types/case";
import { getAllCases } from "../api/cases";
import { useNavigate } from "react-router-dom";

function CaseSelect() {
  const [cases, setCases] = useState<CaseSummary[] | null>(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  const navigate = useNavigate();

  useEffect(() => {
    getAllCases()
      .then((data) => setCases(data))
      .catch((err) => setError(err.message))
      .finally(() => setLoading(false));
  }, []);

  if (loading) {
    return (
      <div className="min-h-screen bg-[#12141c] text-white flex items-center justify-center">
        Loading cases...
      </div>
    );
  }

  if (error || !cases) {
    return (
      <div className="min-h-screen bg-[#12141c] text-red-400 flex items-center justify-center">
        Error: {error}
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
            onClick={() => navigate(`/cases/${c.id}`)}
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
