import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EditHelferComponent } from './editHelfer.component';

describe('CreateHelferComponent', () => {
  let component: EditHelferComponent;
  let fixture: ComponentFixture<EditHelferComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EditHelferComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EditHelferComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
