import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { LocalstorgeService } from 'src/app/Shared/services/localstorge.service';
import { environment } from 'src/environments/environment';
import { QuizAttempt } from '../Model/quiz-attempt';

@Injectable({
  providedIn: 'root'
})

export class QuizAttemptService {


  private baseRoute = environment.baseUrl + '/api/QuizAttempt';

  constructor(private httpClient: HttpClient,
    private localStorageService: LocalstorgeService) { }

  public createQuizAttempt(quizAttempt: QuizAttempt): Observable<QuizAttempt> {
    return this.httpClient.put<QuizAttempt>(this.baseRoute, quizAttempt);
  }

  public getQuizAttempt(quizAttemptId: number) {
    return this.httpClient.get<QuizAttempt>(this.baseRoute + `/${quizAttemptId}`);
  }

  public getQuizAttemptsForQuiz(quizId: number) {
    return this.httpClient.get<Array<QuizAttempt>>(this.baseRoute + '/GetUserQuizAttemptsForQuiz/' + quizId);
  }

  public getAllQuizAttemptsForQuiz(quizId: number) {
    return this.httpClient.get<Array<QuizAttempt>>(this.baseRoute + '/GetAllQuizAttemptsForQuiz/' + quizId);
  }

  public getQuizAttemptWithQuiz(quizAttempt: number) {
    return this.httpClient.get<QuizAttempt>(this.baseRoute + `/GetQuizAttemptWithQuiz/${quizAttempt}`);
  }
  getQuizAttemptWithQuizLight(quizAttempt: number) {
    return this.httpClient.get<QuizAttempt>(this.baseRoute + `/GetQuizAttemptWithQuizLight/${quizAttempt}`);
  }

  public updateQuizAttempt(quizAttempt: QuizAttempt) {
    const userId = this.localStorageService.UserId;
    return this.httpClient.post(this.baseRoute + `/${userId}`, quizAttempt);
  }

  public updateQuizAttemptGrade(quizAttempt: QuizAttempt) {
    return this.httpClient.put(this.baseRoute + `/UpdateQuizAttemptGrade`, quizAttempt);
  }

  public getQuizAttemptsForUser() {
    return this.httpClient.get(this.baseRoute);
  }
}
