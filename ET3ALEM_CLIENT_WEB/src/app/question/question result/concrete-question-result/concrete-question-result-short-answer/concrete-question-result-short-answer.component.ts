import { Component, Input, OnInit } from '@angular/core';
import { ShortAnswerQuestion } from 'src/app/question/Models/short-answer-question';
import { AC_ConcreteQuestionResult } from '../ac-concrete-question-result';
import {
  ShortAnswerAttempt
} from "../../../Models/questionAttempt/short-answer-attempt";

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
    const possibleAnswers = (<ShortAnswerQuestion>this.questionAttempt.QuizQuestion.Question).PossibleAnswers;
    this.correctAnswers = possibleAnswers ? possibleAnswers.split(',').join(', ') : 'EMPTY';
    this.userAnswer = this.questionAttempt.Answer ? this.questionAttempt.Answer : 'EMPTY';
    this.isCorrectAnswer = this.questionAttempt.Grade >= this.questionAttempt.QuizQuestion.Grade;
  }

}
