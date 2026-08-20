import { Routes, Route } from "react-router-dom";
import Landing from "./pages/Landing";
import CaseSelect from "./pages/CaseSelect";
import { CaseHub } from "./pages/CaseHub";

function App() {
  return (
    <Routes>
      <Route path="/" element={<Landing />} />
      <Route path="/cases" element={<CaseSelect />} />
      <Route path="/cases/:id" element={<CaseHub />} />
    </Routes>
  );
}

export default App;
