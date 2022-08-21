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

  ngOnInit(): void {}

  public registerToggle(registerMode: boolean) {
    this.registerMode = !registerMode;
  }

  public cancelClick($event) {
    this.registerMode = $event;
  }
}
