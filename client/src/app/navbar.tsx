import { Box, Button, IconButton, Toolbar, Typography } from "@mui/material";
import AppBar from "@mui/material/AppBar";
import { useNavigate } from "react-router-dom";
import MenuIcon from "@mui/icons-material/Menu";

export const Navbar = () => {
  let isLoggedIn: boolean = localStorage.getItem("profile") != null;
  const navigate = useNavigate();

  const logOut = () => {
    if (isLoggedIn) {
      localStorage.removeItem("profile");
      isLoggedIn = false;

      navigate("/auth");
    }
  };

  return (
    <Box sx={{ flexGrow: 1 }}>
      <AppBar>
        <Toolbar>
          <IconButton
            size="large"
            edge="start"
            color="inherit"
            aria-label="menu"
            sx={{ mr: 2 }}
          >
            <MenuIcon />
          </IconButton>
          <Typography variant="h6" component="div" sx={{ flexGrow: 1 }}>
            Budget Tracker
          </Typography>
          {isLoggedIn && (
            <Button onClick={logOut} color="inherit">
              Log Out
            </Button>
          )}
        </Toolbar>
      </AppBar>
    </Box>
  );
};
