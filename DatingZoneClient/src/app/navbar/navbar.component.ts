import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
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

  constructor(
    private _accountService: AccountService,
    private _router: Router,
    private _toastr: ToastrService
  ) {}

  ngOnInit(): void {
    this.currentUser$ = this._accountService.currentUser$;
  }

  public login(): void {
    this._accountService.login(this.model).subscribe({
      next: (response) => {
        this._toastr.success('Login Success!');
        this._router.navigateByUrl('/members');
      },
      error: (err) => {
        this._toastr.error(err.error);
        console.log(err);
      },
    });
  }

  public logout(): void {
    this._accountService.logout();
    this._toastr.success('Logout Success!');
    this._router.navigateByUrl('/');
  }
}
