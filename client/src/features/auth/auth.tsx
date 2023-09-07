import { useState } from "react";
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

export interface UserInfo {
  firstName: string;
  lastName: string | undefined;
  userName: string;
  email: string;
  password: string;
}

// TODO: Can we initialise with last Name = null?
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

  const [showPassword, setShowPassword] = useState(false);
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
      dispatch(signUp(form));
    } else {
      dispatch(signIn(form));
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
