import '../scss/index.scss';
import React, { ReactNode } from 'react';
import { connect } from 'react-redux';
import { fetchPosts, startRequest, stopRequest } from '../../../general/ts/redux/actions';
import { RootState } from '../../../general/ts/redux/types';

class DemoReaderPage extends React.Component<DemoReaderPageProps> {
    render(): ReactNode {
        return (
            <div className="demo-reader-page">
                <h1>Demo Reader Page</h1>
                <a href={this.props.MatchesDataApiPath}>API URL</a>
            </div>
        );
    }
}

type DemoReaderPageProps = {
    IsLoading: boolean;
    MatchesDataApiPath: string;
};

const mapStateToProps = (state: RootState) => {
    const IsLoading = state.app.IsLoading;
    return { IsLoading };
};

export default connect(
    mapStateToProps,
    { fetchPosts, startRequest, stopRequest }
)(DemoReaderPage);
