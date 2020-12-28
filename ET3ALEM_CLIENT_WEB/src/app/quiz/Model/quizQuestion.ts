import { Transform, Type } from 'class-transformer';
import { Question } from 'src/app/question/Models/question';
import { Helper } from 'src/app/Shared/Classes/helpers/Helper';

export class QuizQuestion {
    Id: number;
    @Transform((value, obj, type) => Helper.getSpecificQuestion(value))
    Question: Question;
    Grade: number;
    QuestionId: number;

    constructor(question: Question=null, grade = 0, Id = 0) {
        this.Question = question;
        this.Grade = grade;
        this.Id = Id;
        this.QuestionId = question?.Id;
    }
}