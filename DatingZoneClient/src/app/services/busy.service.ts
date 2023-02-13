import { Injectable } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';

@Injectable({
  providedIn: 'root',
})
export class BusyService {
  public busyRequestCount: number = 0;

  constructor(private _ngxSpinner: NgxSpinnerService) {}

  public increment(): void {
    this.busyRequestCount++;
    this._ngxSpinner.show(undefined, {
      type: 'ball-scale-multiple',
      bdColor: 'rgba(255,255,255,0)',
      color: '#333333',
    });
  }

  public decrement(): void {
    this.busyRequestCount--;
    if (this.busyRequestCount <= 0) {
      this.busyRequestCount = 0;
      this._ngxSpinner.hide();
    }
  }
}
