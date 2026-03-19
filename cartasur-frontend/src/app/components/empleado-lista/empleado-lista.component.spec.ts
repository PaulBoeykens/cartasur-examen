import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EmpleadoListaComponent } from './empleado-lista.component';

describe('EmpleadoListaComponent', () => {
  let component: EmpleadoListaComponent;
  let fixture: ComponentFixture<EmpleadoListaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EmpleadoListaComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(EmpleadoListaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
