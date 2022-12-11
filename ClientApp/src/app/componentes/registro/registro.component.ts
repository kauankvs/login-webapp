import { Component, OnInit } from '@angular/core';
import { NgModule } from '@angular/core';
import { FormBuilder } from '@angular/forms';

@Component({
  selector: 'app-registro',
  templateUrl: './registro.component.html',
  styleUrls: ['./registro.component.css']
})
export class RegistroComponent implements OnInit {

  modeloUsuario = this.formBuilder.group({
    nome: '',
    sobrenome: '',
    email: '',
    dataDeNascimento: '',
    senha: ''
  });

  constructor(private formBuilder: FormBuilder) { }

  ngOnInit(): void { }

  onSubmit(): void
  {
    console.log();
  }
}
