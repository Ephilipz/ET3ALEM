import {Component, OnChanges, OnInit, SimpleChanges} from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';
import {ToastrService} from 'ngx-toastr';
import {AuthService} from 'src/app/auth/services/auth.service';

@Component({
  selector: 'app-main-menu',
  templateUrl: './main-menu.component.html',
  styleUrls: ['./main-menu.component.css']
})
export class MainMenuComponent implements OnInit {

  isLoggedIn: boolean = false;

  constructor(private auth: AuthService, private toastr: ToastrService, private router: Router) {
  }

  ngOnInit(): void {
    this.auth.loginStateChanged.subscribe(
      (isLoggedIn) => this.isLoggedIn = isLoggedIn
    )
  }

  logout() {
    this.auth.logout().subscribe(
      () => {
        this.isLoggedIn = this.auth.isLoggedIn();
        this.toastr.info('Logged Out');
      },
      (err) => {
      },
      () => {
        this.router.navigate(['']);
      });

  }

  routeContains(text: string): boolean {
    return this.router.url.toLowerCase().indexOf(text.toLowerCase()) != -1;
  }

}
