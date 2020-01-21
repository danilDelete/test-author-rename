import React, { SFC } from 'react';
import { Provider } from 'react-redux';
import { Route, Switch } from 'react-router-dom';
import { ConnectedRouter } from 'connected-react-router';
import BaseLayout from '../../base-layout/ts/base-layout';
import configureStore, { history } from '../../../general/ts/redux/store';
import HomePage from '../../pages/ts/home-page';
import WikiPage from '../../pages/ts/wiki-page';
import NotFoundPage from '../../pages/ts/not-found-page';
import DemoReaderPage from '../../pages/ts/demo-reader-page';
import constants from '../../../general/ts/constants';

const store = configureStore();

const App: SFC<AppProps> = (props) => (
    <Provider store={store}>
        <ConnectedRouter history={history}>
            <BaseLayout>
                <Switch>
                    <Route exact path={constants.ROUTES.HOME} >
                        <HomePage playersDataUrl={props.playersDataUrl}/>
                    </Route>
                    <Route exact path={constants.ROUTES.WIKI} >
                        <WikiPage WikiDataApiPath={props.WikiDataApiPath}/>
                    </Route>
                    <Route exact path={constants.ROUTES.DEMO_READER} >
                        <DemoReaderPage MatchesDataApiPath={props.MatchesDataApiPath}/>
                    </Route>
                    <Route>
                        <NotFoundPage/>
                    </Route>
                </Switch>

            </BaseLayout>
        </ConnectedRouter>
    </Provider>
);

type AppProps = {
    playersDataUrl: string;
    WikiDataApiPath: string;
    MatchesDataApiPath: string;
};

export default App;
