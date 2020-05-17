import { Role } from './UserEnums';

export class LoginUser {
    private email: String;
    private password: String;

    constructor(email: string, password: string) {
        this.email = email;
        this.password = password;
    }
}

export class RegisterUser extends LoginUser {
    private name: string;
    private role: Role

    constructor(name: string, email: string, password: string, role: Role) {
        super(email, password);
        this.name = name;
        this.role = role;
    }
}