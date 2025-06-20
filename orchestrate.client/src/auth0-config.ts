// src/auth0-config.ts

const auth0Config = {
  domain: 'dev-kwxbfmcbpwd0gdjb.us.auth0.com',
  clientId: 'abPJbKPjZkWdGYBANkCQRgzhapfxMw2h',
  redirectUri: window.location.origin,
  audience: 'https://api.orchestrate_dev.local', // auth0.com > Application > api > find key
}

export default auth0Config