import { Component, HostListener, OnInit, ViewChild } from '@angular/core';
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
  @HostListener('window:beforeunload', ['$event']) unloadNotification(
    $event: any
  ) {
    if (this.editForm.dirty) {
      $event.returnValue = true;
    }
  }

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
    this.memberService.updateMember(this.member).subscribe({
      next: (_) => {
        this.toastrService.success('Edit Success');
        this.editForm.reset(this.member);
      },
      error: (err) => {
        const serverError = [];
        Object.entries(err.error.errors).map((err) => {
          serverError.push(err);
        });

        serverError.forEach((err) => {
          this.toastrService.error(err[1][0]);
        });
      },
    });
  }
}
