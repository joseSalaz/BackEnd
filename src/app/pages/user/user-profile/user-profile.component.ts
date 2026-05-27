import { Component, OnInit, Input } from '@angular/core';
import { PersonaService } from '../../../Service/persona.service';
import { Persona } from '../../../Interface/persona';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.scss']
})
export class UserProfileComponent implements OnInit {
  @Input() usuarioId!: number;

  persona: Persona = {
    idPersona: 0,
    nombre: '',
    apellidoPaterno: '',
    apellidoMaterno: '',
    correo: '',
    tipoDocumento: '',
    numeroDocumento: '',
    telefono: ''
  };
  editando = false;

  constructor(private personaService: PersonaService) { }

  ngOnInit(): void {
    if (this.usuarioId) {
      this.cargarDatosPersona();
    }
  }

  cargarDatosPersona(): void {
    this.personaService.obtenerPersonaPorId(this.usuarioId).subscribe({
      next: (persona) => this.persona = persona,
      error: (error) => console.error('Error al obtener datos de la persona:', error)
    });
  }

  toggleEditar(): void {
    this.editando = !this.editando;
    if (!this.editando) {
      this.cargarDatosPersona();
    }
  }

  actualizarUsuario(): void {
    if (!this.persona.idPersona) return;

    this.personaService.actualizarPersona(this.persona).subscribe({
      next: () => {
        Swal.fire({
          title: 'Éxito',
          text: 'Datos actualizados correctamente',
          icon: 'success'
        }).then(() => {
          this.editando = false;
          this.cargarDatosPersona();
        });
      },
      error: () => {
        Swal.fire({
          title: 'Error',
          text: 'No se pudo actualizar la información',
          icon: 'error'
        });
      }
    });
  }
}