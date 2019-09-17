import { combineReducers } from 'redux';
import { IAppState, ActionTypes, SELECT_PLAYER, FETCH_PLAYERS_DATA, START_REQUEST } from './types';

const initialState: IAppState = {
    isLoading: false ,
    players: [],
    selectedPlayer: '',
    DateFrom: '',
    DateTo: ''
}
const rootReducer = (state = initialState, action: ActionTypes) => {
    switch (action.type) {
        case FETCH_PLAYERS_DATA:
            return {
                ...state,
                isLoading: false,
                players: action.payload.players,
                DateFrom: action.payload.DateFrom,
                DateTo: action.payload.DateTo
            };
        case SELECT_PLAYER:
            return {
                ...state,
                selectedPlayer: action.payload
            };

        case START_REQUEST:
            return {
                ...state,
                isLoading: true
            };
        default:
            return state;
    }
};

export default rootReducer;