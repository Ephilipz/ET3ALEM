export class Tokens {
  public JWT: string;
  public RefreshToken: string;
  public UserId: string;

  constructor(JWT: string, refreshToken: string) {
    this.JWT = JWT;
    this.RefreshToken = refreshToken;
  }
}
