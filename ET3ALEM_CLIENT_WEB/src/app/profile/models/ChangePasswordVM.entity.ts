export class ChangePasswordVM{
    OldPassword: string;
    NewPassword: string;

    constructor(oldPassword: string, newPassword: string){
        this.OldPassword = oldPassword;
        this.NewPassword = newPassword;
    }
}