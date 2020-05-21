# PRIME

## Description

The demo shows how to authenticate with Keycloak and request a GPID. It was written in ES6 which supports modern browsers Chrome, Firefox, Edge, Opera, Safari, and was converted to ES5 using Babel to support older browser like Internet Explorer 11 (IE11). Supporting IE11 is not suggested due to known security issues within the browser.

The demo is setup to redirect back to the originating page after authentication, but can be quickly configured to redirect to a secondary page by setting `useMultiplePages` in the appropriate script file.

## Example

* Run `npm install` in the root directory to install the dependencies
* Run `npm run serve` in the root directory to start up the local HTTP server
* Visit `http://localhost:8080` to start the demo
