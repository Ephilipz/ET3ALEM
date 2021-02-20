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
    @Type(() => Date)
    StartTime: Date;
    @Transform((value, object, type) => QuestionTypeResolver.getSpecificQuestionAttempt(value))
    QuestionsAttempts: Array<QuestionAttempt>;
    Grade: number;
    IsGraded: boolean;
    @Type(() => Date)
    SubmitTime?: Date;

    constructor(Id, UserId, QuizId, StartTime, QuestionAttempts = [], Grade = 0, IsGraded = false, SubmitTime?: Date){
        this.Id = Id;
        this.UserId = UserId;
        this.QuizId = QuizId;
        this.StartTime = StartTime;
        this.QuestionsAttempts = QuestionAttempts;
        this.Grade = Grade,
        this.IsGraded = IsGraded;
        this.SubmitTime = SubmitTime;
    }

    UpdateQuestionTypes(){
        this.QuestionsAttempts?.forEach(qA => qA.UpdateQuestionType());
    }
}
