import React, { ReactNode } from 'react';
import { Table, Avatar, Divider, Tooltip, Dropdown, Icon, Button } from 'antd';
import { connect } from 'react-redux';
import { fetchPlayers, startRequest, stopRequest, selectPlayer } from '../../../general/ts/redux/actions';
import FilterForm, { DateValues } from './filter-form';
import { IAppState, RootState, Player } from '../../../general/ts/redux/types';
import ColumnsSelector from './columns-selector';
import { ColumnProps } from 'antd/es/table';
import { nameof } from '../../../general/ts/extentions';
import '../scss/index.scss';


const CELL_CSS_CLASS = 'players-data__cell';
const HIDDEN_CELL_CSS_CLASS = 'is-hidden';

export const COLUMN_NAMES: ColumnNames = {
    ImagePath: { dataIndex: nameof<Player>('ImagePath'), readableName: 'ImagePath' },
    Name: { dataIndex: nameof<Player>('Name'), readableName: 'Players Name' },
    Points: { dataIndex: nameof<Player>('Points'), readableName: 'Points' },
    KdRatio: { dataIndex: nameof<Player>('KdRatio'), readableName: 'K/D Ratio' },
    Kills: { dataIndex: nameof<Player>('Kills'), readableName: 'Kills' },
    Deaths: { dataIndex: nameof<Player>('Deaths'), readableName: 'Deaths' },
    TotalGames: { dataIndex: nameof<Player>('TotalGames'), readableName: 'Total Games' },
    KillsPerGame: { dataIndex: nameof<Player>('KillsPerGame'), readableName: 'Kills/Game' },
    HeadShot: { dataIndex: nameof<Player>('HeadShot'), readableName: 'HeadShot %' },
    Assists: { dataIndex: nameof<Player>('Assists'), readableName: 'Assists' },
    AssistsPerGame: { dataIndex: nameof<Player>('AssistsPerGame'), readableName: 'Assists/Game' },
    DefusedBombs: { dataIndex: nameof<Player>('DefusedBombs'), readableName: 'Defused Bombs' },
    ExplodedBombs: { dataIndex: nameof<Player>('ExplodedBombs'), readableName: 'Exploded Bombs' },
    FriendlyKills: { dataIndex: nameof<Player>('FriendlyKills'), readableName: 'Friendly Kills' }
};
const DEFAULT_COLUMNS = [
    COLUMN_NAMES.KdRatio.dataIndex,
    COLUMN_NAMES.Kills.dataIndex,
    COLUMN_NAMES.Deaths.dataIndex,
    COLUMN_NAMES.HeadShot.dataIndex,
    COLUMN_NAMES.Assists.dataIndex,
    COLUMN_NAMES.TotalGames.dataIndex
];

const PERMANENT_COLUMNS = [COLUMN_NAMES.ImagePath.dataIndex, COLUMN_NAMES.Name.dataIndex];
class PlayersData extends React.Component<PlayersDataProps, PlayersDataState> {
    readonly state = {
        visibleColumns: [...DEFAULT_COLUMNS, ...PERMANENT_COLUMNS]
    };

    private fetchPlayers(playersDataUrl: string, params?: DateValues): void {
        const url = new URL(playersDataUrl, window.location.origin);
        if (params) {
            url.search = new URLSearchParams(params).toString();
        }

        this.props.startRequest();

        fetch(url.toString())
            .then((res: Response) => res.json())
            .then((data: IAppState) => {
                data = typeof data === 'string' ? JSON.parse(data) : data;
                this.props.fetchPlayers(data);
            })
            .catch((error) => {
                this.props.stopRequest();
                throw new Error(error);
            });
    }

    private getAvatar(record: Player): ReactNode {
        if (record.ImagePath) {
            return <Avatar className="players-data__avatar" src={record.ImagePath} />;
        }
        return <Avatar icon="user" />;
    }

    private onRowClick(record: Player): void {
        this.props.selectPlayer(record.Id);
    }

    private cellWrapper(id: string, content: ReactNode): ReactNode {
        const isSelectedClass = id === this.props.SelectedPlayer ? 'is-selected' : '';
        return <div className={`players-data__cell-inner ${isSelectedClass}`}>{content}</div>;
    }

    onFormSubmit = (params: DateValues): void => {
        this.fetchPlayers(this.props.playersDataUrl, params);
    };

    onCheckboxesChange = (selectedColumns: string[]): void => {
        const visibleColumns = [...PERMANENT_COLUMNS, ...selectedColumns];
        this.setState({ visibleColumns });
    };

    private getCellClassName(dataIndex: string): string {
        if (this.state.visibleColumns.includes(dataIndex)) {
            return CELL_CSS_CLASS;
        } else {
            return `${CELL_CSS_CLASS} ${HIDDEN_CELL_CSS_CLASS}`;
        }
    }

