import { Question } from 'src/app/question/Models/question';

export class Quiz {
    Id: Number;
    Name: string;
    instructions: string;
    durationHours: number;
    durationMinutes: number;
    UnlimitedTime: boolean;
    StartDate: Date;
    EndDate: Date;
    NoDueDate: boolean;
    QuizQuestions: Array<Question>;

    constructor(title: string,
        instructions: string = '',
        durationHours: number,
        durationMinutes: number,
        UnlimitedTime: boolean,
        dateStart: Date,
        dueEnd: Date,
        noDueDate: boolean,
        QuizQuestions?: Array<Question>) {
        this.Name = title;
        this.instructions = instructions;
        this.durationHours = durationHours;
        this.durationMinutes = durationMinutes;
        this.UnlimitedTime = UnlimitedTime;
        this.StartDate = dateStart;
        this.EndDate = dueEnd;
        this.NoDueDate = noDueDate;
        this.QuizQuestions = QuizQuestions;
    }
}