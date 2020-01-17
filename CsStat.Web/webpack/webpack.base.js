const webpack = require('webpack');
const path = require('path');
const { CleanWebpackPlugin } = require('clean-webpack-plugin');
const CopyWebpackPlugin = require('copy-webpack-plugin');
const { repository } = require('../package.json');
const globalVars = require('./config');

module.exports = () => ({
    devtool: 'source-map',
    entry: {
        app: './assets'
    },
    output: {
        path: path.resolve(__dirname, '../dist'),
        publicPath: '/dist/',
        filename: '[name].[chunkhash].js',
        jsonpFunction: 'webpackJsonpDelete'
    },
    resolve: {
        extensions: ['.tsx', '.ts', '.js'],
        modules: [
            'node_modules',
            // src
            path.resolve(__dirname, '../../../../'),
            path.resolve(__dirname, '../assets/')
        ]
    },
    module: {
        rules: [
            {
                test: /\.(jsx?|tsx?)$/,
                exclude: [/node_modules(?!(\/|\\)@deleteagency)/],
                loaders: ['babel-loader', 'ts-loader']
            },
            {
                test: /\.(png|svg|jpe?g|gif)$/,
                exclude: /svg[\/\\]/,
                loader: 'file-loader',
                options: {
                    name: 'images/[name].[ext]'
                }
            },
            {
                test: /\.(woff2?|eot|ttf)$/,
                loader: 'file-loader',
                options: {
                    name: 'fonts/[name].[ext]'
                }
            },
            {
                test: /\.svg$/,
                include: /svg[\/\\]/,
                use: [
                    {
                        loader: 'svg-sprite-loader',
                        options: {
                            symbolId: 'icon-[name]'
                        }
                    },
                    {
                        loader: 'svgo-loader',
                        options: {
                            plugins: [
                                { removeNonInheritableGroupAttrs: true },
                                { collapseGroups: true },
                                { removeAttrs: { attrs: '(fill|stroke)' } }
                            ]
                        }
                    }
                ]
            }
        ]
    },
    optimization: {
        splitChunks: {
            chunks: 'initial',
            automaticNameDelimiter: '.',
            name: 'vendors'
        },
        runtimeChunk: {
            name: 'vendors'
        }
    },
    plugins: [
        // set correct path to your /dist folder
        new CleanWebpackPlugin(),

        new CopyWebpackPlugin([
            { from: './assets/favicon', to: '../dist/favicon' }
        ]),

        new webpack.DefinePlugin({
            DEFINE_VIEWPORT_WIDTH_DESKTOP_WIDE: JSON.stringify(
                globalVars.viewports['desktop-wide']
            ),
            DEFINE_VIEWPORT_WIDTH_DESKTOP: JSON.stringify(
                globalVars.viewports.desktop
            ),
            DEFINE_VIEWPORT_WIDTH_TABLET: JSON.stringify(
                globalVars.viewports.tablet
            ),
            DEFINE_VIEWPORT_WIDTH_MOBILE_WIDE: JSON.stringify(
                globalVars.viewports['mobile-wide']
            ),
            DEFINE_CONTAINER_MAX_WIDTH: JSON.stringify(
                globalVars.containerMaxWidth
            ),
            DEFINE_DEFAULT_TRANSITION_DURATION: JSON.stringify(
                globalVars.defaultTransitionDuration
            ),
            DEFINE_REPOSITORY: JSON.stringify(repository)
        })
    ],
    stats: {
        children: false
    }
});
