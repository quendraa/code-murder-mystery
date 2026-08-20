import { Routes, Route } from "react-router-dom";
import Landing from "./pages/Landing";
import CaseSelect from "./pages/CaseSelect";
import { CaseHub } from "./pages/CaseHub";
import { Puzzle } from "./pages/Puzzle";

function App() {
  return (
    <Routes>
      <Route path="/" element={<Landing />} />
      <Route path="/cases" element={<CaseSelect />} />
      <Route path="/cases/:id" element={<CaseHub />} />
      <Route path="/clues/:id" element={<Puzzle />} />
    </Routes>
  );
}

export default App;
