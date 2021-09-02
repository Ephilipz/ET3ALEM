import {HttpClient} from '@angular/common/http';
import {Injectable} from '@angular/core';
import {environment} from 'src/environments/environment';
import {ChangePasswordVM} from '../models/ChangePasswordVM.entity';

@Injectable({
  providedIn: 'root'
})
export class ProfileService {

  constructor(private http: HttpClient) {
  }

  private baseRoute = environment.baseUrl + '/api/Profile';

  public changePassword(vm: ChangePasswordVM) {
    return this.http.post<any>(this.baseRoute + '/ChangePassword', vm);
  }

  public getUserEmail() {
    return this.http.get<any>(this.baseRoute + '/GetUserEmail');
  }
}
