import { Component, OnInit } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { ExtraFormOptions } from 'src/app/Shared/Classes/forms/ExtraFormOptions';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-password-recover',
  templateUrl: './password-recover.component.html',
  styleUrls: ['./password-recover.component.css']
})
export class PasswordRecoverComponent extends ExtraFormOptions implements OnInit {

  constructor(private AuthService : AuthService) { 
    super();
  }

  email = new FormControl('', [Validators.email, Validators.required]);

  ngOnInit(): void {
  }

  sendRecoveryMail(){
    
  }

}
