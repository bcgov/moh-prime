/**
 * @description
 * Use a single page for the entire demo, or redirect after login to
 * a secondary page.
 */
const useMultiplePages = false;

/**
 * @description
 * Redirect URI after authentication.
 */
const redirectUri = (useMultiplePages)
  ? 'http://localhost:8080/redirect.html'
  : 'http://localhost:8080';

/**
 * @description
 * API endpoint for accessing PRIME.
 */
const gpidUri = 'https://pr-611.pharmanetenrolment.gov.bc.ca/api/v1/provisioner-access/gpid';

/**
 * @description
 * Global instance of Keycloak.
 */
let keycloak;

/**
 * @description
 * Immediately invoke Keycloak initialization, and prevent Keycloak from
 * being initialized mulitiple times on a single page.
 */
const keycloakInit = (() => {
  keycloak = new Keycloak({
    url: 'https://sso-dev.pathfinder.gov.bc.ca/auth',
    realm: 'v4mbqqas',
    clientId: 'prime-pos-gpid'
  });

  return keycloak.init()
    .then((authenticated) => {
      console.log('Keycloak Initialized');

      // Setup the page to demonstrate use of a single page, or
      // alternatively redirect to a secondary page
      document.getElementById('action').innerHTML = (_isInitializedAndAuthenticated())
        ? 'Request GPID'
        : 'Login';

      return authenticated;
    })
    .catch(() => Promise.reject(new Error('Failed Keycloak Initialization!')));
})();

/**
 * @description
 * Attempt to get a GPID from PRIME.
 */
function getGpid() {
  return _getToken()
    .then(_requestGpid)
    .then((gpid) => {
      console.log('GPID', gpid);
      document.getElementById('gpid').innerHTML = gpid;
    })
    .catch((error) => console.error(error.message));
}

/**
 * @description
 * Helper to check the initialization of the Keycloak instance.
 */
function _initialize() {
  return keycloakInit
    .catch(() => Promise.reject(new Error('Failed Keycloak Initialization!')));
}

/**
 * @description
 * Helper to get the authentication token.
 */
function _getToken() {
  if (_isInitializedAndAuthenticated()) {
    return Promise.resolve(keycloak.idToken);
  }

  return _initialize()
    .then((authenticated) =>
      (!authenticated)
        ? keycloak.login({ redirectUri, idpHint: 'bcsc' })
        : keycloak.idToken // Token
    )
    .catch(() => Promise.reject(new Error('Failed Authentication!')));
}

/**
 * @description
 * Helper to request the GPID from PRIME.
 */
function _requestGpid(token) {
  if (!keycloak.authenticated) {
    throw new Error('Not Authenticated');
  }

  const init = {
    method: 'GET',
    headers: new Headers({
      withCredentials: true,
      credentials: 'include',
      Authorization: `Bearer ${token}`,
      'Content-Type': 'application/json'
    })
  };
  return fetch(gpidUri, init)
    .then((response) => {
      if (!response.ok) {
        throw new Error('GPID could not be retrieved');
      }
      return response.json();
    })
    .then((body) => body.result) // GPID
    .catch(() => Promise.reject(new Error('Request for GPID Failed!')));
}

/**
 * @description
 * Helper to indicate Keycloak has been initialized, and the user has
 * authenticated with PRIME.
 */
function _isInitializedAndAuthenticated() {
  return (keycloak.hasOwnProperty('authenticated') && keycloak.authenticated);
}
