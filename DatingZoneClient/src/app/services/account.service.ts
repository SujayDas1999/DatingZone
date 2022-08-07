import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable, ReplaySubject } from 'rxjs';
import { User } from '../models/User.model';

@Injectable({
  providedIn: 'root',
})
export class AccountService {
  public base_url = `https://localhost:44300/api/`;

  private currentUserSource = new ReplaySubject<User>(1);
  public currentUser$ = this.currentUserSource.asObservable();

  constructor(private http: HttpClient) {}

  public login(model: any) {
    return this.http
      .post(`https://localhost:44300/api/account/login`, model)
      .pipe(
        map((response: User) => {
          const user = response;
          if (user) {
            localStorage.setItem('user', JSON.stringify(user));
            this.currentUserSource.next(user);
          }
        })
      );
  }

  public setCurrentUser(user: User) {
    this.currentUserSource.next(user);
  }

  public logout() {
    localStorage.removeItem('user');
    this.currentUserSource.next(null);
  }

  public register(model: any): Observable<any> {
    return this.http
      .post(`https://localhost:44300/api/account/register`, model)
      .pipe(
        map((response: User) => {
          if (response) {
            localStorage.setItem('user', JSON.stringify(response));
            this.currentUserSource.next(response);
          }

          return response;
        })
      );
  }
}
