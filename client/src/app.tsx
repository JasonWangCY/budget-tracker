import { Routes, Route } from "react-router-dom";
import { Container } from "@mui/material";
import { Navbar } from "./app/navbar";
import Auth from "./features/auth/auth";
import Dashboard from "./features/dashboard/dashboard";

function App() {
  return (
    <Container>
      <Navbar />
      <Routes>
        <Route path="/auth" element={<Auth />} />
        <Route path="/home" element={<Dashboard />} />
      </Routes>
    </Container>
  );
}

export default App;
