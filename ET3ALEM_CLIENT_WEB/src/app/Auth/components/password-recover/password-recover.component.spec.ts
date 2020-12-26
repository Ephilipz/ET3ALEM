import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { PasswordRecoverComponent } from './password-recover.component';

describe('PasswordRecoverComponent', () => {
  let component: PasswordRecoverComponent;
  let fixture: ComponentFixture<PasswordRecoverComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ PasswordRecoverComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PasswordRecoverComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
