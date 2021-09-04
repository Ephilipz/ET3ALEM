import {QuizAttempt} from "../../../quiz/Model/quiz-attempt";

export class QuizGradingHelper {
  public static getGradeAsPercentage(attempt: QuizAttempt): string {
    if (attempt.IsGraded) {
      const gradeAsPercentage = attempt.Grade / attempt.Quiz.TotalGrade * 100;
      return gradeAsPercentage.toFixed(2) + '%';
    } else {
      return 'Not Graded Yet';
    }
  }
}
