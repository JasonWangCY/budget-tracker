import { combineReducers } from "redux";
import authReducer from "../features/auth/reducer";

const rootReducer = combineReducers({ authReducer });

export default rootReducer;
