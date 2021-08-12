// This file can be replaced during build by using the `fileReplacements` array.
// `ng build --prod` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.

const imgBBApiKey = 'c02e42eab2b387576962d8e41ebd1747';
const imgurClientId = 'c5aa8cf41def8fc';
const imgurClientSecret = '34b17ac1827cbcf542a00f33db8568734929b61f';

export const environment = {
  production: false,
  baseUrl: 'http://localhost:5000',
  imgBBKey : imgBBApiKey,
  postSaveRteImage: `https://api.imgbb.com/1/upload?key=${imgBBApiKey}`,
  imgurClientId,
  imgurClientSecret,
  postUploadImgurImage: `https://api.imgur.com/3/image`,
};
