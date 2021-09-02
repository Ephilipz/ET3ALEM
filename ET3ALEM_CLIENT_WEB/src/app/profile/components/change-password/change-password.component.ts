import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { ExtraFormOptions } from 'src/app/Shared/Classes/forms/ExtraFormOptions';
import { ChangePasswordVM } from '../../models/ChangePasswordVM.entity';
import { ProfileService } from '../../services/profile.service';

@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html',
  styleUrls: ['./change-password.component.css']
})
export class ChangePasswordComponent extends ExtraFormOptions implements OnInit {

  changePasswordFG = new FormGroup({
    oldPassword : new FormControl('', [Validators.required, Validators.minLength(8)]),
    newPassword : new FormControl('', [Validators.required, Validators.minLength(8)]),
  })

  constructor(private toast: ToastrService,
    private profileService: ProfileService) {
    super();
  }

  ngOnInit(): void {
  }

  onSubmit(){
    const oldPassword = this.changePasswordFG.get('oldPassword').value;
    const newPassword = this.changePasswordFG.get('newPassword').value;
    const changePasswordVM = new ChangePasswordVM(oldPassword, newPassword);
    this.profileService.changePassword(changePasswordVM).subscribe(
      (success) => {
        this.toast.success('Password updated successfully');
      },
      (error) => {
        this.toast.error('Error updating the password, make sure the old password is correct')
      }
    )
  }

}
