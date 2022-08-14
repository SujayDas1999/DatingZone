import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

import { AccountService } from '../services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
})
export class RegisterComponent implements OnInit {
  public model: any = {};
  @Output() cancelRegister = new EventEmitter<boolean>();
  constructor(
    private _accountService: AccountService,
    private _toastr: ToastrService,
    private _router: Router
  ) {}

  ngOnInit(): void {}

  register(): void {
    this._accountService.register(this.model).subscribe({
      next: (res) => {
        console.log(res);
        this._router.navigateByUrl('/members');
        this._toastr.success('Register Success');
      },

      error: (err) => {
        if (err.error.errors) {
          const serverError = [];
          Object.entries(err.error.errors).map((err) => {
            serverError.push(err);
          });

          serverError.forEach((err) => {
            this._toastr.error(err[1][0]);
          });
        } else {
          this._toastr.error(err.error);
        }
      },
    });
  }

  cancel(): void {
    this.cancelRegister.emit(false);
  }
}
