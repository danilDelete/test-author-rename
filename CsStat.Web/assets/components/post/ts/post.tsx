import React, { SFC, ReactNode } from 'react';
import { Card, Tag } from 'antd';
import Markdown from 'markdown-to-jsx';
import moment from 'moment';
import { Post as PostType, Tag as TagType } from '../../../general/ts/redux/types';

const USER_DATE_FORMAT = 'll';
const TAG_COLORS = [
    'magenta',
    'red',
    'volcano',
    'orange',
    'gold',
    'lime',
    'green',
    'cyan',
    'blue',
    'geekblue',
    'purple'
]
const Post: SFC<PostProps> = (props) => {
    const getRandomColor = (colors: string[]): string => {
        return colors[Math.floor(Math.random() * colors.length)];
    }

    const getTags = (tags: TagType[]): ReactNode => {
        if (tags.length === 0) {
            return <span></span>;
        }
        return tags.map((tag: TagType, index: number) => {
            return <Tag key={index} color={getRandomColor(TAG_COLORS)}>{tag.Caption}</Tag>;
        });
    };

    const {
        Title, Content, createdAt, tags
    } = props.post;

    return (
        <Card className="post" title={Title} extra={getTags(tags)}>
            <Markdown className="markdown-body">{Content}</Markdown>
            <p>{moment(new Date(createdAt)).format(USER_DATE_FORMAT)}</p>
        </Card>
    );
};

type PostProps = {
    post: PostType;
};

export default Post;
