import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent implements OnInit {
  public registerMode: boolean = false;
  public users: any;

  constructor(private _http: HttpClient) {}

  ngOnInit(): void {
    this.getUsers();
  }

  public registerToggle(registerMode: boolean) {
    this.registerMode = !registerMode;
  }

  public getUsers() {
    this._http
      .get<any>(`https://localhost:44300/api/users`)
      .subscribe((users) => (this.users = users));
  }

  public cancelClick($event) {
    this.registerMode = $event;
  }
}
