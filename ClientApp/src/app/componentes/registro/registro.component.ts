import { Component, OnInit } from '@angular/core';
import { NgModule } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { empty } from 'rxjs';

@Component({
  selector: 'app-registro',
  templateUrl: './registro.component.html',
  styleUrls: ['./registro.component.css']
})
export class RegistroComponent implements OnInit {


  formulario = this.formBuilder.group({
    nome: null,       
    sobrenome: null,  
    email: null,      
    dataDeNascimento: null,     
    senha: null      
  });

  constructor(private formBuilder: FormBuilder) { }

  ngOnInit(): void { }

  onSubmit(): void
  {
    console.log(this.formulario.value);
    this.formulario.reset()
  }
}
