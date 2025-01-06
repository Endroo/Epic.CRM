import { Component, OnInit } from '@angular/core';
import { LoginDto } from './login.model';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { LoginService } from './login.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  user!: LoginDto;
  formGroup!: FormGroup;
  isLoginFailed!: boolean;
  errorMessage!: string;
  constructor(
    private formBuilder: FormBuilder,
    private accountService: LoginService) {
  }
  ngOnInit(): void {
    this.initForm();
  }

  initForm() {
    this.formGroup = this.formBuilder.group({
      username: ['', Validators.required],
      password: ['', Validators.required]
    });
  }

  submit(): void {
    if (this.formGroup.invalid) {
      return;
    }

    var username = this.formGroup.controls['username'].value;
    var password = this.formGroup.controls['password'].value;

    this.accountService.login(username, password).subscribe(
      loginUser => {
        if (loginUser) {
          this.isLoginFailed = false;
        } else {
          this.errorMessage = "User and/or password is not valid.";
          this.isLoginFailed = true;
        }
      }
    );
  }
}