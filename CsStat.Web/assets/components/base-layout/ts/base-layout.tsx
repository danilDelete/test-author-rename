import React, { SFC, ReactNode } from 'react';
import {
    Layout, Typography, Row, Col, Divider
} from 'antd';
import IconCopyright from '../../icon-copyright';
import AuthorsCopyright from '../../authors-copyright';
import '../scss/index.scss';
import Navigation from '../../navigation';

const { Header, Content, Footer } = Layout;
const { Title } = Typography;

const BaseLayout: SFC<BaseLayoutProps> = (props) => (
    <Layout className="base-layout__layout">
        <Header className="base-layout__header">
            <Row type="flex" justify="start" align="middle">
                <Col xs={6} lg={2} className="base-layout__logo">
                    <span style={{ fontSize: '3rem', }}>🏅</span>
                </Col>
                <Col xs={12} lg={6}>
                    <Title level={1} className="base-layout__title">
                        Fuse8 CS:GO Statistics
                    </Title>
                </Col>
                <Col xs={6} lg={10}>
                    <Navigation/>
                </Col>
            </Row>
        </Header>
        <Content className="base-layout__content">
            {props.children}
        </Content>
        <Footer>
            <AuthorsCopyright />
            <Divider />
            <a href="https://bitbucket.org/radik_fayskhanov/counterstrikestat">Repository is available on Bitbucket.</a>
            <Divider />
            <IconCopyright />
        </Footer>
    </Layout>
);

type BaseLayoutProps = {
    children: ReactNode;
};

export default BaseLayout;
