import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HelferDetailComponent } from './helfer-detail.component';

describe('HelferDetailComponent', () => {
  let component: HelferDetailComponent;
  let fixture: ComponentFixture<HelferDetailComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HelferDetailComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HelferDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
