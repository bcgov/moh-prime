# PRIME

## Table of Contents

[TOC]

## Installation and Configuration

The installation and configuration of the PRIME development environment is sequentially ordered to ensure software dependencies are available when needed during setup.

### Installation

The following list includes the required software needed to run the application, as well as, the suggested IDE with extensions for web client development, and software for source control management and API development/testing.

#### Git and GitKraken

[Download](https://git-scm.com/downloads) and install the Git version control system, and optionally [download](https://www.gitkraken.com) and install the free GitKraken Git GUI client.

Clone the PRIME repository into a project directory GitKraken or the terminal by typing:

```bash
git clone https://github.com/bcgov/moh-prime
```

#### Node

[Download](https://nodejs.org/en/) and install **Node v12.x**

#### VS Code

[Download](https://code.visualstudio.com/) and install VSCode and accept the prompt to install the recommended extensions when the PRIME repository is initially opened in VSCode.

#### PostMan

[Download](https://www.getpostman.com/apps) and install the Postman HTTP client.

## Building and Running

### Dependancies

Load project dependancies and dev dependancies and cli tools

Check the version of @angular/cli used in the package.json and use the version found there below

```bash
npm install -g @angular/cli@12.2.17
```

```bash
npm install -g yarn
```

```bash
yarn install
```

### Client

To build, run, and open the Angular application in the default browser at http://localhost:4200 for development go to the PRIME project repository in the terminal and type:

```bash
ng serve -o
```

To test the production build locally before pushing features to the repository for deployment type:

```bash
ng build --configuration=production
```

#### Angular CLI Reference

This project was generated with [Angular CLI](https://github.com/angular/angular-cli) version 8.3.x. Refer to the Angular CLI documentation for the available commands, but the most used commands during development will be:

1. `ng serve -o` to serve your application locally in memory during development at `http://localhost:4200` through the default browser, which watches for changes, recompiles, and automatically refresh the application in the browser
1. `ng build` to build the application, which is stored in `/dist` directory.  Use the `--configuration=production` flag for a production build, which significantly decreases the size of the application
1. `ng g <blueprint>` to create code scaffolding for a directive, pipe, service, class, guard, interface, enum, and module
1. `ng lint` to lint the application code using TSLint.
1. `ng test` to execute the unit tests via [Karma](https://karma-runner.github.io). The test runner has a variety of options that can be used to refine how the test suite is executed that can be found by running `ng test --help`.  One of these options allows for the narrowing of the tests run through the use of globbing patterns - `ng test --include='**/*.pipe.spec.ts'`.
1. `ng e2e` to execute the end-to-end tests via [Protractor](http://www.protractortest.org/).
1. `npm test` will run the tests the same way they work on github actions.


##### Unit tests issues

Sometimes unit tests will start to fail either consistently or intermitently. Intermitenet unit tests that are not considered (non-deterministic) can be troublesome and hard to detect.

 Below are some steps to be able to find out where the issues are

 1- Go to karma.conf.js and uncomment the jasmine client that sets the order of execution to non-random
 2- Run the tests and see if the failure occurs consitently and take note of the failing test number or where the errors occur
 3- Investigate the tests that are failing as well as the tests before the ones that are failing.
 4- If tests fail with routing issues and you can't find the test file name in the logs, keep note of the route that is failing and investigate the spec files of the components that try to call those routes

 Sometimes running tests in order may note reproduce the error. In that case you can set the random flag to true and play with the seed until you find one that reproduces the error and stick with it. Alternatively, set the random flag to true and comment out the seed for now. You can use a tool that logs out the seed, and when a random run fails get the seed and use it by setting `jasmin: { random: true, seed: (the seed you found)}`

##### Getting Help

1. To get more help on the Angular CLI use `ng help`
1. `ng doc component` to look up documentation for features
1. `ng serve --help` to look up doc for `ng serve` command

## Coding Styles

Coding styles should adhere to the [Angular Style Guide](https://angular.io/docs/ts/latest/guide/style-guide.html) at all times!  The editor config setup for the project will also assist with coding style automatically, as well as VSCode settings.
