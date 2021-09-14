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

##### Getting Help

1. To get more help on the Angular CLI use `ng help`
1. `ng doc component` to look up documentation for features
1. `ng serve --help` to look up doc for `ng serve` command

## Coding Styles

Coding styles should adhere to the [Angular Style Guide](https://angular.io/docs/ts/latest/guide/style-guide.html) at all times!  The editor config setup for the project will also assist with coding style automatically, as well as VSCode settings.
