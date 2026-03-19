import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Empleado } from '../../models/empleado.model';

@Component({
  selector: 'app-empleado-lista',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './empleado-lista.component.html',
  styleUrls: ['./empleado-lista.component.css']
})
export class EmpleadoListaComponent {
  @Input() empleados: Empleado[] = [];
}