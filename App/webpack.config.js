const path = require("path");
const MiniCssExtractPlugin = require("mini-css-extract-plugin");

module.exports = {
  mode: "development",
  entry: "./src/index.ts",
  module: {
    rules: [
      {
        test: /\.ts$/i,
        use: "ts-loader"
      },
      {
        test: /\.scss$/i,
        use: [
          MiniCssExtractPlugin.loader,
          "css-loader",
          "sass-loader",
        ],
      },
    ]
  },
  output: {
    path: path.resolve(__dirname, "dist"),
    filename: "index.min.js",
  },
  plugins: [
    new MiniCssExtractPlugin({
      filename: "index.min.css"
    })
  ]
};