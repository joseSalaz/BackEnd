import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.scss']
})
export class SignUpComponent implements OnInit {

  formCrea: FormGroup;
  showPassword: boolean = false;

  constructor(private fb: FormBuilder) { 
    this.formCrea = this.fb.group({
      Nombre: ['', Validators.required],
      ApellidoPaterno: ['', Validators.required],
      ApellidoMaterno: ['', Validators.required],
      Correo: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required]
    });
  }

  ngOnInit(): void {}

  signUp(): void {
    if (this.formCrea.invalid) return;
    console.log(this.formCrea.value);
  }

  toggleShowPassword(): void {
    this.showPassword = !this.showPassword;
  }
}
