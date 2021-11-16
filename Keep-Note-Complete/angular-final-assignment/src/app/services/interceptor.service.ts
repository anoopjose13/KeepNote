import { HttpInterceptor,HttpRequest,HttpHandler,HttpEvent } from "@angular/common/http";
import {Injectable} from "@angular/core";
import { AuthenticationService } from "./authentication.service";
import { Observable } from "rxjs/Observable";

@Injectable()
export class interceptorService implements HttpInterceptor
{
    constructor(public authService:AuthenticationService){}
    ///Used to automatically attach the authentication token to the header
    intercept(request:HttpRequest<any>,next:HttpHandler):Observable<HttpEvent<any>>{
        let headers = request.headers.set('Content-Type','application/json');
        headers = headers.set('Authorization', `Bearer ${this.authService.getBearerToken()}`);
        request=request.clone({headers:headers});
        return next.handle(request);
    }

}