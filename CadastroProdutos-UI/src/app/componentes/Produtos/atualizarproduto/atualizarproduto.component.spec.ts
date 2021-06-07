import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AtualizarprodutoComponent } from './atualizarproduto.component';

describe('AtualizarprodutoComponent', () => {
  let component: AtualizarprodutoComponent;
  let fixture: ComponentFixture<AtualizarprodutoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AtualizarprodutoComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AtualizarprodutoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
