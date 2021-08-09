// Karma configuration file, see link for more information
// https://karma-runner.github.io/1.0/config/configuration-file.html

module.exports = function (config) {
  config.set({
    basePath: '',
    frameworks: ['jasmine', '@angular-devkit/build-angular'],
    plugins: [
      require('karma-jasmine'),
      require('karma-chrome-launcher'),
      require('karma-jasmine-html-reporter'),
      require('karma-coverage-istanbul-reporter'),
      require('@angular-devkit/build-angular/plugins/karma'),
    ],
    client: {
      // leave Jasmine Spec Runner output visible in browser
      clearContext: false,
    },
    coverageIstanbulReporter: {
      dir: require('path').join(__dirname, './coverage/angular-frontend'),
      reports: ['html', 'lcovonly', 'text-summary'],
      fixWebpackSourcePaths: true
    },
    reporters: ['progress', 'kjhtml'],
    port: 9876,
    colors: true,
    logLevel: config.LOG_INFO,
    autoWatch: true,
    browsers: ['Chrome'],
    customLaunchers: {
      ChromeHeadlessNoSandbox: {
        base: 'ChromeHeadless',
        flags: [
          // required to run without privileges in docker
          '--no-sandbox',
          '--user-data-dir=/tmp/chrome-test-profile',
          '--disable-web-security',
          '--disable-gpu',
          '--disable-background-networking',
          '--disable-default-apps',
          '--disable-extensions',
          '--disable-sync',
          '--disable-translate',
          '--headless',
          '--hide-scrollbars',
          '--metrics-recording-only',
          '--mute-audio',
          '--no-first-run',
          '--safebrowsing-disable-auto-update',
          '--ignore-certificate-errors',
          '--ignore-ssl-errors',
          '--ignore-certificate-errors-spki-list',
          '--remote-debugging-port=9222',
          '--remote-debugging-address=0.0.0.0',
          '--disable-dev-shm-usage',
          '--disable-setuid-sandbox',
          '--disable-namespace-sandbox',
          '--window-size=800x600',
        ],
      },
    },
    // browserDisconnectTimeout: 10000,
    // browserDisconnectTolerance: 3,
    // browserNoActivityTimeout: 60000,
    singleRun: false,
    restartOnFileChange: true,
  });
};