    private getColumns = (): ColumnProps<Player>[] => [
        {
            dataIndex: COLUMN_NAMES.ImagePath.dataIndex,
            render: (_link: any, record: Player) => {
                const content = this.getAvatar(record);
                return this.cellWrapper(record.Id, content);
            },
            width: '5%',
            className: this.getCellClassName(COLUMN_NAMES.ImagePath.dataIndex)
        },
        {
            dataIndex: COLUMN_NAMES.Name.dataIndex,
            title: COLUMN_NAMES.Name.readableName,
            className: this.getCellClassName(COLUMN_NAMES.Name.dataIndex),
            render: (_link: any, record: Player) => {
                return this.cellWrapper(record.Id, record.Name);
            },
            sorter: (a: Player, b: Player) => a.Name.localeCompare(b.Name)
        },
        {
            dataIndex: COLUMN_NAMES.Points.dataIndex,
            title: COLUMN_NAMES.Points.readableName,
            className: this.getCellClassName(COLUMN_NAMES.Points.dataIndex),
            render: (_link: any, record: Player) => {
                return this.cellWrapper(record.Id, record.Points);
            },
            sorter: (a: Player, b: Player) => b.Points - a.Points
        },
        {
            dataIndex: COLUMN_NAMES.KdRatio.dataIndex,
            title: () => <Tooltip title="Kills / Deaths">{COLUMN_NAMES.KdRatio.readableName}</Tooltip>,
            className: this.getCellClassName(COLUMN_NAMES.KdRatio.dataIndex),
            render: (_link: any, record: Player) => {
                return this.cellWrapper(record.Id, record.KdRatio);
            },
            sorter: (a: Player, b: Player) => b.KdRatio - a.KdRatio
        },
        {
            dataIndex: COLUMN_NAMES.Kills.dataIndex,
            title: COLUMN_NAMES.Kills.readableName,
            className: this.getCellClassName(COLUMN_NAMES.Kills.dataIndex),
            render: (_link: any, record: Player) => {
                return this.cellWrapper(record.Id, record.Kills);
            },
            sorter: (a: Player, b: Player) => b.Kills - a.Kills
        },
        {
            dataIndex: COLUMN_NAMES.Deaths.dataIndex,
            title: COLUMN_NAMES.Deaths.readableName,
            className: this.getCellClassName(COLUMN_NAMES.Deaths.dataIndex),
            render: (_link: any, record: Player) => {
                return this.cellWrapper(record.Id, record.Deaths);
            },
            sorter: (a: Player, b: Player) => b.Deaths - a.Deaths
        },
        {
            dataIndex: COLUMN_NAMES.KillsPerGame.dataIndex,
            title: COLUMN_NAMES.KillsPerGame.readableName,
            className: this.getCellClassName(COLUMN_NAMES.KillsPerGame.dataIndex),
            render: (_link: any, record: Player) => {
                return this.cellWrapper(record.Id, record.KillsPerGame);
            },
            sorter: (a: Player, b: Player) => b.KillsPerGame - a.KillsPerGame
        },
        {
            dataIndex: COLUMN_NAMES.HeadShot.dataIndex,
            title: COLUMN_NAMES.HeadShot.readableName,
            className: this.getCellClassName(COLUMN_NAMES.HeadShot.dataIndex),
            render: (_link: any, record: Player) => {
                return this.cellWrapper(record.Id, record.HeadShot);
            },
            sorter: (a: Player, b: Player) => b.HeadShot - a.HeadShot
        },
        {
            dataIndex: COLUMN_NAMES.Assists.dataIndex,
            title: COLUMN_NAMES.Assists.dataIndex,
            className: this.getCellClassName(COLUMN_NAMES.Assists.dataIndex),
            render: (_link: any, record: Player) => {
                return this.cellWrapper(record.Id, record.Assists);
            },
            sorter: (a: Player, b: Player) => b.Assists - a.Assists
        },
        {
            dataIndex: COLUMN_NAMES.AssistsPerGame.dataIndex,
            title: COLUMN_NAMES.AssistsPerGame.readableName,
            className: this.getCellClassName(COLUMN_NAMES.AssistsPerGame.dataIndex),
            render: (_link: any, record: Player) => {
                return this.cellWrapper(record.Id, record.AssistsPerGame);
            },
            sorter: (a: Player, b: Player) => b.AssistsPerGame - a.AssistsPerGame
        },
        {
            dataIndex: COLUMN_NAMES.DefusedBombs.dataIndex,
            title: COLUMN_NAMES.DefusedBombs.readableName,
            className: this.getCellClassName(COLUMN_NAMES.DefusedBombs.dataIndex),
            render: (_link: any, record: Player) => {
                return this.cellWrapper(record.Id, record.DefusedBombs);
            },
            sorter: (a: Player, b: Player) => b.DefusedBombs - a.DefusedBombs
        },
        {
            dataIndex: COLUMN_NAMES.ExplodedBombs.dataIndex,
            title: COLUMN_NAMES.ExplodedBombs.readableName,
            className: this.getCellClassName(COLUMN_NAMES.ExplodedBombs.dataIndex),
            render: (_link: any, record: Player) => {
                return this.cellWrapper(record.Id, record.ExplodedBombs);
            },
            sorter: (a: Player, b: Player) => b.ExplodedBombs - a.ExplodedBombs
        },
        {
            dataIndex: COLUMN_NAMES.FriendlyKills.dataIndex,
            title: COLUMN_NAMES.FriendlyKills.readableName,
            className: this.getCellClassName(COLUMN_NAMES.FriendlyKills.dataIndex),
            render: (_link: any, record: Player) => {
                return this.cellWrapper(record.Id, record.FriendlyKills);
            },
            sorter: (a: Player, b: Player) => b.FriendlyKills - a.FriendlyKills
        },
        {
            dataIndex: COLUMN_NAMES.TotalGames.dataIndex,
            title: COLUMN_NAMES.TotalGames.readableName,
            className: this.getCellClassName(COLUMN_NAMES.TotalGames.dataIndex),
            render: (_link: any, record: Player) => {
                return this.cellWrapper(record.Id, record.TotalGames);
            },
            sorter: (a: Player, b: Player) => b.TotalGames - a.TotalGames
        }
    ];

