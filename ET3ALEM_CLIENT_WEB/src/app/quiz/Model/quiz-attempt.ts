import { Transform, Type } from "class-transformer";
import { MCQAttempt } from "src/app/question/Models/mcq-attempt";
import { QuestionAttempt } from "src/app/question/Models/question-attempt";
import { QuestionType } from "src/app/question/Models/question-type.enum";
import { TrueFalseAttempt } from "src/app/question/Models/true-false-attempt";
import { QuestionTypeResolver } from "src/app/question/shared/question-type-resolver";

export class QuizAttempt {
    Id: number;
    UserId: string;
    QuizId: number;
    @Transform((value, object, type) => QuestionTypeResolver.getSpecificQuestionAttempt(value))
    QuestionAttempts: Array<QuestionAttempt>;
    Grade: number;
    IsGraded: boolean;

    constructor(Id, UserId, QuizId, QuestionAttempts = [], Grade = 0, IsGraded = false){
        this.Id = Id;
        this.UserId = UserId;
        this.QuizId = QuizId;
        this.QuestionAttempts = QuestionAttempts;
        this.Grade = Grade,
        this.IsGraded = IsGraded;
    }
}
