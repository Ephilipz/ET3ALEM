import {Component, Input, OnInit} from '@angular/core';
import {TrueFalseAttempt} from 'src/app/question/Models/true-false-attempt';
import {TrueFalseQuestion} from 'src/app/question/Models/true-false-question';
import {AC_ConcreteAnswerQuestion} from '../ac-concrete-answer-question';

@Component({
  selector: 'app-concrete-answer-question-tf',
  templateUrl: './concrete-answer-question-tf.component.html',
  styleUrls: ['./concrete-answer-question-tf.component.css']
})
export class ConcreteAnswerQuestionTFComponent extends AC_ConcreteAnswerQuestion implements OnInit {

  @Input() questionAttempt: TrueFalseAttempt;
  question: TrueFalseQuestion;
  Answer: boolean;

  constructor() {
    super();
  }

  ngOnInit(): void {
    this.question = this.questionAttempt.QuizQuestion.Question as TrueFalseQuestion;
    if (this.questionAttempt.IsAnswered)
      this.Answer = this.questionAttempt.Answer;
  }

  getAnswers() {
    this.questionAttempt.Answer = this.Answer;
    return this.questionAttempt;
  }

  onMatButtonToggleChange() {
    this.questionAttempt.IsAnswered = true;
  }
}
