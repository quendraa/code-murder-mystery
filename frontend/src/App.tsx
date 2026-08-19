import { useEffect, useState } from "react";
import "./App.css";
import SuspectCard from "./components/SuspectCard";
import type { Case } from "./types/case";
import { getCase } from "./api/cases";

const CASE_ID = "f222da7c-1bd5-4f77-a19a-f28800198b3b"; // your seeded case's ID

function App() {
  const [caseData, setCaseData] = useState<Case | null>(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    getCase(CASE_ID)
      .then((data) => setCaseData(data))
      .catch((err) => setError(err.message))
      .finally(() => setLoading(false));
  }, []);

  if (loading) {
    return (
      <div className="min-h-screen bg-[#12141c] text-white flex items-center justify-center">
        Loading...
      </div>
    );
  }

  if (error || !caseData) {
    return (
      <div className="min-h-screen bg-[#12141c] text-red-400 flex items-center justify-center">
        Error: {error}
      </div>
    );
  }

  return (
    <div className="min-h-screen bg-[#12141c] flex flex-col items-center justify-center p-6 gap-8">
      <h1 className="text-white text-2xl font-bold">{caseData.title}</h1>
      <div className="flex gap-4 flex-wrap justify-center">
        {caseData.suspects.map((suspect) => (
          <SuspectCard
            key={suspect.id}
            name={suspect.name}
            role={suspect.bio}
          />
        ))}
      </div>
    </div>
  );
}

export default App;
