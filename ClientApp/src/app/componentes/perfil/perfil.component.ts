import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { UsuarioDisplay } from '../../interfaces/usuario-display';
import { ComunicacaoServidorService } from '../../services/comunicacao-servidor.service';

@Component({
  selector: 'app-perfil',
  templateUrl: './perfil.component.html',
  styleUrls: ['./perfil.component.css']
})
export class PerfilComponent implements OnInit
{
  private readonly httpService: ComunicacaoServidorService;
  usuarioDisplay: UsuarioDisplay = {
    nome: '',
    sobrenome: '',
    email: '',
    idade: '',
    papel: '',
    dataDeCriacao: '',
  };


  constructor(private service: ComunicacaoServidorService)
  {
    this.httpService = service;
    this.httpService.selecionarPerfilHttp().subscribe((data) => this.usuarioDisplay = data);
  }

  ngOnInit(): void
  {
  }

  
}
