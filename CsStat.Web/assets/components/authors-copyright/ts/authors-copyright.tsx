import React, { ReactNode } from 'react';
import constants from '../../../general/ts/constants';

const API_URL_TAG = (repoPath: string): string => `https://api.github.com/repos${repoPath}/contributors`;

export default class AuthorsCopyright extends React.Component<any, AuthorsCopyrightState> {
    state = {
        data: [
            {
                /* eslint-disable */
                avatar_url: '',
                html_url: '',
                login: ''
                /* eslint-enable */
            }
        ]
    };

    private getApiUrl(): string {
        const repoPath = new URL(constants.REPOSITORY).pathname;
        return API_URL_TAG(repoPath);
    }

    private getGithubAccounts = async () => {
        const apiUrl = this.getApiUrl();
        const accounts: UserData[] = await fetch(apiUrl)
            .then((res: Response) => res.json())
            .then((acc) => acc.filter((elem: { type: string }) => elem.type === 'User'))
            .catch((err) => {
                throw new Error(err);
            });

        this.setState({
            data: accounts
        });
    };

    componentDidMount = () => {
        this.getApiUrl();
        this.getGithubAccounts();
    };

    render(): ReactNode {
        const { data } = this.state;
        return (
            <div className="contributors">
                <div className="contributors__title">
                    <h4 className="contributors__title-txt">Authors and Contributors</h4>
                </div>

                <div className="contributors__content">
                    <ul className="contributors__list">
                        {data.map((item, index) => (
                            <li key={index} className="contributors__item">
                                <a className="contributors__link" href={item.html_url}>
                                    <div className="contributors__item-title">{item.login}</div>
                                </a>
                            </li>
                        ))}
                    </ul>
                </div>
            </div>
        );
    }
}

type AuthorsCopyrightState = {
    data: UserData[];
};

type UserData = {
    avatar_url: string;
    html_url: string;
    login: string;
};
