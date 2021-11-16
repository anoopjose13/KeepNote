import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import * as jwt from 'jsonwebtoken';
import { Register } from '../register';
//import * as jwt_decode from 'jwt-decode';
@Injectable()
export class AuthenticationService { private basic_url = 'https://localhost:44372/auth/';
contentType:string;
constructor(private httpClient: HttpClient) {}

authenticateUser(data) {
  // to check the user authentication
   const httpOptions = {
    headers: new HttpHeaders({
      'Content-Type':  'application/json',
      'Access-Control-Allow-Origin' :'*',
      'Access-Control-Allow-Methods': "GET,POST,OPTIONS,DELETE,PUT"
    })
  };
  return this.httpClient.post(`${this.basic_url}login/`, data,httpOptions)
}
registerUser(register) {
  const httpOptions = {
    headers: new HttpHeaders({
      'Content-Type':  'application/json',
      'Access-Control-Allow-Origin' :'*',
      'Access-Control-Allow-Methods': "GET,POST,OPTIONS,DELETE,PUT"
    })
  };
  return this.httpClient.post(`${this.basic_url}register/`,register,httpOptions)
     
 
}
setBearerToken(token) {
  // set the token to localstorage
  localStorage.setItem('bearerToken', token);
}

getBearerToken() {
  // get the  token from localstorage
  return localStorage.getItem('bearerToken');
}
setUserId(userId)
{
  localStorage.setItem('userId', userId);
}
getUserId()
{
  return localStorage.getItem('userId');
}
isUserAuthenticated(token): boolean {
   if(!token) token = token;
    if(!token) return true;

    const date = this.getTokenExpirationDate(token);
    if(date === undefined) return false;
    return !(new Date().valueOf() > date.valueOf());
  
}
getTokenExpirationDate(token: string): Date {
  var base64Url = token.split('.')[1];
  var base64 = base64Url.replace('-', '+').replace('_', '/');
  const decoded =  JSON.parse(window.atob(base64));//jwt_decode(token);

  if (decoded.exp === undefined) return null;

  const date = new Date(0); 
  date.setUTCSeconds(decoded.exp);
  return date;
}
}
