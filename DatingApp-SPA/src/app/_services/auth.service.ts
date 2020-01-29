import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { logging } from 'protractor';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  baseURL = 'http://localhost:56718/api/auth/';
constructor(private http: HttpClient) { }

  login(model: any) {
      return this.http.post(this.baseURL + 'login', model )
      .pipe(
        map((response: any ) => {
          const user = response;
          if(user != null) {
            localStorage.setItem('token', user.token );
          }
        })
      );
  }

  register(model: any){
    return this.http.post(this.baseURL + 'register' , model);
  }
}
