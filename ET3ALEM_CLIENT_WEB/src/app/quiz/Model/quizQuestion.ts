import { Question } from 'src/app/question/Models/question';

export class QuizQuestion {
    Id: number;
    Question: Question;
    Grade: number;
    QuestionId: number;

    constructor(question: Question, grade = 0, Id = 0) {
        this.Question = question;
        this.Grade = grade;
        this.Id = Id;
        this.QuestionId = question.Id;
    }
}