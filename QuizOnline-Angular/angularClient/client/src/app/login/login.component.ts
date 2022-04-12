import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { UserService } from '../service/user.service';
import { first } from 'rxjs/operators';
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  public formData: any = {};
  public showMessage: boolean = false;

  UserName: string = '';
  PasswordHash: string = '';
  constructor(private router: Router, private userService: UserService) {

  }

  ngOnInit(): void {
  }

  onClose(): void {

    this.router.navigate([''])
      .then(() => {
        window.location.reload();
      });
  }
  userLogin(formdata: NgForm) {


    this.formData = formdata.value;
    this.showMessage = true;

    this.userService.login(this.formData)
      .pipe(first())
      .subscribe(
        data => {

          this.router.navigate([''])
            .then(() => {
              window.location.reload();
            });

        },
        error => {
          alert('register error')
          //this.alertService.error(error);
          //this.loading = false;
        });


  }
}
