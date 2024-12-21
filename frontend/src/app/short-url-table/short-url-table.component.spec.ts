import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ShortUrlTableComponent } from './short-url-table.component';

describe('ShortUrlTableComponent', () => {
  let component: ShortUrlTableComponent;
  let fixture: ComponentFixture<ShortUrlTableComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ShortUrlTableComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ShortUrlTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
