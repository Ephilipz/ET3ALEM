import { Transform, Type } from "class-transformer";
import { RegisterUser } from "src/app/auth/Model/User";
import { QuestionTypeResolver } from "src/app/question/shared/question-type-resolver";
import { Quiz } from "./quiz";
import {
  QuestionAttempt
} from "../../question/Models/questionAttempt/question-attempt";

export class QuizAttempt {
    Id: number;
    UserId: string;
    @Type(()=> RegisterUser)
    User;
    QuizId: number;
    @Type(() => Quiz)
    Quiz: Quiz;
    @Type(() => Date)
    StartTime: Date;
    @Type(() => QuestionAttempt)
    @Transform((value, object, type) => QuestionTypeResolver.getSpecificQuestionAttempt(value))
    QuestionsAttempts: Array<QuestionAttempt> = [];
    Grade: number;
    IsGraded: boolean;
    @Type(() => Date)
    SubmitTime?: Date;
    IsSubmitted: boolean;

    constructor(Id, UserId, QuizId, StartTime, QuestionAttempts = [], Grade = 0, IsGraded = false, SubmitTime?: Date) {
        this.Id = Id;
        this.UserId = UserId;
        this.QuizId = QuizId;
        this.StartTime = StartTime;
        this.QuestionsAttempts = QuestionAttempts;
        this.Grade = Grade;
        this.IsGraded = IsGraded;
        this.SubmitTime = SubmitTime;
    }

    UpdateQuestionTypes() {
        this.QuestionsAttempts?.forEach(qA => qA.UpdateQuestionType());
    }
}
