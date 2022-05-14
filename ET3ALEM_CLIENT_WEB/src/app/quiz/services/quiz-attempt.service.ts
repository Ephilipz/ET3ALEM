import {HttpClient} from '@angular/common/http';
import {Injectable} from '@angular/core';
import {Observable} from 'rxjs';
import {LocalStorageService} from 'src/app/Shared/services/local-storage.service';
import {environment} from 'src/environments/environment';
import {QuizAttempt} from '../Model/quiz-attempt';

@Injectable({
  providedIn: 'root'
})

export class QuizAttemptService {


  private baseRoute = environment.baseUrl + '/api/QuizAttempt';

  constructor(private httpClient: HttpClient,
              private localStorageService: LocalStorageService) {
  }

  public createQuizAttempt(quizAttempt: QuizAttempt): Observable<QuizAttempt> {
    return this.httpClient.post<QuizAttempt>(this.baseRoute, quizAttempt);
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

  public getUngradedQuizAttemptsForQuiz(quizId: number){
    return this.httpClient.get<Array<QuizAttempt>>(this.baseRoute + '/GetUngradedAttemptsForQuiz/' + quizId);
  }

  public getQuizAttemptWithQuiz(quizAttempt: number) {
    return this.httpClient.get<QuizAttempt>(this.baseRoute + `/GetQuizAttemptWithQuiz/${quizAttempt}`);
  }

  public getQuizAttemptWithQuizLight(quizAttempt: number) {
    return this.httpClient.get<QuizAttempt>(this.baseRoute + `/GetQuizAttemptWithQuizLight/${quizAttempt}`);
  }

  public updateQuizAttempt(quizAttempt: QuizAttempt) {
    const userId = this.localStorageService.UserId;
    return this.httpClient.put(this.baseRoute + `/${userId}`, quizAttempt);
  }

  public updateQuizAttemptGrade(quizAttempt: QuizAttempt) {
    return this.httpClient.put(this.baseRoute + `/UpdateQuizAttemptGrade`, quizAttempt);
  }

  public getQuizAttemptsForUser() {
    return this.httpClient.get(this.baseRoute);
  }
}
