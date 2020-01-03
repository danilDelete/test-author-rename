import React, { SFC } from 'react';
import { Provider } from 'react-redux';
import { Route, Switch } from 'react-router-dom';
import { ConnectedRouter } from 'connected-react-router';
import BaseLayout from '../../base-layout/ts/base-layout';
import configureStore, { history } from '../../../general/ts/redux/store';
import HomePage from '../../pages/ts/home-page';
import WikiPage from '../../pages/ts/wiki-page';
import NotFoundPage from '../../pages/ts/not-found-page';

const store = configureStore();

const App: SFC<AppProps> = (props) => (
    <Provider store={store}>
        <ConnectedRouter history={history}>
            <BaseLayout>
                <Switch>
                    <Route exact path="/" >
                        <HomePage playersDataUrl={props.playersDataUrl}/>
                    </Route>
                    <Route exact path="/wiki" >
                        <WikiPage WikiDataApiPath={props.WikiDataApiPath}/>
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
};

export default App;
