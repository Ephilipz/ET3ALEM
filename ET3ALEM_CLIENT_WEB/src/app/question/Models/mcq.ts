import { Question } from './question';
import { QuestionType } from './question-type.enum';
import { Choice } from './choice';
import { Type } from 'class-transformer';

export class MultipleChoiceQuestion extends Question {
    @Type(() => Choice)
    Choices: Array<Choice>;
    
    McqAnswerType: McqAnswerType = McqAnswerType.SingleChoice;

    constructor(Id: number = 0, Body: string = '', choices = [new Choice(), new Choice()], mcqAnswerType: McqAnswerType = McqAnswerType.SingleChoice) {
        super(Id, Body);
        this.QuestionType = QuestionType.MCQ;
        this.Choices = choices;
        this.McqAnswerType = mcqAnswerType;
    }
}

export enum McqAnswerType {
    SingleChoice,
    MultipleChoice
}