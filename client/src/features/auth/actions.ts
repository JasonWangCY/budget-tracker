import * as actionType from "./constants";
import * as api from "../../api/index";
import { type UserInfo } from "./auth";

export interface AuthState {
  userInfo: UserInfo;
}

export interface AuthAction {
  type: string;
  userInfo: UserInfo;
}

export type AuthDispatchType = (args: AuthAction) => AuthAction;

export const signIn = (formData: UserInfo) => {
  return async (dispatch: AuthDispatchType) => {
    try {
      console.log("trying to sign in...");
      // TODO: Set up CORS request
      const { data } = await api.signIn(formData);
      const action: AuthAction = {
        type: actionType.AUTH,
        userInfo: formData,
      };

      dispatch(action);

      // router.push('/');
    } catch (error) {
      console.log(error);
    }
  };
};

export const signUp = (formData: UserInfo) => {
  return async (dispatch: AuthDispatchType) => {
    try {
      console.log("trying to sign up...");
      const { data } = await api.signUp(formData);
      const action: AuthAction = {
        type: actionType.AUTH,
        userInfo: formData,
      };

      dispatch(action);
    } catch (error) {
      console.log(error);
    }
  };
};
