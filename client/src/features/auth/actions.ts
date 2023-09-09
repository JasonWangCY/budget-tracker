import * as actionType from "./constants";
import * as api from "../../api/index";
import { type UserInfo } from "./auth";
import { type NavigateFunction } from "react-router-dom";

export interface AuthState {
  userInfo: UserInfo;
}

export interface AuthAction {
  type: string;
  userInfo: UserInfo;
}

export type AuthDispatchType = (args: AuthAction) => AuthAction;

export const signIn = (formData: UserInfo, navigate: NavigateFunction) => {
  return async (dispatch: AuthDispatchType) => {
    try {
      console.log("trying to sign in...");
      const { data: authResponse } = await api.signIn(formData);
      const action: AuthAction = {
        type: actionType.AUTH,
        userInfo: formData,
      };

      localStorage.setItem("profile", authResponse.token);
      dispatch(action);

      navigate("/home");
    } catch (error) {
      console.log(error);
    }
  };
};

export const signUp = (formData: UserInfo, navigate: NavigateFunction) => {
  return async (dispatch: AuthDispatchType) => {
    try {
      console.log("trying to sign up...");
      const { data } = await api.signUp(formData);
      const action: AuthAction = {
        type: actionType.AUTH,
        userInfo: formData,
      };

      dispatch(action);

      navigate("/home");
    } catch (error) {
      console.log(error);
    }
  };
};
