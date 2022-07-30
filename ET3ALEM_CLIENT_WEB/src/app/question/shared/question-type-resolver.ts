import {
  ConcreteEditQuestionMCQComponent
} from "../edit-create-question/ConcreteQuestions/concrete-edit-question-mcq/concrete-edit-quesiton-mcq.component";
import {
  ConcreteEditQuestionTrueFalseComponent
} from "../edit-create-question/ConcreteQuestions/concrete-edit-question-true-false/concrete-edit-question-true-false.component";
import {MultipleChoiceQuestion} from "../Models/mcq";
import {QuestionType} from "../Models/question-type.enum";
import {TrueFalseQuestion} from "../Models/true-false-question";
import {
  ConcreteAnswerQuestionMCQComponent
} from "../answer-question/ConcreteAnswerQuestions/concrete-answer-question-mcq/concrete-answer-question-mcq.component";
import {
  ConcreteAnswerQuestionTFComponent
} from "../answer-question/ConcreteAnswerQuestions/concrete-answer-question-tf/concrete-answer-question-tf.component";

import {
  ConcreteQuestionResultMCQComponent
} from "../question result/concrete-question-result/concrete-question-result-mcq/concrete-question-result-mcq.component";
import {
  ConcreteQuestionResultTFComponent
} from "../question result/concrete-question-result/concrete-question-result-tf/concrete-question-result-tf.component";
import {Question} from "../Models/question";
import {ShortAnswerQuestion} from "../Models/short-answer-question";
import {
  ConcreteEditQuestionShortAnswerComponent
} from "../edit-create-question/ConcreteQuestions/concrete-edit-question-short-answer/concrete-edit-question-short-answer.component";
import {
  ConcreteAnswerQuestionShortAnswerComponent
} from "../answer-question/ConcreteAnswerQuestions/concrete-answer-question-short-answer/concrete-answer-question-short-answer.component";
import {
  ConcreteQuestionResultShortAnswerComponent
} from "../question result/concrete-question-result/concrete-question-result-short-answer/concrete-question-result-short-answer.component";
import {LongAnswerQuestion} from "../Models/long-answer-question";
import {
  ConcreteQuestionResultLongAnswerComponent
} from "../question result/concrete-question-result/concrete-question-result-long-answer/concrete-question-result-long-answer.component";
import {
  ConcreteAnswerQuestionLongAnswerComponent
} from "../answer-question/ConcreteAnswerQuestions/concrete-answer-question-long-answer/concrete-answer-question-long-answer.component";
import {
  ConcreteEditQuestionLongAnswerComponent
} from "../edit-create-question/ConcreteQuestions/concrete-edit-question-long-answer/concrete-edit-question-long-answer.component";
import {OrderQuestion} from "../Models/order-question";
import {QuestionAttempt} from "../Models/questionAttempt/question-attempt";
import {MCQAttempt} from "../Models/questionAttempt/mcq-attempt";
import {TrueFalseAttempt} from "../Models/questionAttempt/true-false-attempt";
import {
  ShortAnswerAttempt
} from "../Models/questionAttempt/short-answer-attempt";
import {LongAnswerAttempt} from "../Models/questionAttempt/long-answer-attempt";
import {OrderAttempt} from "../Models/questionAttempt/order-attempt";
import {
  ConcreteEditQuestionOrderComponent
} from "../edit-create-question/ConcreteQuestions/concrete-edit-question-order/concrete-edit-question-order.component";
import {
  ConcreteAnswerQuestionOrderComponent
} from "../answer-question/ConcreteAnswerQuestions/concrete-answer-question-order/concrete-answer-question-order.component";
import {
  ConcreteQuestionResultOrderComponent
} from "../question result/concrete-question-result/concrete-question-result-order/concrete-question-result-order.component";

/**
 * A shared class with the question type dictionaries for the creation of specific questions
 */
export class QuestionTypeResolver {


  public static editQuestionComponentsMap = {
    MCQ: ConcreteEditQuestionMCQComponent,
    TrueFalse: ConcreteEditQuestionTrueFalseComponent,
    ShortAnswer: ConcreteEditQuestionShortAnswerComponent,
    LongAnswer: ConcreteEditQuestionLongAnswerComponent,
    OrderQuestion: ConcreteEditQuestionOrderComponent
  }


