import { Reducer, combineReducers } from 'redux';
import { connectRouter } from 'connected-react-router';
import {
    IAppState,
    ActionTypes,
    SELECT_PLAYER,
    FETCH_PLAYERS_DATA,
    START_REQUEST,
    STOP_REQUEST,
    FETCH_POSTS_DATA
} from './types';

export const initialState: IAppState = {
    IsLoading: false,
    Players: [],
    SelectedPlayer: '',
    DateFrom: '',
    DateTo: '',
    Posts: []
};

const appReducer: Reducer<IAppState> = (
    state: IAppState = initialState,
    action: ActionTypes
): IAppState => {
    switch (action.type) {
        case FETCH_PLAYERS_DATA:
            return {
                ...state,
                IsLoading: false,
                Players: action.payload.Players,
                DateFrom: action.payload.DateFrom,
                DateTo: action.payload.DateTo,
                SelectedPlayer: ''
            };
        case FETCH_POSTS_DATA:
            return {
                ...state,
                IsLoading: false,
                Posts: action.payload,
            };
        case SELECT_PLAYER:
            return {
                ...state,
                SelectedPlayer: action.payload
            };

        case START_REQUEST:
            return {
                ...state,
                IsLoading: true
            };

        case STOP_REQUEST:
            return {
                ...state,
                IsLoading: false
            };

        default:
            return state;
    }
};

export const createRootReducer = (history: any): Reducer =>
    combineReducers({
        router: connectRouter(history),
        app: appReducer
    });

export default createRootReducer;

