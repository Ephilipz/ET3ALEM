import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { QuizAttempt } from '../Model/quiz-attempt';

@Injectable({
  providedIn: 'root'
})
export class QuizAttemptService {

  private baseRoute = environment.baseUrl + '/api/QuizAttempt';

  constructor(private httpClient: HttpClient) { }

  public createQuizAttempt(quizAttempt: QuizAttempt): Observable<QuizAttempt> {
    return this.httpClient.post<QuizAttempt>(this.baseRoute, quizAttempt);
  }

  public getQuizAttemptsForQuiz(quizId: number) {
    return this.httpClient.get<Array<QuizAttempt>>(this.baseRoute + '/GetQuizAttemptsForQuiz/' + quizId);
  }

  public updateQuizAttempt(quizAttempt: QuizAttempt) {
    return this.httpClient.put(this.baseRoute + `/${quizAttempt.Id}`, quizAttempt);
  }
}