  public static answerQuestionComponentMap = {
    MCQ: ConcreteAnswerQuestionMCQComponent,
    TrueFalse: ConcreteAnswerQuestionTFComponent,
    ShortAnswer: ConcreteAnswerQuestionShortAnswerComponent,
    LongAnswer: ConcreteAnswerQuestionLongAnswerComponent,
    OrderQuestion: ConcreteAnswerQuestionOrderComponent
  }

  public static viewQuestionResultComponentMap = {
    MCQ: ConcreteQuestionResultMCQComponent,
    TrueFalse: ConcreteQuestionResultTFComponent,
    ShortAnswer: ConcreteQuestionResultShortAnswerComponent,
    LongAnswer: ConcreteQuestionResultLongAnswerComponent,
    OrderQuestion: ConcreteQuestionResultOrderComponent
  }

  public static questionTypeNames = [
    {text: 'True False', value: QuestionType.TrueFalse},
    {text: 'Multiple Choice', value: QuestionType.MCQ},
    {text: 'Short Answer', value: QuestionType.ShortAnswer},
    {text: 'Long Answer', value: QuestionType.LongAnswer},
    {text: 'Order Question', value: QuestionType.OrderQuestion}
  ];

  public static getNewQuestionInstance(type: QuestionType) {
    switch (type) {
      case QuestionType.MCQ:
        return new MultipleChoiceQuestion();
      case QuestionType.TrueFalse:
        return new TrueFalseQuestion();
      case QuestionType.ShortAnswer:
        return new ShortAnswerQuestion();
      case QuestionType.LongAnswer:
        return new LongAnswerQuestion();
      case QuestionType.OrderQuestion:
        return new OrderQuestion();
      default:
        break;
    }
  }

  public static getSpecificQuestion(question: any) {
    if (!question) return null;
    const type: QuestionType = question.QuestionType;
    switch (type) {
      case QuestionType.MCQ:
        return new MultipleChoiceQuestion(question.Id, question.Body, question.Choices, question.McqAnswerType, question.Comment);
      case QuestionType.TrueFalse:
        return new TrueFalseQuestion(question.Id, question.Body, question.Answer, question.Comment);
      case QuestionType.ShortAnswer:
        return new ShortAnswerQuestion(question.Id, question.Body, question.Comment, question.PossibleAnswers);
      case QuestionType.LongAnswer:
        return new LongAnswerQuestion(question.Id, question.Body, question.Comment)
      case QuestionType.OrderQuestion:
        return new OrderQuestion(question.Id, question.Body, question.OrderedElements, question.CorrectOrderIds, question.Comment);
      default:
        return question;
    }
  }

  public static getSpecificQuestionList(questions: Array<Question>) {
    return questions.map(this.getSpecificQuestion);
  }

  public static getSpecificQuestionAttempt(questionAttempts: QuestionAttempt[]): QuestionAttempt[] {
    if (!questionAttempts) return [];
    questionAttempts.map(questionAttempt => {
      const type = (<any>questionAttempt)?.QuizQuestion?.Question?.QuestionType;
      if (!type)
        return null;
      switch (type) {
        case QuestionType.MCQ:
          return new MCQAttempt(questionAttempt.Id, questionAttempt.QuizQuestion, questionAttempt.Grade, (<MCQAttempt>questionAttempt).Choices);
        case QuestionType.TrueFalse:
          return new TrueFalseAttempt(questionAttempt.Id, questionAttempt.QuizQuestion, questionAttempt.Grade, (<TrueFalseAttempt>questionAttempt).Answer);
        case QuestionType.ShortAnswer:
          return new ShortAnswerAttempt(questionAttempt.Id, questionAttempt.QuizQuestion, questionAttempt.Grade, (<ShortAnswerAttempt>questionAttempt).Answer);
        case QuestionType.LongAnswer:
          return new LongAnswerAttempt(questionAttempt.Id, questionAttempt.QuizQuestion, questionAttempt.Grade, (<LongAnswerAttempt>questionAttempt).LongAnswer);
        case QuestionType.OrderQuestion:
          return new OrderAttempt(questionAttempt.Id, questionAttempt.QuizQuestion, questionAttempt.Grade, (<OrderAttempt>questionAttempt).Answer);
        default:
          return questionAttempt;
      }
    });
    return questionAttempts;
  }
}
