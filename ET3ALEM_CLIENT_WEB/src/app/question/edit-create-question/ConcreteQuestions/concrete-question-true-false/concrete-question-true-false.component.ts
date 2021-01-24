import { Component, Input, OnInit } from '@angular/core';
import { plainToClass } from 'class-transformer';
import { TrueFalseQuestion } from 'src/app/question/Models/true-false-question';
import { AC_ConcreteEditQuestion } from '../ac-concrete-question';

@Component({
  selector: 'app-concrete-question-true-false',
  templateUrl: './concrete-question-true-false.component.html',
  styleUrls: ['./concrete-question-true-false.component.css']
})
export class ConcreteQuestionTrueFalseComponent extends AC_ConcreteEditQuestion implements OnInit {

  constructor() { 
    super();
  }
  
  protected validate() {
    return true;
  }

  @Input() inputQuestion: TrueFalseQuestion;

  ngOnInit(): void {
    this.inputQuestion = plainToClass(TrueFalseQuestion, this.inputQuestion);
  }

  saveQuestion() {
    super.saveQuestion();
    return this.getQuestion();
  }
}
