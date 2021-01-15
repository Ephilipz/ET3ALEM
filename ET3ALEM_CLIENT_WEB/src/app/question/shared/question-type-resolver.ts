import { Component } from "@angular/core";
import { ConcreteQuestionMCQComponent } from "../edit-create-question/ConcreteQuestions/concrete-question-mcq/concrete-quesiton-mcq.component";
import { ConcreteQuestionTrueFalseComponent } from "../edit-create-question/ConcreteQuestions/concrete-question-true-false/concrete-question-true-false.component";
import { AC_ConcreteEditQuestion } from "../edit-create-question/ConcreteQuestions/ac-concrete-question";
import { MultipleChoiceQuestion } from "../Models/mcq";
import { QuestionType } from "../Models/question-type.enum";
import { TrueFalseQuestion } from "../Models/true-false-question";
import { ConcreteAnswerQuestionMCQComponent } from "../answer-question/ConcreteAnswerQuestions/concrete-answer-question-mcq/concrete-answer-question-mcq.component";
import { ConcreteAnswerQuestionTFComponent } from "../answer-question/ConcreteAnswerQuestions/concrete-answer-question-tf/concrete-answer-question-tf.component";

/**
 * A shared class with the question type dictionaries for the creation of specific questions 
 */
export class QuestionTypeResolver {
    public static editQuestionComponentsMap = {
        MCQ : ConcreteQuestionMCQComponent,
        TrueFalse : ConcreteQuestionTrueFalseComponent
    }

    
    public static answerQuestionComponentMap = {
        MCQ : ConcreteAnswerQuestionMCQComponent,
        TrueFalse : ConcreteAnswerQuestionTFComponent
    }

    public static questionTypeNames = [
        {text: 'True False', value: QuestionType.TrueFalse},
        {text: 'Multiple Choice', value: QuestionType.MCQ},
    ];
    
    public static getQuestionInstance(type: QuestionType){
        switch(type){
            case QuestionType.MCQ:
                return new MultipleChoiceQuestion();
            case QuestionType.TrueFalse:
                return new TrueFalseQuestion();
            default:
                break;
        }
    }
    
}
