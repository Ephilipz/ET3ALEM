import { Question } from 'src/app/question/Models/question';

export class QuizQuestion {
    Id: Number;
    Question: Question;
    Grade: Number;
    QuestionId: Number;

    constructor(question: Question, grade = 0, Id = 0) {
        this.Question = question;
        this.Grade = grade;
        this.Id = Id;
        this.QuestionId = question.Id;
    }
}