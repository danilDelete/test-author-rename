import React from 'react';
import { RadialChart, Hint, DiscreteColorLegend } from 'react-vis';
import { Badge, Card } from 'antd';
import randomColor from 'randomcolor';
import MapGunNameToImageUrl from './mapping/gun-image-map';
import { Gun } from '../../../general/ts/redux/types';
import 'react-vis/dist/style.css';

class GunsChart extends React.Component<GunsChartProps, GunsChartState> {
    componentWillMount() {
        this.setState({ isHovered: false });
        const { guns } = this.props;
        this.updateState(guns);
    }

    componentWillReceiveProps(nextProps: GunsChartProps) {
        const { guns } = nextProps;
        this.updateState(guns);
    }

    render() {
        const { data, legendItems, hoveredChartSection, selectedImageUrl, selectedImageName, isHovered } = this.state;
        return (
            <div className="guns-chart__container">
                <DiscreteColorLegend
                    className="guns-chart__legend"
                    items={legendItems}
                    onItemMouseEnter={(item: LegendItem) => this.setStateForImage(item.id)}
                    onItemMouseLeave={() => this.resetStateForImage()}
                />
                <RadialChart
                    className="guns-chart__chart"
                    innerRadius={50}
                    radius={100}
                    getAngle={(d: Data) => d.theta}
                    data={data}
                    onValueMouseOver={(v: Data) => {
                        this.setStateForImage(v.id);
                        this.setState({ hoveredChartSection: v, isHovered: true });
                    }}
                    onSeriesMouseOut={() => {
                        this.resetStateForImage();
                        this.setState({ isHovered: false });
                    }}
                    width={200}
                    height={200}
                    padAngle={0.04}
                    animation={{ damping: 9, stiffness: 300 }}
                >
                    {isHovered !== false ? (
                        <Hint value={hoveredChartSection}>
                            <Badge count={hoveredChartSection.theta} overflowCount={10000}>
                                <div className="guns-chart__text">{hoveredChartSection.label}</div>
                            </Badge>
                        </Hint>
                    ) : null}
                </RadialChart>
                <div className="guns-chart__card-wrapper">
                    {selectedImageUrl && (
                        <Card
                            className="guns-chart__card"
                            size="small"
                            title={selectedImageName}
                            bodyStyle={{ position: 'relative', flex: '1' }}
                        >
                            <div className="guns-chart__img-wrapper">
                                <img src={selectedImageUrl}></img>
                            </div>
                        </Card>
                    )}
                </div>
            </div>
        );
    }

    private updateState(guns: Gun[]) {
        const colors: Color[] = this.getColors(guns);
        const data: Data[] = this.getData(guns, colors);
        const legendItems: LegendItem[] = this.getLegend(guns, colors);
        this.setState({ colors, data, legendItems });
    }

    private getData = (guns: Gun[], colors: Color[]) =>
        guns.map(g => ({
            theta: g.Kills,
            label: g.Name,
            id: g.Id,
            style: {
                fill: this.findColor(colors, g.Id),
                stroke: false
            }
        }));

    private getColors = (guns: Gun[]) =>
        guns.map(g => ({
            id: g.Id,
            color: randomColor({
                luminosity: 'bright',
                hue: 'random'
            })
        }));

    private getLegend = (guns: Gun[], colors: Color[]) =>
        guns.map(g => ({
            title: `${g.Name}: ${g.Kills} kills`,
            id: g.Id,
            color: this.findColor(colors, g.Id)
        }));

    private setStateForImage = (id: number) => {
        const { guns } = this.props;
        const selectedImageUrl = MapGunNameToImageUrl(this.findGunName(guns, id));
        const selectedImageName = this.findGunName(guns, id);
        this.setState({ selectedImageUrl, selectedImageName });
    };

    private resetStateForImage() {
        this.setState({ selectedImageUrl: '', selectedImageName: '' });
    }

    private findColor = (colors: Color[], id: number) => {
        const colorObj = colors.find(c => c.id === id);
        return colorObj ? colorObj.color : '';
    };

    private findGunName = (guns: Gun[], id: number) => {
        const gunObj = guns.find(g => g.Id === id);
        return gunObj ? gunObj.Name : '';
    };
}

type GunsChartState = {
    data: Data[];
    colors: Color[];
    legendItems: LegendItem[];
    isHovered: boolean;
    hoveredChartSection: Data;
    selectedImageUrl: string;
    selectedImageName: string;
};

type GunsChartProps = {
    guns: Gun[];
};

type Color = {
    id: number;
    color: any;
};
type Data = {
    theta: number;
    label: string;
    id: number;
    style: {
        fill: string;
        stroke: boolean;
    };
};
type LegendItem = {
    id: number;
    title: string;
    color: string;
};
export default GunsChart;
