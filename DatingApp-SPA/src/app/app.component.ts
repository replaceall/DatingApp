import { Component, OnInit } from '@angular/core';
import { AuthService } from './_services/auth.service';
import { JwtHelperService } from '@auth0/angular-jwt';
import { User } from './models/User';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  jwtHelper = new JwtHelperService();
  constructor(private authSerice: AuthService) {}

  ngOnInit() {
    const token = localStorage.getItem('token');
    const user: User = JSON.parse(localStorage.getItem('user'));
    if (token) {
      this.authSerice.decodedToken = this.jwtHelper.decodeToken(token);
    }
    if (user) {
      this.authSerice.currentUser = user;
      this.authSerice.chnageMemberPhoto(user.photoUrl);
    }
  }
}
