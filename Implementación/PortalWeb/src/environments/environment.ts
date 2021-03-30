// This file can be replaced during build by using the `fileReplacements` array.
// `ng build --prod` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.

var production = true;
var nameEnvironment = 'dev';
var apiLogin = '';
var keyEncript = "";
var timeInactividad = 900000

switch (nameEnvironment) {
  case 'local':
    apiLogin = "https://gcsbog-dev01:93/api/";
    keyEncript = "U2NhcmUyMDIwKg==";
    
    break;
  case 'dev':
    apiLogin = "https://gcsbog-dev01:93/api/";
    keyEncript = "U2NhcmUyMDIwKg==";
    
    break;
  case 'pro':
    apiLogin = "https://autenticacion.scare.org.co/api/";
    keyEncript = "U2NhcmUyMDIwKg==";
    break;
}

export const environment = {
  production: production,
  apiLogin: apiLogin,
  keyEncript: keyEncript,
  timeInactividad: timeInactividad
};