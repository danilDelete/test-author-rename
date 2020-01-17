import {
    createStore, applyMiddleware, Store
} from 'redux';
import { composeWithDevTools } from 'redux-devtools-extension/logOnlyInProduction';
import { routerMiddleware } from 'connected-react-router';
import * as History from 'history';
import createRootReducer from './reducers';

export const history = History.createBrowserHistory();
const middleware = applyMiddleware(routerMiddleware(history));
const composedEnhancers = composeWithDevTools(middleware);

export default function configureStore(preloadedState?: any): Store {
    const store = createStore(
        createRootReducer(history), // root reducer with router state
        preloadedState,
        composedEnhancers
    );

    return store;
}
export type AppState = ReturnType<typeof createRootReducer>;
