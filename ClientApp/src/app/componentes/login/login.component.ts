import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { empty } from 'rxjs';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  formulario = this.formBuilder.group({
    email: null,
    senha: null
  });

  constructor(private formBuilder: FormBuilder) { }

  ngOnInit(): void {
  }

  onSubmit(): void {
    console.log(this.formulario.value)
  }

}
