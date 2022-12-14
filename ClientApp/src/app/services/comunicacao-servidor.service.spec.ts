import { TestBed } from '@angular/core/testing';

import { ComunicacaoServidorService } from './comunicacao-servidor.service';

describe('CominicacaoServidorService', () => {
  let service: ComunicacaoServidorService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ComunicacaoServidorService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
