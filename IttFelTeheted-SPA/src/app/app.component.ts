import { Component, OnInit } from '@angular/core';
import { AuthService } from './_services/auth.service';
import { JwtHelperService } from '@auth0/angular-jwt';
import { TimeagoIntl } from 'ngx-timeago';
import {strings as hunStrings} from 'ngx-timeago/language-strings/hu';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  jwtHelper = new JwtHelperService();
  title = 'ittFelteheted.hu';

  constructor(private authService: AuthService, intl: TimeagoIntl) {
    intl.strings = hunStrings;
    intl.changes.next();
  }

  ngOnInit() {
    const token = localStorage.getItem('token');
    if (token) {
      this.authService.decodedToken = this.jwtHelper.decodeToken(token);
    }
  }
}
