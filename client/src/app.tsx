import { Routes, Route } from "react-router-dom";
import { Container } from "@mui/material";
import { Navbar } from "./app/navbar";
import Auth from "./features/auth/auth";

function App() {
  return (
    <Container>
      <Navbar />
      <Routes>
        <Route path="/auth" element={<Auth />} />
      </Routes>
    </Container>
  );
}

export default App;
