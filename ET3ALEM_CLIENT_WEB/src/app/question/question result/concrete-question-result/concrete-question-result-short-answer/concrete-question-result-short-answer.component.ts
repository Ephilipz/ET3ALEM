import { Component, Input, OnInit } from '@angular/core';
import { ShortAnswerAttempt } from 'src/app/question/Models/short-answer-attempt';
import { ShortAnswerQuestion } from 'src/app/question/Models/short-answer-question';
import { AC_ConcreteQuestionResult } from '../ac-concrete-question-result';

@Component({
  selector: 'app-concrete-question-result-short-answer',
  templateUrl: './concrete-question-result-short-answer.component.html',
  styleUrls: ['./concrete-question-result-short-answer.component.css']
})

export class ConcreteQuestionResultShortAnswerComponent extends AC_ConcreteQuestionResult implements OnInit {

  @Input() questionAttempt: ShortAnswerAttempt;
  correctAnswers: string = null;
  userAnswer: string = null;
  isCorrectAnswer = false;

  constructor() {
    super();
  }

  ngOnInit(): void {
    this.correctAnswers = (<ShortAnswerQuestion>this.questionAttempt.QuizQuestion.Question).PossibleAnswers.split(',').join(', ');
    this.userAnswer = this.questionAttempt.Answer;
    this.isCorrectAnswer = this.questionAttempt.Grade == this.questionAttempt.QuizQuestion.Grade;
  }

}
