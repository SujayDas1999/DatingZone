import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable, of, tap } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Member } from '../models/Member.model';

@Injectable({
  providedIn: 'root',
})
export class MemberService {
  public base_url = environment.apiUrl;

  public members: Member[] = [];

  constructor(private http: HttpClient) {}

  public getMembers(): Observable<Member[]> {
    return this.http
      .get<Member[]>(`${this.base_url}users`)
      .pipe(tap((members) => (this.members = members)));
  }

  public getMember(username: string): Observable<Member> {
    const newMember = this.members.find(
      (member) => member.username == username
    );

    if (newMember) return of(newMember);

    return this.http.get<Member>(`${this.base_url}users/username/${username}`);
  }

  public updateMember(member: Member): Observable<any> {
    return this.http
      .put<Observable<any>>(`${this.base_url}users/update`, member)
      .pipe(
        map((_) => {
          const index = this.members.indexOf(member);
          this.members[index] = {
            ...this.members[index],
            ...member,
          };
        })
      );
  }
}
