import { Injectable } from '@angular/core';
import { EncryptDecryptService } from './encrypt-decrypt.service';

@Injectable({
  providedIn: 'root'
})
export class LocalstorgeService {
  constructor(private encryptdecryptservice: EncryptDecryptService) {

  }
  private _userId: string = '_userId';
  private _jwtToken: string = '_jwtToken';
  private _refreshToken: string = '_refreshToken';
  public get UserId(): string {
    return this.getItem(this._userId);
  }

  public set UserId(value: string) {
    this.setItem(this._userId, value);
  }

  public get JWT(): string {
    return this.getItem(this._jwtToken);
  }

  public set JWT(value: string) {
    this.setItem(this._jwtToken, value);
  }
  public get RefreshToken(): string {
    return this.getItem(this._refreshToken);
  }

  public set RefreshToken(value: string) {
    this.setItem(this._refreshToken, value);
  }

  public clear() {
    localStorage.clear();
  }

  private getItem(key: string): string {
    return this.encryptdecryptservice.decrypt(localStorage.getItem(this.encryptdecryptservice.encrypt(key)));
  }
  private setItem(key: string, value: any) {
    localStorage.setItem(this.encryptdecryptservice.encrypt(key), this.encryptdecryptservice.encrypt(value));
  }

}
