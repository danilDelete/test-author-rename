import React from 'react';
import { List, Avatar, Tag, Typography } from 'antd';

const { Title } = Typography;

const AuthorsCopyright = () => (
    <List
        size="small"
        bordered={true}
        header={<Title level={4}>Authors and Contributors:</Title>}
        itemLayout="horizontal"
        dataSource={data}
        renderItem={person => (
            <List.Item>
                <List.Item.Meta
                    avatar={<Avatar src={person.avatarSrc} />}
                    title={
                        <div>
                            <a href={person.githubLink}>{person.name}</a> – <Tag>{person.role}</Tag>
                        </div>
                    }
                />
            </List.Item>
        )}
    />
);
export default AuthorsCopyright;
const githubAccounts = {
    rdk174: 'Rdk174',
    medprj: 'Medprj',
    host: 'hostile-d'
};

const data = [
    {
        name: 'Radik F',
        githubLink: `https://github.com/${githubAccounts.rdk174}`,
        avatarSrc: `https://github.com/${githubAccounts.rdk174}.png`,
        role: 'backend'
    },
    {
        name: 'Alexander M',
        githubLink: `https://github.com/${githubAccounts.medprj}`,
        avatarSrc: `https://github.com/${githubAccounts.medprj}.png`,
        role: 'backend'
    },
    {
        name: 'Danil S',
        githubLink: `https://github.com/${githubAccounts.host}`,
        avatarSrc: `https://github.com/${githubAccounts.host}.png`,
        role: 'frontend'
    }
];
