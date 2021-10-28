const imgBBApiKey = 'c02e42eab2b387576962d8e41ebd1747';
const imgurClientId = 'c5aa8cf41def8fc';
const imgurClientSecret = '34b17ac1827cbcf542a00f33db8568734929b61f';

export const environment = {
  production: true,
  baseUrl: 'https://et3allim.com',
  imgBBKey : imgBBApiKey,
  postSaveRteImage: `https://api.imgbb.com/1/upload?key=${imgBBApiKey}`,
  imgurClientId,
  imgurClientSecret,
  postUploadImgurImage: `https://api.imgur.com/3/image`,
};
