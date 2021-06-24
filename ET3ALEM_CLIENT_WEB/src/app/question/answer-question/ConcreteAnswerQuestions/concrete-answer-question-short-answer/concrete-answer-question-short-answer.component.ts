import { Component, Input, OnInit } from '@angular/core';
import { ShortAnswerAttempt } from 'src/app/question/Models/short-answer-attempt';
import { ShortAnswerQuestion } from 'src/app/question/Models/short-answer-question';
import { QuizQuestion } from 'src/app/quiz/Model/quizQuestion';
import { AC_ConcreteAnswerQuestion } from '../ac-concrete-answer-question';

@Component({
  selector: 'app-concrete-answer-question-short-answer',
  templateUrl: './concrete-answer-question-short-answer.component.html',
  styleUrls: ['./concrete-answer-question-short-answer.component.css']
})
export class ConcreteAnswerQuestionShortAnswerComponent extends AC_ConcreteAnswerQuestion implements OnInit {

  @Input() questionAttempt: ShortAnswerAttempt;
  quizQuestion: QuizQuestion = null;
  question: ShortAnswerQuestion;
  Answer = null;

  constructor() {
    super();
  }

  ngOnInit(): void {
    this.quizQuestion = this.questionAttempt.QuizQuestion;
    this.question = <ShortAnswerQuestion>this.quizQuestion.Question;
  }

  getAnswers(){
    this.questionAttempt.Answer = this.Answer;
    return this.questionAttempt;
  }

}
