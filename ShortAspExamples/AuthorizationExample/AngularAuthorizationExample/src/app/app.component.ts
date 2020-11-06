import { Component } from '@angular/core';
import { AuthService } from './services/auth.service';
import { error } from '@angular/compiler/src/util';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  public get isLoggedIn(): boolean {
    return this.as.isAuthenticated()
  }

  constructor(private as: AuthService) { }

  login(email: string, pass: string) {
    this.as.login(email, pass).subscribe(
      res => {

      }, error => {
        alert("Wrong login or password!")
      })
  }

  logout() {
    this.as.logout();
  }
}
