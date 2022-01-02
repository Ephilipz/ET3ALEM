import {Component} from '@angular/core';
import {ActivatedRoute} from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'ET3ALLEM';

  constructor(private activatedRoute: ActivatedRoute) {
  }

  public isAuthRoute(): boolean {
    return window.location.toString().toLowerCase().indexOf('auth') != -1;
  }

  public noScrolling(): boolean {
    return this.isHomePage();
  }

  private isHomePage(): boolean {
    return window.location.pathname.trim() == '/';
  }
}
