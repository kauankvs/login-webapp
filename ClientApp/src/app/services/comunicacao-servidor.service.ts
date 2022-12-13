import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ComunicacaoServidorService {
  SERVER_URL: string = "https://localhost:7214";

  constructor(private httpClient: HttpClient) { }

  registrarHttp(formData: FormData) {
    this.httpClient.post<any>(this.SERVER_URL + "/registrar", formData).subscribe(
      (res) => console.log(res),
      (err) => console.log(err)
    )
  }
}
