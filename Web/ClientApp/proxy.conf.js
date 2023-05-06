const { env } = require('process');

const target = env.ASPNETCORE_HTTPS_PORT ? `https://localhost:${env.ASPNETCORE_HTTPS_PORT}` :
  env.ASPNETCORE_URLS ? env.ASPNETCORE_URLS.split(';')[0] : 'http://localhost:30641';

const PROXY_CONFIG = [
  {
    context: ["/company", "/api/company"],
    target: target,
    secure: false,
    headers: {
      Connection: 'Keep-Alive'
    }
  },
  {
    context: [
      "/company/GetByTaxIdentifier/:id",
      "/api/company/GetByTaxIdentifier/"
    ],
    target: target,
    secure: false,
    headers: {
      Connection: 'Keep-Alive'
    },
    pathRewrite: {
      '^/company/GetByTaxIdentifier/([0-9]+)$': '/api/company/GetByTaxIdentifier/$1'
    }
  }
];

module.exports = PROXY_CONFIG;
