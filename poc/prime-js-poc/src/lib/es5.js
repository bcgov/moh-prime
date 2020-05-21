'use strict';

/**
 * @description
 * Use a single page for the entire demo, or redirect after login to
 * a secondary page.
 */
var useMultiplePages = false;

/**
 * @description
 * Redirect URI after authentication.
 */
var redirectUri = (useMultiplePages)
  ? 'http://localhost:8080/redirect.html'
  : 'http://localhost:8080';

/**
 * @description
 * API endpoint for accessing PRIME.
 */
var gpidUri = 'https://pr-611.pharmanetenrolment.gov.bc.ca/api/v1/provisioner-access/gpid';

/**
 * @description
 * Global instance of Keycloak.
 */
var keycloak;

/**
 * @description
 * Immediately invoke Keycloak initialization, and prevent Keycloak from
 * being initialized mulitiple times on a single page.
 */
var keycloakInit = function () {
  keycloak = new Keycloak({
    url: 'https://sso-dev.pathfinder.gov.bc.ca/auth',
    realm: 'v4mbqqas',
    clientId: 'prime-pos-gpid'
  });

  return keycloak.init()
    .then(function (authenticated) {
      console.log('Keycloak Initialized');

      // Setup the page to demonstrate use of a single page, or
      // alternatively redirect to a secondary page
      document.getElementById('action').innerHTML = isInitializedAndAuthenticated()
        ? 'Request GPID'
        : 'Login';

      return authenticated;
    })
    .catch(function () {
      return Promise.reject(new Error('Failed Keycloak Initialization!'));
    });
}();

/**
 * @description
 * Attempt to get a GPID from PRIME.
 */
function getGpid() {
  return _getToken()
    .then(_requestGpid)
    .then(function (gpid) {
      console.log('GPID', gpid);
      document.getElementById('gpid').innerHTML = gpid;
    })
    .catch(function (error) {
      return console.error(error.message);
    });
}

/**
 * @description
 * Helper to initialize the instance of Keycloak.
 */
function _initialize() {
  return keycloakInit
    .then(function (authenticated) {
      console.log('Keycloak Initialized');
      return authenticated;
    })
    .catch(function () {
      return Promise.reject(new Error('Failed Keycloak Initialization!'));
    });
}

/**
 * @description
 * Helper to get the authentication token.
 */
function _getToken() {
  if (isInitializedAndAuthenticated()) {
    return Promise.resolve(keycloak.idToken);
  }

  return _initialize()
    .then(function (authenticated) {
      return !authenticated
        ? keycloak.login({
          redirectUri: redirectUri,
          idpHint: 'bcsc'
        })
        : keycloak.idToken; // Token
    })
    .catch(function () {
      return Promise.reject(new Error('Failed Authentication!'));
    });
}

/**
 * @description
 * Helper to request the GPID from PRIME.
 */
function _requestGpid(token) {
  if (!keycloak.authenticated) {
    throw new Error('Not Authenticated');
  }

  var init = {
    method: 'GET',
    headers: new Headers({
      withCredentials: true,
      credentials: 'include',
      Authorization: "Bearer ".concat(token),
      'Content-Type': 'application/json'
    })
  };
  return fetch(gpidUri, init)
    .then(function (response) {
      if (!response.ok) {
        throw new Error('GPID could not be retrieved');
      }

      return response.json();
    })
    .then(function (body) {
      return body.result; // GPID
    })
    .catch(function () {
      return Promise.reject(new Error('Request for GPID Failed!'));
    });
}

/**
 * @description
 * Helper to indicate Keycloak has been initialized, and the user has
 * authenticated with PRIME.
 */
function isInitializedAndAuthenticated() {
  return keycloak.hasOwnProperty('authenticated') && keycloak.authenticated;
}
