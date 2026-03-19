import { Component, EventEmitter, Output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Empleado } from '../../models/empleado.model';

@Component({
  selector: 'app-empleado-form',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './empleado-form.component.html',
  styleUrls: ['./empleado-form.component.css']
})
export class EmpleadoFormComponent {
  @Output() empleadoAgregado = new EventEmitter<Empleado>();

  nombre = '';
  apellido = '';
  activo = true;

  agregar(): void {
    if (!this.nombre.trim() || !this.apellido.trim()) return;

    const nuevo: Empleado = {
      id: Date.now(),
      nombre: this.nombre.trim(),
      apellido: this.apellido.trim(),
      activo: this.activo
    };

    this.empleadoAgregado.emit(nuevo);
    this.nombre = '';
    this.apellido = '';
    this.activo = true;
  }
}