import { Question } from './question';
import { QuestionType } from './question-type.enum';
import { Choice } from './choice';
import { Type } from 'class-transformer';
import { GeneralHelper } from 'src/app/Shared/Classes/helpers/GeneralHelper';

export class MultipleChoiceQuestion extends Question {
    @Type(() => Choice)
    Choices: Array<Choice>;

    McqAnswerType: McqAnswerType = McqAnswerType.SingleChoice;

    constructor(Id: number = 0, Body: string = '', choices = [new Choice(), new Choice()], mcqAnswerType: McqAnswerType = McqAnswerType.SingleChoice, Comment: string = null) {
        super(Id, Body, Comment);
        this.QuestionType = QuestionType.MCQ;
        this.Choices = choices;
        this.McqAnswerType = mcqAnswerType;
    }

    protected duplicateQuestionAnswer(newQuestion: MultipleChoiceQuestion) {
        newQuestion.Choices.forEach(choice => choice.Id = GeneralHelper.randomInteger(0, 100) * -1);
    }
}

export enum McqAnswerType {
    SingleChoice,
    MultipleChoice
}
