import { Injectable } from '@angular/core';
import * as CryptoJS from 'crypto-js';

@Injectable({
  providedIn: 'root'
})
export class EncryptDecryptService {
  constructor() { }

  encrypt(value: any) {
    if (value) {
      const key = this.getKey();
      const encrypted = CryptoJS.AES.encrypt(CryptoJS.enc.Utf8.parse(value.toString()), key, this.getEncryptionKey(key));
      return encrypted.toString();
    }
    return '';
  }

  decrypt(value: string): string {
    if (value) {
      try {
        const key = this.getKey();
        const decrypted = CryptoJS.AES.decrypt(value, key, this.getEncryptionKey(key));
        return decrypted.toString(CryptoJS.enc.Utf8);
      }
      catch (err) { }
    }
    return null;
  }

  private getKey(): string {
    return CryptoJS.enc.Utf8.parse('$elt3allim^#'); // prefereed size 16 digit (128/8)
  }

  private getEncryptionKey(key: string) {
    return {
      keySize: 128 / 8,
      iv: key,
      mode: CryptoJS.mode.CBC,
      padding: CryptoJS.pad.Pkcs7
    };
  }
}
