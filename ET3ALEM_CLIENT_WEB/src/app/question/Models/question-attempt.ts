import { QuizQuestion } from "src/app/quiz/Model/quizQuestion";

export abstract class QuestionAttempt {
    Id: number;
    QuizQuestionId: number;
    QuizQuestion: QuizQuestion;
    Grade: number;
    readonly IsGraded: boolean;

    constructor(Id = 0, QuizQuestionId = 0, Grade = 0){
        this.Id = Id;
        this.QuizQuestionId = QuizQuestionId;
        this.Grade = Grade;
    }

}
