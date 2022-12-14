import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { empty } from 'rxjs';
import { ComunicacaoServidorService } from '../../services/comunicacao-servidor.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  private httpService: ComunicacaoServidorService;
  formulario: FormGroup = this.formBuilder.group({
    email: null,
    senha: null
  });

  constructor(private formBuilder: FormBuilder, httpService: ComunicacaoServidorService) {
    this.httpService = httpService;
  }

  ngOnInit(): void {
  }

  onSubmit(): void {
    const formData = new FormData()
    formData.append("email", this.formulario.get("email")?.value);
    formData.append("senha", this.formulario.get("senha")?.value);
    this.httpService.loginHttp(formData);
  }

}
