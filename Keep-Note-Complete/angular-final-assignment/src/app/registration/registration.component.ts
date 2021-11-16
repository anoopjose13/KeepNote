import { Component, OnInit } from '@angular/core';
import { Register } from '../register';
import { FormControl, Validators } from '@angular/forms';
import { AuthenticationService } from '../services/authentication.service';
import { RouterService } from '../services/router.service';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit {
  registration:Register;
  submitMessage: any;
  username = new FormControl('', [Validators.required]);
  password = new FormControl('', [Validators.required]);
  firstname = new FormControl('', [Validators.required]);
  lastname = new FormControl('', [Validators.required]);
  role = new FormControl('', [Validators.required]);

  constructor(private authService: AuthenticationService, private routeService: RouterService) { }

  ngOnInit() {
    
  }
  registerSubmit() {
    if (this.username.valid && this.password.valid
      && this.firstname.valid && this.lastname.valid
      && this.role)
    {
    this.registration= new Register();
    this.registration.userId = this.username.value;
    this.registration.password = this.password.value;
    this.registration.firstname = this.firstname.value;
    this.registration.lastname = this.lastname.value;
    this.registration.role = this.role.value;
    }
    
    this.authService.registerUser(this.registration).subscribe(
      response => {
        if(response)
        {
          this.routeService.routeToLogin();
        }
        else
        {
          this.routeService.routeToRegistration()
          this.submitMessage='Registration unsuccessfull';
        }

      },
      err => {
        this.submitMessage = (err.status === 403) ? err.error.message : err.message ;
      }
    );
  }
  Login(){
    this.routeService.routeToLogin();
  }
  getUserNameErrorMessage() {
    return this.username.hasError('required') ? 'You must enter a Username' : '';
  }
  getPasswordErrorMessage() {
    return this.password.hasError('required') ? 'You must enter a password' : '';
  }
  getFirstnameErrorMessage() {
    return this.firstname.hasError('required') ? 'You must enter a firstname' : '';
  }
  getLastnameErrorMessage() {
    return this.lastname.hasError('required') ? 'You must enter a last name' : '';
  }
  getRoleErrorMessage() {
    return this.role.hasError('required') ? 'You must enter a role' : '';
  }
  
}
