const path = require('path');
const { VueLoaderPlugin } = require('vue-loader');

module.exports = {
    entry: './src/main.js', // Vue giriş noktası
    output: {
        path: path.resolve(__dirname, 'dist'),
        filename: 'bundle.js',
        publicPath: '/dist/'
    },
    module: {
        rules: [
            { test: /\.vue$/, loader: 'vue-loader' },
            { test: /\.js$/, exclude: /node_modules/, loader: 'babel-loader' },
            { test: /\.css$/, use: ['vue-style-loader', 'css-loader'] }
        ]
    },
    resolve: {
        extensions: ['.js', '.vue', '.json'],
        alias: {
            vue$: 'vue/dist/vue.esm-bundler.js'
        }
    },
    plugins: [new VueLoaderPlugin()],
    devServer: {
        contentBase: path.join(__dirname, 'wwwroot'),
        publicPath: '/dist/',
        hot: true,
        port: 8080
    },
    mode: 'development'
};
