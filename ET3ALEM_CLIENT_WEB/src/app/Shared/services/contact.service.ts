import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { environment } from "../../../environments/environment";

@Injectable({
  providedIn: 'root'
})
export class ContactService {

  private baseUrl = environment.baseUrl + '/api/ContactUs'
  constructor(private http: HttpClient) {
  }

  public submitContactForm(formValue: any) {
    return this.http.post<any>(this.baseUrl, formValue);
  }
  public getContactUsMessages() {
    return this.http.get<any>(this.baseUrl + '/GetAll');
  }
}
