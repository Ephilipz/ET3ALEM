import { QuestionAttempt } from './question-attempt';

export class TrueFalseAttempt extends QuestionAttempt {
  Answer: boolean;
  IsAnswered: boolean;
  constructor(Id, QuizQuestionId, Grade, Answer = false, IsAnswered = false) {
    super(Id, QuizQuestionId, Grade);
    this.Answer = Answer;
    this.IsAnswered = IsAnswered;
  }
}
