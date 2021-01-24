import { Component, Input, OnInit } from '@angular/core';
import { TrueFalseAttempt } from 'src/app/question/Models/true-false-attempt';
import { TrueFalseQuestion } from 'src/app/question/Models/true-false-question';
import { QuizQuestion } from 'src/app/quiz/Model/quizQuestion';
import { AC_ConcreteAnswerQuestion } from '../ac-concrete-answer-question';

@Component({
  selector: 'app-concrete-answer-question-tf',
  templateUrl: './concrete-answer-question-tf.component.html',
  styleUrls: ['./concrete-answer-question-tf.component.css']
})
export class ConcreteAnswerQuestionTFComponent extends AC_ConcreteAnswerQuestion implements OnInit {

  @Input() quizQuestion: QuizQuestion;
  question: TrueFalseQuestion;
  Answer: boolean;
  
  constructor() {
    super();
  }
  
  ngOnInit(): void {
    this.question = <TrueFalseQuestion>this.quizQuestion.Question;
  }
  
  getAnswers() {
    return new TrueFalseAttempt(0, this.quizQuestion, 0, false);
  }
}
