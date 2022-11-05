/* eslint-disable no-console */
/* eslint-disable import/no-extraneous-dependencies */
const webpack = require('webpack');
const dotenv = require('dotenv');
const config = require('../webpack.prod');

dotenv.config();
process.env.NODE_ENV = 'production';

console.log('Generating minified bundle. This will take a moment...');

webpack(config).run((error, stats) => {
  if (error) {
    // so a fatal error occurred. Stop here.
    console.error(error);
    return 1;
  }

  const jsonStats = stats.toJson();

  if (jsonStats.errors.length > 0) {
    return jsonStats.errors.map(({ message }) => console.log(message));
  }

  if (jsonStats.warnings.length > 0) {
    console.log('Webpack generated the following warnings: ');
    jsonStats.warnings.map(({ message }) => console.log(message));
  }

  // if we got this far, the build succeeded.
  console.log("Your app is compiled in production mode in /dist. It's ready to roll!");

  return 0;
});
