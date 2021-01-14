import { Expose, Type } from 'class-transformer';
import { QuizQuestion } from './quizQuestion';

export class Quiz {
    Id: Number;
    readonly UserId: string = null;
    Name: string;
    Code: string;
    Instructions: string;
    DurationSeconds: number;
    UnlimitedTime: boolean;
    StartDate: Date;
    EndDate: Date;
    NoDueDate: boolean;

    @Type(() => QuizQuestion)
    QuizQuestions: Array<QuizQuestion> = [];

    constructor(
        Id: Number = 0,
        code: string = '',
        title: string,
        instructions: string = '',
        durationSeconds: number,
        UnlimitedTime: boolean,
        dateStart: Date,
        dueEnd: Date,
        noDueDate: boolean,
        QuizQuestions?: Array<QuizQuestion>) {
        this.Id = Id;
        this.Code = code;
        this.Name = title;
        this.Instructions = instructions;
        this.DurationSeconds = durationSeconds;
        this.UnlimitedTime = UnlimitedTime;
        this.StartDate = dateStart;
        this.EndDate = dueEnd;
        this.NoDueDate = noDueDate;
        this.QuizQuestions = QuizQuestions;
        if(!this.UserId)
            this.UserId = 'NA';
    }

    public updateQuiz(title: string,
        instructions: string = '',
        durationSeconds: number,
        UnlimitedTime: boolean,
        dateStart: Date,
        dueEnd: Date,
        noDueDate: boolean,
        QuizQuestions?: Array<QuizQuestion>) {
            this.Name = title;
            this.Instructions = instructions;
            this.DurationSeconds = durationSeconds;
            this.UnlimitedTime = UnlimitedTime;
            this.StartDate = dateStart;
            this.EndDate = dueEnd;
            this.NoDueDate = noDueDate;
            this.QuizQuestions = QuizQuestions;
    }
}