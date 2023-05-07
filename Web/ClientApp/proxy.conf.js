const { env } = require('process');

const target = env.ASPNETCORE_HTTPS_PORT ? `https://localhost:${env.ASPNETCORE_HTTPS_PORT}` :
  env.ASPNETCORE_URLS ? env.ASPNETCORE_URLS.split(';')[0] : 'http://localhost:30641';

const PROXY_CONFIG = [
  {
    context: [
      "/company",
      "/company/:id",
      "/api/company",
      "/api/company/:id"
    ],
    target: target,
    secure: false,
    headers: {
      Connection: 'Keep-Alive'
    },
    pathRewrite: {
      '^/company/([0-9]+)$': '/api/company/$1'
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
  },
  {
    context: [
      "/supplier",
      "/supplier/:id",
      "/api/supplier",
      "/api/supplier/:id"
    ],
    target: target,
    secure: false,
    headers: {
      Connection: 'Keep-Alive'
    },
    pathRewrite: {
      '^/supplier/([0-9]+)$': '/api/supplier/$1'
    }
  },
  {
    context: [
      "/supplier/GetByTaxIdentifier/:id",
      "/api/supplier/GetByTaxIdentifier/"
    ],
    target: target,
    secure: false,
    headers: {
      Connection: 'Keep-Alive'
    },
    pathRewrite: {
      '^/supplier/GetByTaxIdentifier/([0-9]+)$': '/api/supplier/GetByTaxIdentifier/$1'
    }
  },
];

module.exports = PROXY_CONFIG;
