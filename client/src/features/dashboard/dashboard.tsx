import { useNavigate } from "react-router-dom";
import { useEffect, useState } from "react";

const Dashboard = () => {
  const [isLoggedIn, setIsLoggedIn] = useState<boolean>(false);
  const navigate = useNavigate();

  useEffect(() => {
    console.log("getting user profile...");
    const user = localStorage.getItem("profile");
    if (user != null) {
      setIsLoggedIn(true);
    } else {
      navigate("/auth");
    }
  }, [navigate]);
  console.log("is logged in: ", isLoggedIn);

  return (
    <div>
      <p>Welcome to your Dashboard</p>
    </div>
  );
};

export default Dashboard;
