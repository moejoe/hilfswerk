import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddEinsatzComponent } from './addEinsatz.component';

describe('CreateEinsatz', () => {
  let component: AddEinsatzComponent;
  let fixture: ComponentFixture<CreateEinsatzComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddEinsatzComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddEinsatzComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
