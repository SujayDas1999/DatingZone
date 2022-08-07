import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from '../models/User.model';
import { AccountService } from '../services/account.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css'],
})
export class NavbarComponent implements OnInit {
  model: any = {};

  public currentUser$: Observable<User>;

  constructor(private _accountService: AccountService) {}

  ngOnInit(): void {
    this.currentUser$ = this._accountService.currentUser$;
  }

  public login(): void {
    this._accountService.login(this.model).subscribe({
      next: (response) => {
        console.log(response);
      },
      error: (err) => console.log(err),
    });
  }

  public logout(): void {
    this._accountService.logout();
  }
}
