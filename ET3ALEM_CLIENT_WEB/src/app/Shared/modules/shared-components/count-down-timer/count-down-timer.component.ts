import { Component, EventEmitter, Input, OnDestroy, OnInit, Output } from '@angular/core';

import { timer } from 'rxjs/internal/observable/timer';
import { Subscription } from 'rxjs/internal/Subscription';
import { takeWhile, tap } from 'rxjs/operators';
import  DateHelper from 'src/app/Shared/Classes/helpers/date.helper';

@Component({
  selector: 'count-down-timer',
  templateUrl: './count-down-timer.component.html',
  styleUrls: ['./count-down-timer.component.css']
})
export class CountDownTimerComponent implements OnInit, OnDestroy {

  @Input() displayDays: boolean = false;
  @Input() displayHours: boolean = true;
  @Input() displayMinutes: boolean = true;
  @Input() displaySeconds: boolean = true;
  @Input() endDate: Date;
  @Input() isUTC: boolean = true;
  @Output() finished = new EventEmitter();

  subscriber: Subscription = null;

  days: number = 0;
  hours: number = 0;
  minutes: number = 0;
  seconds: number = 0;

  constructor() { }

  ngOnInit(): void {
    const now = this.isUTC ? DateHelper.utcNow : DateHelper.now;
    const diff = DateHelper.difference(this.endDate, now);
    const secondsLeft = DateHelper.asSeconds(diff);
    const source = timer(0, 1000);
    this.subscriber = source.pipe(
      tap(val => {
        if (val > Math.floor(secondsLeft)) {
          this.finished.emit(null);
          this.subscriber.unsubscribe();
        }
      }))
      .subscribe(
        val => this.updateTimeDifference(Math.floor(secondsLeft) - val)
      );
  }

  updateTimeDifference(secondsLeft) {
    if (this.displayDays) {
      this.days = Math.floor(secondsLeft / 86400)
      secondsLeft -= this.days * 86400;
    }
    if (this.displayHours) {
      this.hours = Math.floor(secondsLeft / 3600)
      secondsLeft -= this.hours * 3600;
    }
    if (this.displayMinutes) {
      this.minutes = Math.floor(secondsLeft / 60)
      secondsLeft -= this.minutes * 60;
    }
    if (this.displaySeconds) {
      this.seconds = secondsLeft
    }
  }

  ngOnDestroy() {
    this.subscriber.unsubscribe();
  }

}
