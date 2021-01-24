import { QuestionAttempt } from "./question-attempt";

export class TrueFalseAttempt extends QuestionAttempt{
    Answer: boolean;
    constructor(Id, QuizQuestionId, Grade, Answer = false){
        super(Id, QuizQuestionId, Grade);
        this.Answer = Answer;
    }
}
