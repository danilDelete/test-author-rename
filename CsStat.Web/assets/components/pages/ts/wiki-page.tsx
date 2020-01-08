import React, { ReactNode } from 'react';
import '../scss/index.scss';
import { Row, Col, Empty } from 'antd';
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

    getPosts(): ReactNode {
        const { Posts } = this.props;

        if (Posts.length > 0) {
            return (
                <Col xs={24} lg={14}>
                    {Posts.map((post, index) => <Post key={index} post={post}/>)}
                </Col>
            );
        }
        return (
            <Col xs={24} lg={24}>
                <Empty/>
            </Col>
        );
    }

    render(): ReactNode {
        return (
            <Row type="flex" justify="start" align="middle">
                { this.getPosts() }
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
