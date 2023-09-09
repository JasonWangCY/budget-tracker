import { useEffect, useState } from "react";
import { useAppDispatch } from "../../app/hooks";
import { signIn, signUp } from "./actions";
import {
  IconButton,
  TextField,
  InputAdornment,
  Grid,
  Button,
} from "@mui/material";
import { Visibility, VisibilityOff } from "@mui/icons-material";
import { type Dispatch } from "redux";
import { Navigate, useNavigate } from "react-router-dom";

export interface UserInfo {
  firstName: string;
  lastName: string | undefined;
  userName: string;
  email: string;
  password: string;
}

const initialState: UserInfo = {
  firstName: "",
  lastName: "",
  userName: "",
  email: "",
  password: "",
};

const Auth = () => {
  const [form, setForm] = useState<UserInfo>(initialState);
  const [isSignUp, setIsSignUp] = useState<boolean>(false);
  const dispatch: Dispatch<any> = useAppDispatch();
  const navigate = useNavigate();
  // TODO: Need to move away from storing JWT in local storage.
  // https://dev.to/rdegges/please-stop-using-local-storage-1i04
  // Consider storing in HTTP only cookies,
  // but the downside is that there is no easy way to add the authorization header in the HTTP request.
  // https://www.learmoreseekmore.com/2022/10/reactjs-v18-jwtauthentication-using-httponly-cookie.html
  const [isLoggedIn, setIsLoggedIn] = useState<boolean>(
    localStorage.getItem("profile") != null,
  );
  const [showPassword, setShowPassword] = useState(false);

  // TODO: Handle redirects in a more systematic way
  // https://stackoverflow.com/questions/70341850/react-redirect-to-login-page-when-not-authenticated
  if (isLoggedIn) {
    return <Navigate to="/home" replace={true} />;
  }

  const handleShowPassword = () => {
    setShowPassword(!showPassword);
  };

  const switchMode = () => {
    setIsSignUp((prevIsSignUp) => !prevIsSignUp);
    setShowPassword(false);
  };

  const handleSubmit = (e: any) => {
    e.preventDefault();

    if (isSignUp) {
      dispatch(signUp(form, navigate));
    } else {
      dispatch(signIn(form, navigate));
    }
  };

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setForm({ ...form, [e.target.name]: e.target.value });
  };

  return (
    <div className="Auth">
      <div>
        <h1>{isSignUp ? "Sign up" : "Sign in"}</h1>
      </div>
      <form onSubmit={handleSubmit}>
        {isSignUp && (
          <>
            <TextField
              name="firstName"
              label="First Name"
              onChange={handleChange}
            />
            <TextField
              name="lastName"
              label="Last Name"
              onChange={handleChange}
            />
            <TextField
              name="email"
              label="Email Address"
              onChange={handleChange}
              type="email"
            />
          </>
        )}
        <TextField name="userName" label="User Name" onChange={handleChange} />
        <TextField
          name="password"
          label="Password"
          onChange={handleChange}
          type={showPassword ? "text" : "password"}
          InputProps={{
            endAdornment: (
              <InputAdornment position="end">
                <IconButton
                  aria-label="toggle password visibility"
                  onClick={handleShowPassword}
                  edge="end"
                >
                  {showPassword ? <VisibilityOff /> : <Visibility />}
                </IconButton>
              </InputAdornment>
            ),
          }}
        />
        {isSignUp && (
          <TextField
            name="confirmPassword"
            label="Confirm Password"
            // TODO: Validate password confirmation
            type="password"
          />
        )}
      </form>
      <Button
        type="submit"
        variant="contained"
        color="primary"
        onClick={handleSubmit}
      >
        {isSignUp ? "Sign Up" : "Sign In"}
      </Button>
      <Grid container>
        <Grid item>
          <Button onClick={switchMode}>
            {isSignUp
              ? "Already have an account? Sign in"
              : "Don't have an account? Sign up"}
          </Button>
        </Grid>
      </Grid>
    </div>
  );
};

export default Auth;
