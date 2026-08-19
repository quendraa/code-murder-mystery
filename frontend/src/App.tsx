import "./App.css";
import SuspectCard from "./components/SuspectCard";

const suspects = [
  { name: "Priya Patel", role: "Co-founder / CEO" },
  { name: "Dev Okafor", role: "Junior Engineer" },
  { name: "Sarah Kim", role: "Company Counsel" },
  { name: "Tom Reyes", role: "Lead Investor" },
  { name: "Alex Park", role: "Intern" },
];

function App() {
  return (
    <div className="min-h-screen bg-[#12141c] flex items-center justify-center p-6">
      <div className="flex gap-4 flex-wrap justify-center">
        {suspects.map((suspect) => (
          <SuspectCard
            key={suspect.name}
            name={suspect.name}
            role={suspect.role}
          />
        ))}
      </div>
    </div>
  );
}

export default App;
