import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-access-quiz',
  templateUrl: './access-quiz.component.html',
  styleUrls: ['./access-quiz.component.css']
})
export class AccessQuizComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

  x(){
    alert('d');
  }

}