    get columnSelector(): ReactNode {
        const { visibleColumns } = this.state;
        const colsToRender = visibleColumns.filter((x) => !PERMANENT_COLUMNS.includes(x));
        return <ColumnsSelector visibleColumns={colsToRender} onCheckboxesChange={this.onCheckboxesChange} />;
    }

    render(): ReactNode {
        const { IsLoading, DateFrom, DateTo, Players } = this.props;

        return (
            <>
                <Divider orientation="left">Choose Dates to Filter Statistics</Divider>
                <div className="players-data__filters">
                    <FilterForm
                        onFormSubmit={this.onFormSubmit}
                        isLoading={IsLoading}
                        dateFrom={DateFrom}
                        dateTo={DateTo}
                    />
                    <Dropdown overlay={this.columnSelector} trigger={['click']}>
                        <Button className="ant-dropdown-link">
                            Select columns to render <Icon type="down" />
                        </Button>
                    </Dropdown>
                </div>
                <Divider />
                <Table
                    className="players-data"
                    rowClassName={() => 'players-data__row'}
                    columns={this.getColumns()}
                    dataSource={Players}
                    rowKey={record => record.Id}
                    pagination={false}
                    size="middle"
                    bordered={true}
                    scroll={{ x: true }}
                    loading={IsLoading}
                    onRow={record => {
                        return {
                            onClick: () => {
                                this.onRowClick(record);
                            }
                        };
                    }}
                />
            </>
        );
    }
}

type PlayersDataProps = {
    playersDataUrl: string;
    SelectedPlayer: string;
    IsLoading: boolean;
    DateFrom: string;
    DateTo: string;
    Players: Player[];
    fetchPlayers: typeof fetchPlayers;
    startRequest: typeof startRequest;
    stopRequest: typeof stopRequest;
    selectPlayer: typeof selectPlayer;
};

type PlayersDataState = {
    visibleColumns: string[];
};

const mapStateToProps = (state: RootState) => {
    const SelectedPlayer = state.app.SelectedPlayer;
    const IsLoading = state.app.IsLoading;
    const DateFrom = state.app.DateFrom;
    const DateTo = state.app.DateTo;
    const Players = state.app.Players;
    return { SelectedPlayer, IsLoading, DateFrom, DateTo, Players };
};

export type ColumnNames = {
    [key: string]: ColumnMapping;
    ImagePath: ColumnMapping;
    Name: ColumnMapping;
    Points: ColumnMapping;
    KdRatio: ColumnMapping;
    Kills: ColumnMapping;
    Deaths: ColumnMapping;
    TotalGames: ColumnMapping;
    KillsPerGame: ColumnMapping;
    HeadShot: ColumnMapping;
    Assists: ColumnMapping;
    AssistsPerGame: ColumnMapping;
    DefusedBombs: ColumnMapping;
    ExplodedBombs: ColumnMapping;
    FriendlyKills: ColumnMapping;
};

export type ColumnMapping = {
    dataIndex: string;
    readableName: string;
};

export default connect(
    mapStateToProps,
    { fetchPlayers, startRequest, stopRequest, selectPlayer }
)(PlayersData);
