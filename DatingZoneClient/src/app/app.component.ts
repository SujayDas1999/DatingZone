import { Component, OnInit } from '@angular/core';
import { User } from './models/User.model';
import { AccountService } from './services/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent implements OnInit {
  title = 'DatingZone';
  public users: any;

  constructor(private _accountService: AccountService) {}

  ngOnInit() {
    this.setCurrentUser();
  }

  public setCurrentUser() {
    const user: User = JSON.parse(localStorage.getItem('user'));
    this._accountService.setCurrentUser(user);
  }
}
