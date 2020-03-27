import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateHelferComponent } from './createHelfer.component';

describe('CreateHelferComponent', () => {
  let component: CreateHelferComponent;
  let fixture: ComponentFixture<CreateHelferComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CreateHelferComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateHelferComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
