import { QuizQuestion } from './quizQuestion';

export class Quiz {
    Id: Number;
    Name: string;
    instructions: string;
    DurationSeconds: number;
    UnlimitedTime: boolean;
    StartDate: Date;
    EndDate: Date;
    NoDueDate: boolean;
    QuizQuestions: Array<QuizQuestion> = [];

    constructor(
        Id: Number = 0,
        title: string,
        instructions: string = '',
        durationSeconds: number,
        UnlimitedTime: boolean,
        dateStart: Date,
        dueEnd: Date,
        noDueDate: boolean,
        QuizQuestions?: Array<QuizQuestion>) {
        this.Id = Id;
        this.Name = title;
        this.instructions = instructions;
        this.DurationSeconds = durationSeconds;
        this.UnlimitedTime = UnlimitedTime;
        this.StartDate = dateStart;
        this.EndDate = dueEnd;
        this.NoDueDate = noDueDate;
        this.QuizQuestions = QuizQuestions;
    }
}