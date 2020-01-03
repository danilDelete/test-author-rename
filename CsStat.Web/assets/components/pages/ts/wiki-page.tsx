import React, { ReactNode } from 'react';
import '../scss/index.scss';
import { Row, Col } from 'antd';
import { connect } from 'react-redux';
import { fetchPosts, startRequest, stopRequest } from '../../../general/ts/redux/actions';
import { RootState, Post as PostType } from '../../../general/ts/redux/types';
import Post from '../../post';

class WikiPage extends React.Component<WikiPageProps> {
    fetchPosts(WikiDataApiPath: string): void {
        const url = new URL(WikiDataApiPath);

        fetch(url.toString())
            .then((res: Response) => res.json())
            .then((data: PostType[]) => {
                data = typeof data === 'string' ? JSON.parse(data) : data;

                this.props.fetchPosts(data);
            })
            .catch((error) => {
                throw new Error(error);
            });
    }

    componentDidMount(): void {
        this.fetchPosts(this.props.WikiDataApiPath);
    }

    render(): ReactNode {
        const { Posts } = this.props;
        return (
            <Row type="flex" justify="start" align="middle">
                <Col xs={24} lg={14}>
                    { Posts && Posts.map((post, index) => <Post key={index} post={post}/>) }
                </Col>
            </Row>
        );
    }
}

type WikiPageProps = {
    Posts: PostType[];
    IsLoading: boolean;
    WikiDataApiPath: string;
    fetchPosts: typeof fetchPosts;
};

const mapStateToProps = (state: RootState) => {
    const IsLoading = state.app.IsLoading;
    const Posts = state.app.Posts;
    return { IsLoading, Posts };
};

export default connect(
    mapStateToProps,
    { fetchPosts, startRequest, stopRequest }
)(WikiPage);
