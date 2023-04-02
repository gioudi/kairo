const path = require('path');
const webpack = require('webpack');
const { CleanWebpackPlugin } = require("clean-webpack-plugin");
const MiniCssExtractPlugin = require("mini-css-extract-plugin");

module.exports = {
    //target: "web",
    entry: {
        app: './Scripts/App.ts',
        usuarios: './Scripts/Pages/Usuarios/Usuarios.ts',
        cabezotes: './Scripts/Pages/Cabezotes/Cabezotes.ts',
        trailers: './Scripts/Pages/Trailers/Trailers.ts',
        conductores: './Scripts/Pages/Conductores/Conductores.ts',
        areas: './Scripts/Pages/Areas/Areas.ts',
        log: './Scripts/Pages/Log/Log.ts',
        roles: './Scripts/Pages/Roles/Roles.ts',
        terminales: './Scripts/Pages/Terminales/Terminales.ts',
        tablasSistema: './Scripts/Pages/TablasCorreccion/TablasSistema.ts',
        tanques: './Scripts/Pages/Tanques/Tanques.ts',
        contadores: './Scripts/Pages/Contadores/Contadores.ts',
        lineas: './Scripts/Pages/Lineas/Lineas.ts',
        compañias: './Scripts/Pages/Compañias/Compañias.ts',
        proveedores: './Scripts/Pages/Proveedores/Proveedores.ts',
        mapeoArchivos: './Scripts/Pages/ProcesamientoArchivos/MapeoArchivos.ts',
        productos: './Scripts/Pages/Productos/Productos.ts',
        despachos: './Scripts/Pages/Despachos/Despachos.ts',
        graficos: './Scripts/Pages/Graficos/Graficos.ts'
    },
    mode: 'production',
    optimization: {
        minimize: true,
        splitChunks: {
            cacheGroups: {
                vendor: {
                    //test: /[\\/]node_modules[\\/](?!turbolinks)(.[a-zA-Z0-9.\-_]+)[\\/]/,
                    test: /[\\/]node_modules[\\/]/,
                    name: 'packages',
                    chunks: 'all',
                }
            }
        }
    },
    module: {
        rules: [
            {
                test: /\.tsx?$/,
                use: 'ts-loader',
                exclude: [/node_modules/, /typings/]
            },
            {
                test: /\.css$/,
                use: [MiniCssExtractPlugin.loader, "css-loader"]
            }
        ]
    },
    resolve: {
        extensions: ['.tsx', '.ts', '.js']
    },
    output: {
        filename: 'js/pages/[name].js',
        path: path.resolve(__dirname, 'wwwroot/dist/'),
        library: 'kairosv2',
        libraryTarget: 'umd'
    },
    devtool: false,
    plugins: [
        new CleanWebpackPlugin(),
        new webpack.SourceMapDevToolPlugin({
            filename: "[file].map",
            fallbackModuleFilenameTemplate: '[absolute-resource-path]',
            moduleFilenameTemplate: '[absolute-resource-path]'
        }),
        new MiniCssExtractPlugin({
            filename: "css/[name].css"
        }),
        new webpack.ProvidePlugin({
            $: 'jquery',
            jQuery: 'jquery',
            'window.jQuery': 'jquery',
            'window.$': 'jquery'
        })
    ]
};  