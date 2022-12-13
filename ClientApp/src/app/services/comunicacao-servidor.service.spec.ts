import { TestBed } from '@angular/core/testing';

import { CominicacaoServidorService } from './comunicacao-servidor.service';

describe('CominicacaoServidorService', () => {
  let service: CominicacaoServidorService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CominicacaoServidorService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
