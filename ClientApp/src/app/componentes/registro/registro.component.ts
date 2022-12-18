import { Component, OnInit } from '@angular/core';
import { NgModule } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { empty } from 'rxjs';
import { ComunicacaoServidorService } from '../../services/comunicacao-servidor.service';

@Component({
  selector: 'app-registro',
  templateUrl: './registro.component.html',
  styleUrls: ['./registro.component.css']
})
export class RegistroComponent implements OnInit {

  private httpService: ComunicacaoServidorService;
  formulario: FormGroup = this.formBuilder.group({
    nome: null,       
    sobrenome: null,  
    email: null,      
    idade: null,     
    senha: null      
  });

  constructor(private formBuilder: FormBuilder, httpService: ComunicacaoServidorService) {
    this.httpService = httpService;
  }

  ngOnInit(): void { }

  onSubmit(): void
  {
    const formData = new FormData()
    formData.append("nome", this.formulario.get("nome")?.value);
    formData.append("sobrenome", this.formulario.get("sobrenome")?.value);
    formData.append("email", this.formulario.get("email")?.value);
    formData.append("idade", this.formulario.get("idade")?.value);
    formData.append("senha", this.formulario.get("senha")?.value);
    this.httpService.registrarHttp(formData);
  }
}
