import { useState } from "react";
import { useAppDispatch } from "../../app/hooks";
import { signIn } from "./actions";

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

const SignUp = () => {
  const [form, setForm] = useState(initialState);
  const [isSignUp, setIsSignUp] = useState(false);
  const dispatch = useAppDispatch();

  const [showPassword, setShowPassword] = useState(false);
  const handleShowPassword = () => {
    setShowPassword(!showPassword);
  }

  const switchMode = () => {};

  const handleSubmit = (e: any) => {
    e.preventDefault();

    if (isSignUp) {
      dispatch(signIn(form));
    }
  };
};

export const Auth = () => {
  return (
    <>
      <div>
        <p>Auth page</p>
      </div>
    </>
  );
};
