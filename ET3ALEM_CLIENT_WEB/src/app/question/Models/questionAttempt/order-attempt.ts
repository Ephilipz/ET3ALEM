import {QuestionAttempt} from "./question-attempt";
import {QuizQuestion} from "../../../quiz/Model/quizQuestion";

export class OrderAttempt extends QuestionAttempt{
  Answer: string;

  constructor(Id: number, QuizQuestion: QuizQuestion, Grade: number, Answer: string ) {
    super(Id, QuizQuestion, Grade);
    this.Answer = Answer;
  }
}
