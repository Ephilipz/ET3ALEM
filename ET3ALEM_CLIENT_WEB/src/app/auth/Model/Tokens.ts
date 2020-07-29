export class Tokens {
    public JWT: string;
    public RefreshToken: string;

    constructor(JWT: string, refreshToken: string) {
        this.JWT = JWT;
        this.RefreshToken = refreshToken;
    }
}