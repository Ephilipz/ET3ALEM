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
      this.answer = (<TrueFalseQuestion>this.questionAttempt.QuizQuestion.Question).Answer;
    }
  }

  /**
   * Gets the tooltip text if the answer is incorrect
   * @param currentBool : the boolean of the section
   * @returns string with tooltip text
   */
  getToolTipText(currentBool: boolean) {
    if(this.answer == this.questionAttempt.Answer){
      return null;
    }

    return currentBool == this.questionAttempt.Answer ? 'Your Answer' : 'Correct Answer';
  }

}
