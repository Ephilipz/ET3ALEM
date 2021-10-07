
export class LoginUser {
    public Email: String;
    public Password: String;

    constructor(email: string, password: string) {
        this.Email = email;
        this.Password = password;
    }
}

export class RegisterUser extends LoginUser {
    public FullName: string;

    constructor(name: string, email: string, password: string) {
        super(email, password);
        this.FullName = name;
    }
}
