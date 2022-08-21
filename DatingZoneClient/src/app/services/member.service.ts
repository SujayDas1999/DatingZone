import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Member } from '../models/Member.model';

@Injectable({
  providedIn: 'root',
})
export class MemberService {
  public base_url = environment.apiUrl;

  constructor(private http: HttpClient) {}

  public getMembers(): Observable<Member[]> {
    return this.http.get<Member[]>(`${this.base_url}users`);
  }

  public getMember(username: string): Observable<Member> {
    return this.http.get<Member>(`${this.base_url}users/username/${username}`);
  }
}
