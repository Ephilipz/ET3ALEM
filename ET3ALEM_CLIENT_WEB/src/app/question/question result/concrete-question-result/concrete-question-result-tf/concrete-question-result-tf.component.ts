import { Component, OnInit } from '@angular/core';
import { TrueFalseAttempt } from 'src/app/question/Models/true-false-attempt';
import { TrueFalseQuestion } from 'src/app/question/Models/true-false-question';
import { AC_ConcreteQuestionResult } from '../ac-concrete-question-result';

@Component({
  selector: 'app-concrete-question-result-tf',
  templateUrl: './concrete-question-result-tf.component.html',
  styleUrls: ['./concrete-question-result-tf.component.css']
})
export class ConcreteQuestionResultTFComponent implements OnInit, AC_ConcreteQuestionResult {

  questionAttempt: TrueFalseAttempt;
  answer: boolean = false;

  constructor() { }

  ngOnInit(): void {
    if (this.questionAttempt) {
      this.answer = (this.questionAttempt.QuizQuestion.Question as TrueFalseQuestion).Answer;
    }
  }

  /**
   * Gets the tooltip text if the answer is incorrect
   * @param currentBool : the boolean of the section
   * @returns string with tooltip text
   */
  getToolTipText(currentBool: boolean) {
    if (this.answer == this.questionAttempt.Answer) {
      return null;
    }
    if (currentBool == this.questionAttempt.Answer && this.questionAttempt.IsAnswered)
      return 'Your Answer';
    else if (currentBool == this.questionAttempt.Answer && !this.questionAttempt.IsAnswered)
      return null;
    else
      return 'Correct Answer';
  }
}
