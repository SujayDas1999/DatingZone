import { Component, OnInit } from '@angular/core';
import { Member } from 'src/app/models/Member.model';
import { MemberService } from 'src/app/services/member.service';

@Component({
  selector: 'app-member-list',
  templateUrl: './member-list.component.html',
  styleUrls: ['./member-list.component.css'],
})
export class MemberListComponent implements OnInit {
  public members: Member[];

  constructor(private memberService: MemberService) {}

  ngOnInit(): void {
    this.loadMembers();
  }

  public loadMembers() {
    this.memberService.getMembers().subscribe((members) => {
      this.members = members;
    });
  }
}
