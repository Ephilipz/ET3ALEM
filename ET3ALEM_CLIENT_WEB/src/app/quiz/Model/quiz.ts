import { Expose, Type } from 'class-transformer';
import { QuizQuestion } from './quizQuestion';

export class Quiz {
    Id: number;
    readonly UserId: string = null;
    Name: string;
    Code: string;
    Instructions: string;
    DurationSeconds: number;
    UnlimitedTime: boolean;
    @Type(() => Date)
    StartDate: Date;
    @Type(() => Date)
    EndDate: Date;
    @Type(() => Date)
    CreatedDate: Date;
    NoDueDate: boolean;
    AllowedAttempts: number;
    UnlimitedAttempts: boolean;
    ShowGrade: boolean;
    AutoGrade: boolean;
    TotalGrade: number;
    ShuffleQuestions: boolean;
    IncludeAllQuestions: boolean;
    IncludedQuestionsCount: number;
    @Type(() => QuizQuestion)
    QuizQuestions: Array<QuizQuestion> = [];

    constructor(
        Id: number = 0,
        code: string = '',
        title: string,
        instructions: string = '',
        durationSeconds: number,
        UnlimitedTime: boolean,
        dateStart: Date,
        dueEnd: Date,
        createdDate: Date,
        noDueDate: boolean,
        QuizQuestions?: Array<QuizQuestion>,
        AllowedAttempts: number = 1,
        UnlimitedAttempts: boolean = false,
        ShowGrade: boolean = true,
        AutoGrade: boolean = true,
        ShuffleQuestions: boolean = false,
        IncludeAllQuestions: boolean = true,
        IncludedQuestionsCount: number = null) {
        this.Id = Id;
        this.Code = code;
        this.Name = title;
        this.Instructions = instructions;
        this.DurationSeconds = durationSeconds;
        this.UnlimitedTime = UnlimitedTime;
        this.StartDate = dateStart;
        this.EndDate = dueEnd;
        this.NoDueDate = noDueDate;
        this.CreatedDate = createdDate;
        this.QuizQuestions = QuizQuestions;
        if (!this.UserId)
            this.UserId = 'NA';
        this.AllowedAttempts = AllowedAttempts;
        this.UnlimitedAttempts = UnlimitedAttempts;
        this.ShowGrade = ShowGrade,
            this.AutoGrade = AutoGrade;
        this.ShuffleQuestions = ShuffleQuestions;
        this.IncludeAllQuestions = IncludeAllQuestions || IncludedQuestionsCount == this.QuizQuestions?.length;
        this.IncludedQuestionsCount = IncludedQuestionsCount;
    }

    public updateQuiz(title: string,
        instructions: string = '',
        durationSeconds: number,
        UnlimitedTime: boolean,
        dateStart: Date,
        dueEnd: Date,
        noDueDate: boolean,
        QuizQuestions?: Array<QuizQuestion>,
        AllowedAttempts: number = 1,
        UnlimitedAttempts: boolean = false,
        ShowGrade: boolean = true,
        AutoGrade: boolean = true,
        ShuffleQuestions: boolean = false,
        IncludeAllQuestions: boolean = true,
        IncludedQuestionsCount: number = null) {
        this.Name = title;
        this.Instructions = instructions;
        this.DurationSeconds = durationSeconds;
        this.UnlimitedTime = UnlimitedTime;
        this.StartDate = dateStart;
        this.EndDate = dueEnd;
        this.NoDueDate = noDueDate;
        this.QuizQuestions = QuizQuestions;
        this.AllowedAttempts = AllowedAttempts;
        this.UnlimitedAttempts = UnlimitedAttempts;
        this.ShowGrade = ShowGrade,
            this.AutoGrade = AutoGrade;
        this.ShuffleQuestions = ShuffleQuestions;
        this.IncludeAllQuestions = IncludeAllQuestions || IncludedQuestionsCount == this.QuizQuestions?.length;;
    }
}