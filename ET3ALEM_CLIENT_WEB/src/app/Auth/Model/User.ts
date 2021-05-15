import { Role } from './UserEnums';

export class LoginUser {
    public Email: String;
    public Password: String;

    constructor(email: string, password: string) {
        this.Email = email;
        this.Password = password;
    }
}

export class RegisterUser extends LoginUser {
    public Name: string;

    constructor(name: string, email: string, password: string, role: Role) {
        super(email, password);
        this.Name = name;
    }
}