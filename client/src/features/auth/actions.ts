import { AUTH } from "./constants";
import * as api from "../../api/index";
import { type UserInfo } from "./auth";
import { type Dispatch } from "react";
import { type AnyAction } from "redux";

export const signIn =
  (formData: UserInfo) => async (dispatch: Dispatch<AnyAction>) => {
    try {
      const { data } = await api.signIn(formData);

      dispatch({ type: AUTH, data });

      // router.push('/');
    } catch (error) {
      console.log(error);
    }
  };
