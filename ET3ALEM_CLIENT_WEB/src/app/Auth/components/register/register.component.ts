import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
  encapsulation: ViewEncapsulation.None
})
export class RegisterComponent implements OnInit {

  studentForm = new FormGroup({
    fullName: new FormControl('', Validators.required),
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', [Validators.required, Validators.minLength(8)]),
    code: new FormControl('', [Validators.required, Validators.maxLength(8), Validators.minLength(8), Validators.pattern(/^[a-zA-Z0-9\.]*$/)])
  });

  constructor() { }

  ngOnInit(): void {
  }

  public errorHandling(form: string, control: string, error: string) {
    return this[form].controls[control].invalid;
  }

}
