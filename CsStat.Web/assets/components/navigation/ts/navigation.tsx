import { Menu } from 'antd';
import React, { ReactNode } from 'react';
import { connect } from 'react-redux';
import { Link } from 'react-router-dom';
import { RootState } from '../../../general/ts/redux/types';
import constants from '../../../general/ts/constants';

class Navigation extends React.Component <NavigationProps> {
    render(): ReactNode {
        return (
            <Menu
                className="navigation"
                mode="horizontal"
                theme="dark"
                selectedKeys={[this.props.router.location.pathname]}
            >
                <Menu.Item key={constants.ROUTES.HOME}>
                    <Link to={constants.ROUTES.HOME}>
                        Home
                    </Link>
                </Menu.Item>
                <Menu.Item key={constants.ROUTES.WIKI}>
                    <Link to={constants.ROUTES.WIKI}>Wiki</Link>
                </Menu.Item>
                <Menu.Item key={constants.ROUTES.DEMO_READER}>
                    <Link to={constants.ROUTES.DEMO_READER}>Demo Reader</Link>
                </Menu.Item>
            </Menu>
        );
    }
}
type NavigationProps = {
    router: any;
};
const mapStateToProps = (state: RootState): NavigationProps => ({
    router: state.router
});

export default connect(mapStateToProps)(Navigation);
