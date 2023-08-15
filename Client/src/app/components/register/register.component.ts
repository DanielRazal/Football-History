import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import UserDto from 'src/app/models/userDTO';
import { SwalService } from 'src/app/services/swal.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {

  registerForm!: FormGroup;
  selectedPhoto: File | undefined;

  constructor(private formBuilder: FormBuilder, private userService: UserService
    , private router: Router, private swalService: SwalService) { }

  ngOnInit(): void {
    this.registerForm = this.formBuilder.group({
      userName: [''],
      password: [''],
      firstName: [''],
      lastName: [''],
      email: [''],
      photo: [''],
      acceptedTerms: [false, Validators.requiredTrue]
    });
  }

  onPhotoSelected(event: any) {
    if (event.target.files && event.target.files.length > 0) {
      this.selectedPhoto = event.target.files[0];
    }
  }

  onSubmit() {
    const userDTO = this.registerForm.value as UserDto;
    const formData = this.userService.fromData(userDTO, this.selectedPhoto);

    this.userService.register(formData).subscribe((res) => {
      this.swalService.success(userDTO.firstName + ' ' + userDTO.lastName, res.message,
        `Status code: ${res.statusCode}`);
      this.router.navigate(['login']);
      this.registerForm.reset();

    }, (error) => {
      if (error.status === 401) {
        this.swalService.error('Error', error.error, `Status code: ${error.status}`);
      } else if (error.error && error.error.errors) {
        const errorKeys = Object.keys(error.error.errors);

        errorKeys.forEach((key) => {
          const errorMessage = error.error.errors[key][0];
          this.swalService.error('Error', errorMessage, `Status code: ${error.status}`);
        });
      }
    });
  }

}
