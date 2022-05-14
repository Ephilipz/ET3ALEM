import {QuestionAttempt} from "../../Models/questionAttempt/question-attempt";

export abstract class AC_ConcreteQuestionResult {
  // public isCorrectGrade(): boolean {
  //   return this.questionAttempt?.Grade >= this.questionAttempt?.QuizQuestion?.Grade;
  // }

  abstract questionAttempt: QuestionAttempt = null;
}
