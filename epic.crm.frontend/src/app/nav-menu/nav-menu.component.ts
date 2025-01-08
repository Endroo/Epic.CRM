import { Component } from '@angular/core';
import { LoginService } from '../login/login.service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.scss']
})
export class NavMenuComponent {

  constructor(private accountService: LoginService) { }

  get loginUser() {
    return LoginService.loginUser;
  }


  logout() {
    this.accountService.logout();
  }
}
