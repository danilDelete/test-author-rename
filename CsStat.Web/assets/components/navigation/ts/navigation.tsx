import { Menu } from 'antd';
import React, { ReactNode } from 'react';
import { connect } from 'react-redux';
import { Link } from 'react-router-dom';
import { RootState } from '../../../general/ts/redux/types';

class Navigation extends React.Component <NavigationProps> {
    render(): ReactNode {
        return (
            <Menu
                className="navigation"
                mode="horizontal"
                theme="dark"
                selectedKeys={[this.props.router.location.pathname]}
            >
                <Menu.Item key="/">
                    <Link to="/">
                        Home
                    </Link>
                </Menu.Item>
                <Menu.Item key="/wiki">
                    <Link to="/wiki">Wiki</Link>
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
