import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-count-down-timer',
  templateUrl: './count-down-timer.component.html',
  styleUrls: ['./count-down-timer.component.css']
})
export class CountDownTimerComponent implements OnInit {

  @Input() displayDays: boolean = false;
  @Input() displayHours: boolean = true;
  @Input() displayMinutes: boolean = true;
  @Input() displaySeconds: boolean = true;

  constructor() { }

  ngOnInit(): void {
  }

}
