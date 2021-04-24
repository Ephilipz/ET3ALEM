import { QuizQuestion } from "src/app/quiz/Model/quizQuestion";
import { QuestionType } from "./question-type.enum";

export abstract class QuestionAttempt {
    Id: number;
    QuizQuestionId: number;
    QuizQuestion: QuizQuestion;
    questionType: QuestionType;
    Grade: number;
    readonly IsGraded: boolean;

    constructor(Id = 0, QuizQuestion, Grade = 0){
        this.Id = Id;
        this.QuizQuestion = QuizQuestion;
        this.QuizQuestionId = this.QuizQuestion?.Id;
        this.Grade = Grade;
    }

    public UpdateQuestionType(){
        this.questionType = this.QuizQuestion?.Question?.QuestionType;
    }

}
