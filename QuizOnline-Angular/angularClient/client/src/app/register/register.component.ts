import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { UserService } from '../service/user.service';
import { first } from 'rxjs/operators';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  public formData: any = {};
  public showMessage: boolean = false;
  FirstName: string = '';
  LastName: string = '';
  Email: string = '';
  UserName: string = '';
  PasswordHash: string = '';
  PhoneNumber: string = '';
  constructor(private router: Router,private userService: UserService) {
    
  }

  ngOnInit(): void {
  }
  
  onClose(): void {
   
    this.router.navigate([''])
      .then(() => {
        window.location.reload();
      });
  }
  registerUser(formdata: NgForm) {
    
  
    this.formData = formdata.value;
    this.showMessage = true;

    this.userService.register(this.formData)
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
