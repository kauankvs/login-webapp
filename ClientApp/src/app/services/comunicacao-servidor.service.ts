import { HttpClient, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, map, Observable } from 'rxjs';
import { UsuarioDisplay } from '../interfaces/usuario-display';

@Injectable({
  providedIn: 'root'
})
export class ComunicacaoServidorService {
  private SERVER_URL: string = "https://localhost:7214/usuario";
  private usuarioDisplay: UsuarioDisplay | undefined ;

  constructor(private httpClient: HttpClient) { }

  registrarHttp(formData: FormData)
  {
    this.httpClient.post(this.SERVER_URL + "/registrar", formData, { withCredentials: true }).subscribe(
      (res) => console.log(res),
      (err) => console.log(err)
    )
  }

  loginHttp(formData: FormData)
  {
    this.httpClient.post(this.SERVER_URL + "/login", formData, { withCredentials: true }).subscribe(
      (res) => console.log(res),
      (err) => console.log(err)
    )
  };

  selecionarPerfilHttp(): Observable<UsuarioDisplay>
  {
    return this.httpClient.get<UsuarioDisplay>(this.SERVER_URL + "/minha-conta", { withCredentials: true } ).pipe(
      map(
        (data: UsuarioDisplay) =>
        {
          return data;
        }),
      catchError(
        (err, caught) =>
        {
          console.error(err);
          throw err;
        })
    );
  };
}
