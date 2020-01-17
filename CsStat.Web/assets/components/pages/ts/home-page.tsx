import React, { SFC } from 'react';
import { Row, Col } from 'antd';
import PlayersData from '../../players-data/ts/players-data';
import PlayerCard from '../../player-card/ts/player-card';
import '../scss/index.scss';

const HomePage: SFC<HomePageProps> = props => (
    <div className="home-page">
        <Row type="flex" justify="start">
            <Col xs={24} lg={14}>
                <PlayersData playersDataUrl={props.playersDataUrl} />
            </Col>
            <Col xs={24} lg={{ span: 9, offset: 1 }}>
                <div className="home-page__card">
                    <PlayerCard />
                </div>
            </Col>
        </Row>
    </div>
);

type HomePageProps = {
    playersDataUrl: string;
};

export default HomePage;
