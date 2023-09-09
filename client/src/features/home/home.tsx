import { Navigate, useNavigate } from "react-router-dom";
import { useEffect, useState } from "react";

const Home = () => {
  const [isLoggedIn, setIsLoggedIn] = useState<boolean>(
    localStorage.getItem("profile") != null,
  );
  const navigate = useNavigate();

  // useEffect(() => {
  //   console.log("getting user profile...");
  //   const user = localStorage.getItem("profile");
  //   if (user != null) {
  //     setIsLoggedIn(true);
  //   } else {
  //     navigate("/auth");
  //   }
  // }, [navigate]);
  // console.log("is logged in: ", isLoggedIn);

  if (!isLoggedIn) {
    return <Navigate to="/auth" replace={true} />;
  }

  return (
    <div>
      <p>Welcome to your Dashboard</p>
    </div>
  );
};

export default Home;
