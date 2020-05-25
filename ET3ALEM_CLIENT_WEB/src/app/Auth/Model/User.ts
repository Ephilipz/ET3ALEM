import { Role } from './UserEnums';

export class LoginUser {
    public email: String;
    public password: String;

    constructor(email: string, password: string) {
        this.email = email;
        this.password = password;
    }
}

export class RegisterUser extends LoginUser {
    public name: string;
    public role: Role

    constructor(name: string, email: string, password: string, role: Role) {
        super(email, password);
        this.name = name;
        this.role = role;
    }
}