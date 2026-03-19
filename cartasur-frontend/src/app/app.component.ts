import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EmpleadoFormComponent } from './components/empleado-form/empleado-form.component';
import { EmpleadoListaComponent } from './components/empleado-lista/empleado-lista.component';
import { ApiDummyService } from './services/api-dummy.service';
import { Empleado } from './models/empleado.model';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, EmpleadoFormComponent, EmpleadoListaComponent],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  empleados: Empleado[] = [];
  dummyData: any = null;
  dummyError = false;

  constructor(private apiDummyService: ApiDummyService) {}

  ngOnInit(): void {
    this.apiDummyService.getDummyData().subscribe({
      next: (data) => this.dummyData = data,
      error: () => this.dummyError = true
    });
  }

  agregarEmpleado(empleado: Empleado): void {
    this.empleados = [...this.empleados, empleado];
  }

  get activos(): Empleado[] {
    return this.empleados.filter(e => e.activo);
  }

  get inactivos(): Empleado[] {
    return this.empleados.filter(e => !e.activo);
  }
}