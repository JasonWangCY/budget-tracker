import * as actionType from "./constants";
import { type AuthState, type AuthAction } from "./actions";

const initialState: AuthState = {
  userInfo: {
    firstName: "",
    lastName: "",
    userName: "",
    email: "",
    password: "",
  },
};

const authReducer = (
  state: AuthState = initialState,
  action: AuthAction,
): AuthState => {
  console.log(action.userInfo);

  switch (action.type) {
    case actionType.AUTH:
      console.log("AUTH");

      return { ...state, userInfo: action.userInfo };
    case actionType.LOGOUT:
      console.log("LOGOUT");
      localStorage.clear();

      return { ...state, userInfo: initialState.userInfo };
    default:
      console.log("default");
      return state;
  }
};

export default authReducer;
