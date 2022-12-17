import { HttpClient, HttpHeaders, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { UsuarioDisplay } from '../interfaces/usuario-display';

@Injectable({
  providedIn: 'root'
})
export class ComunicacaoServidorService {
  private SERVER_URL: string = "https://localhost:7214/usuario";
  private usuarioDisplay?: UsuarioDisplay;
  headers: HttpHeaders = new HttpHeaders();
  
  constructor(private httpClient: HttpClient) {
    this.headers
      .set('Access-Control-Allow-Credentials', 'true')
      .set('Access-Control-Allow-Headers', 'X-Requested-With,content-type')
      .set('Access-Control-Allow-Methods', 'GET, POST, OPTIONS, PUT, PATCH, DELETE')
  }

  registrarHttp(formData: FormData)
  {
    this.headers.set('Access-Control-Allow-Origin', 'https://localhost:7214/usuario/registrar');

    this.httpClient.post(this.SERVER_URL + "/registrar", formData, { headers: this.headers }).subscribe(
      (res) => console.log(res),
      (err) => console.log(err)
    )
  }

  loginHttp(formData: FormData)
  {
    this.headers.set('Access-Control-Allow-Origin', 'https://localhost:7214/usuario/login');

    this.httpClient.post(this.SERVER_URL + "/login", formData, { headers: this.headers }).subscribe(
      (res) => console.log(res),
      (err) => console.log(err)
    )
  };

  selecionarPerfilHttp(): Observable<UsuarioDisplay> {
    this.headers.set('Access-Control-Allow-Origin', 'https://localhost:7214/usuario/minha-conta');
    return this.httpClient.get<UsuarioDisplay>(this.SERVER_URL + "/minha-conta", { headers: this.headers });
  }
}
