import { MultipleChoiceQuestion } from 'src/app/question/Models/mcq';
import { QuestionType } from 'src/app/question/Models/question-type.enum';
import { TrueFalseQuestion } from 'src/app/question/Models/true-false-question';

export class Helper {

    static BtoMB(B: number, precision = 2): number {
        return +Math.round(B / 1048576).toFixed(precision);
    }

    static randomInteger(min, max) {
        return Math.floor(Math.random() * (max - min + 1)) + min;
    }

    static ticksToHoursMinutes(ticks: number) {
        return { "hour": Math.floor(ticks / 7200), "minute": (ticks % 7200) * 1200 }
    }

    static hoursMinutesToTicks(hours: number, minutes: number) {
        return hours * 72000 + minutes * 1200;
    }

    static getSpecificQuestion(question: any) {
        let type: QuestionType = question.QuestionType;
        switch (type) {
            case QuestionType.MCQ:
                return new MultipleChoiceQuestion(question.Id, question.Body, question.Choices, question.McqAnswerType);
            case QuestionType.TrueFalse:
                return new TrueFalseQuestion(question.Id, question.Body, question.Answer);
            default:
                alert("didn't detect question type");
                return question;
        }
    }
}