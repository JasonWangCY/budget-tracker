import { Routes, Route } from "react-router-dom";
import { Container } from "@mui/material";
import { Navbar } from "./app/navbar";
import Auth from "./features/auth/auth";
import Home from "./features/home/home";

function App() {
  return (
    <Container>
      <Navbar />
      <Routes>
        <Route path="/" element={<Auth />} />
        <Route path="/auth" element={<Auth />} />
        <Route path="/home" element={<Home />} />
      </Routes>
    </Container>
  );
}

export default App;
