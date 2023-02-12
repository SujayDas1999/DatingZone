import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import {
  NgxGalleryAnimation,
  NgxGalleryImage,
  NgxGalleryOptions,
} from '@kolkov/ngx-gallery';
import { tap } from 'rxjs';
import { Member } from 'src/app/models/Member.model';
import { MemberService } from 'src/app/services/member.service';

@Component({
  selector: 'app-member-detail',
  templateUrl: './member-detail.component.html',
  styleUrls: ['./member-detail.component.css'],
})
export class MemberDetailComponent implements OnInit {
  public member: Member;
  public galleryOptions: NgxGalleryOptions[];
  public galleryImages: NgxGalleryImage[];

  constructor(
    private memberService: MemberService,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.loadMember();
    this.setGalleryOptions();
  }

  public loadMember() {
    this.memberService
      .getMember(this.route.snapshot.paramMap.get('username'))
      .pipe(
        tap((member) => (this.member = member)),
        tap(() => (this.galleryImages = this.setGalleryPhotos()))
      )
      .subscribe();
  }

  private setGalleryOptions() {
    this.galleryOptions = [
      {
        width: '500px',
        height: '500px',
        thumbnailsColumns: 4,
        imagePercent: 100,
        imageAnimation: NgxGalleryAnimation.Slide,
        preview: false,
      },
    ];
  }

  private setGalleryPhotos(): NgxGalleryImage[] {
    let imageurls = [];

    this.member.photos.forEach((photo) => {
      imageurls.push({
        small: photo.imageUrl,
        big: photo.imageUrl,
        medium: photo.imageUrl,
      });
    });

    return imageurls;
  }
}
