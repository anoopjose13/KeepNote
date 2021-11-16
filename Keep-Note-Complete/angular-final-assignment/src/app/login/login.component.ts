import { Component } from '@angular/core';
import { RouterService } from './../services/router.service';
import { FormControl, Validators } from '@angular/forms';
import { AuthenticationService } from '../services/authentication.service';
import { User } from '../User';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})

export class LoginComponent {
  submitMessage: any;
  username = new FormControl('', [Validators.required]);
  password = new FormControl('', [Validators.required]);

  constructor(private authService: AuthenticationService, private routerService: RouterService) {}
  loginSubmit() {
    if (this.username.valid && this.password.valid) {
      this.authService.authenticateUser({'userId': this.username.value, 'password': this.password.value}).subscribe(
        response => {
          this.authService.setBearerToken(response['token']);
          this.authService.setUserId(this.username.value);
          this.routerService.routeToDashboard();
        },
        err => {
          this.submitMessage = (err.status === 403) ? err.error.message : err.message ;
        }
      );
    }
  }
  getUserNameErrorMessage() {
    return this.username.hasError('required') ? 'You must enter a Username' : '';
  }
  getPasswordErrorMessage() {
    return this.password.hasError('required') ? 'You must enter a password' : '';
  }
}
