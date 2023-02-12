import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { concatMap, take, tap } from 'rxjs';
import { Member } from 'src/app/models/Member.model';
import { User } from 'src/app/models/User.model';
import { AccountService } from 'src/app/services/account.service';
import { MemberService } from 'src/app/services/member.service';

@Component({
  selector: 'app-member-edit',
  templateUrl: './member-edit.component.html',
  styleUrls: ['./member-edit.component.css'],
})
export class MemberEditComponent implements OnInit {
  public member: Member;
  public user: User;
  @ViewChild('editForm') public editForm: NgForm;

  constructor(
    private memberService: MemberService,
    private accountService: AccountService,
    private toastrService: ToastrService
  ) {
    this.accountService.currentUser$
      .pipe(take(1))
      .subscribe((user) => (this.user = user));
  }

  ngOnInit(): void {
    this.getMemberDetails();
  }

  public getMemberDetails(): void {
    this.memberService
      .getMember(this.user.userName)
      .subscribe((member) => (this.member = member));
  }

  public updateMember(): void {
    console.log(this.member);
    this.toastrService.success('Edit Success');
    this.editForm.reset(this.member);
  }
}
