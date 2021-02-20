import { Transform, Type } from 'class-transformer';
import { Question } from 'src/app/question/Models/question';
import { QuestionTypeResolver } from 'src/app/question/shared/question-type-resolver';

export class QuizQuestion {
    Id: number;
    @Transform((value, obj, type) => QuestionTypeResolver.getSpecificQuestion(value))
    Question: Question;
    Grade: number;
    QuestionId: number;
    Sequence: number;

    constructor(question: Question=null, grade = 0, Id = 0, Sequence = 0) {
        this.Question = question;
        this.Grade = grade;
        this.Id = Id;
        this.QuestionId = question?.Id;
        this.Sequence = Sequence;
    }
}