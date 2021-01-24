import { ConcreteQuestionMCQComponent } from "../edit-create-question/ConcreteQuestions/concrete-question-mcq/concrete-quesiton-mcq.component";
import { ConcreteQuestionTrueFalseComponent } from "../edit-create-question/ConcreteQuestions/concrete-question-true-false/concrete-question-true-false.component";
import { MultipleChoiceQuestion } from "../Models/mcq";
import { QuestionType } from "../Models/question-type.enum";
import { TrueFalseQuestion } from "../Models/true-false-question";
import { ConcreteAnswerQuestionMCQComponent } from "../answer-question/ConcreteAnswerQuestions/concrete-answer-question-mcq/concrete-answer-question-mcq.component";
import { ConcreteAnswerQuestionTFComponent } from "../answer-question/ConcreteAnswerQuestions/concrete-answer-question-tf/concrete-answer-question-tf.component";
import { QuestionAttempt } from "../Models/question-attempt";
import { MCQAttempt } from "../Models/mcq-attempt";
import { TrueFalseAttempt } from "../Models/true-false-attempt";

/**
 * A shared class with the question type dictionaries for the creation of specific questions 
 */
export class QuestionTypeResolver {


    public static editQuestionComponentsMap = {
        MCQ: ConcreteQuestionMCQComponent,
        TrueFalse: ConcreteQuestionTrueFalseComponent
    }


    public static answerQuestionComponentMap = {
        MCQ: ConcreteAnswerQuestionMCQComponent,
        TrueFalse: ConcreteAnswerQuestionTFComponent
    }

    public static questionTypeNames = [
        { text: 'True False', value: QuestionType.TrueFalse },
        { text: 'Multiple Choice', value: QuestionType.MCQ },
    ];

    public static getNewQuestionInstance(type: QuestionType) {
        switch (type) {
            case QuestionType.MCQ:
                return new MultipleChoiceQuestion();
            case QuestionType.TrueFalse:
                return new TrueFalseQuestion();
            default:
                break;
        }
    }

    public static getSpecificQuestion(question: any) {
        if (!question) return null;
        let type: QuestionType = question.QuestionType;
        switch (type) {
            case QuestionType.MCQ:
                return new MultipleChoiceQuestion(question.Id, question.Body, question.Choices, question.McqAnswerType);
            case QuestionType.TrueFalse:
                return new TrueFalseQuestion(question.Id, question.Body, question.Answer);
            default:
                return question;
        }
    }

    public static getSpecificQuestionAttempt(questionAttempt: QuestionAttempt): any {
        const type = (<any>questionAttempt)?.QuizQuestion?.Question?.QuestionType;
        if (!type)
            return null;
        switch (type) {
            case QuestionType.MCQ:
                return new MCQAttempt(questionAttempt.Id, questionAttempt.QuizQuestion, questionAttempt.Grade, (<MCQAttempt>questionAttempt).Choices);
            case QuestionType.TrueFalse:
                return new TrueFalseAttempt(questionAttempt.Id, questionAttempt.QuizQuestion, questionAttempt.Grade, (<TrueFalseAttempt>questionAttempt).Answer);
            default:
                return questionAttempt;
        }
    }
}
