import { Routes, Route } from "react-router-dom";
import { CaseSelect } from "./pages/CaseSelect";
import { CaseHub } from "./pages/CaseHub";
import { Puzzle } from "./pages/Puzzle";
import { Landing } from "./pages/Landing";
import { Accusation } from "./pages/Accusation";

function App() {
  return (
    <Routes>
      <Route path="/" element={<Landing />} />
      <Route path="/cases" element={<CaseSelect />} />
      <Route path="/cases/:id" element={<CaseHub />} />
      <Route path="/clues/:id" element={<Puzzle />} />
      <Route path="/cases/:id/accuse" element={<Accusation />} />
    </Routes>
  );
}

export default App;
