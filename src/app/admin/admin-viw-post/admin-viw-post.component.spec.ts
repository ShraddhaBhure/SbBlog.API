import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminViwPostComponent } from './admin-viw-post.component';

describe('AdminViwPostComponent', () => {
  let component: AdminViwPostComponent;
  let fixture: ComponentFixture<AdminViwPostComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AdminViwPostComponent]
    });
    fixture = TestBed.createComponent(AdminViwPostComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
