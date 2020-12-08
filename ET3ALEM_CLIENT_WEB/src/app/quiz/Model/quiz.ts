import { QuizQuestion } from './quizQuestion';

export class Quiz {
    Id: Number;
    Name: string;
    code: string;
    instructions: string;
    DurationSeconds: number;
    UnlimitedTime: boolean;
    StartDate: Date;
    EndDate: Date;
    NoDueDate: boolean;
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
        this.code = code;
        this.Name = title;
        this.instructions = instructions;
        this.DurationSeconds = durationSeconds;
        this.UnlimitedTime = UnlimitedTime;
        this.StartDate = dateStart;
        this.EndDate = dueEnd;
        this.NoDueDate = noDueDate;
        this.QuizQuestions = QuizQuestions;
    }

    public static quizFromExisting(q: Quiz, title: string,
        instructions: string = '',
        durationSeconds: number,
        UnlimitedTime: boolean,
        dateStart: Date,
        dueEnd: Date,
        noDueDate: boolean,
        QuizQuestions?: Array<QuizQuestion>): Quiz{
        return new Quiz(q.Id, q.code, title,
            instructions,
            durationSeconds,
            UnlimitedTime,
            dateStart,
            dueEnd,
            noDueDate,
            QuizQuestions)
    }
}