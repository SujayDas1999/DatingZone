import { Component, Input, OnInit } from '@angular/core';
import { Member } from 'src/app/models/Member.model';

@Component({
  selector: 'app-photo-editor',
  templateUrl: './photo-editor.component.html',
  styleUrls: ['./photo-editor.component.css'],
})
export class PhotoEditorComponent implements OnInit {
  @Input() public member: Member | undefined;

  constructor() {}

  ngOnInit(): void {}
}
