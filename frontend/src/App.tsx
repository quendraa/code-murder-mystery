import { Routes, Route } from "react-router-dom";
import Landing from "./pages/Landing";
import CaseSelect from "./pages/CaseSelect";

function App() {
  return (
    <Routes>
      <Route path="/" element={<Landing />} />
      <Route path="/cases" element={<CaseSelect />} />
    </Routes>
  );
}

export default App;
