import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

import { AccountService } from '../services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
})
export class RegisterComponent implements OnInit {
  public model: any = {};
  @Output() cancelRegister = new EventEmitter<boolean>();
  constructor(private _accountService: AccountService) {}

  ngOnInit(): void {}

  register(): void {
    this._accountService
      .register(this.model)
      .subscribe((res) => console.log(res));
  }

  cancel(): void {
    this.cancelRegister.emit(false);
  }
}
