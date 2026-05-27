import { Component, OnInit, Input, ChangeDetectorRef, OnChanges, SimpleChanges } from '@angular/core';
import { DireccionService } from '../../../Service/direccion.service';
import { Direccion } from '../../../Interface/direccion';
import Swal from 'sweetalert2';

declare var google: any;

@Component({
  selector: 'app-user-address',
  templateUrl: './user-address.component.html',
  styleUrls: ['./user-address.component.scss']
})
export class UserAddressComponent implements OnChanges {
  @Input() usuarioId!: number;

  direcciones: Direccion[] = [];
  mostrarModal = false;
  mensajeError: string = '';

  nuevaDireccion: Direccion = {
    idPersona: 0,
    direccion1: '',
    referencia: '',
    departamento: '',
    provincia: '',
    distrito: '',
    codigoPostal: '',
    esPredeterminada: false
  };

  constructor(
    private direccionService: DireccionService,
    private cdr: ChangeDetectorRef
  ) { }
  ngOnChanges(changes: SimpleChanges): void {
    throw new Error('Method not implemented.');
  }

  ngOnInit(): void {
    if (this.usuarioId) {
      this.nuevaDireccion.idPersona = this.usuarioId;
      this.cargarDirecciones();
    }
  }

  cargarDirecciones(): void {
    this.direccionService.getDireccionesByUsuario(this.usuarioId).subscribe({
      next: (data) => this.direcciones = data,
      error: (err) => console.error('Error al cargar direcciones:', err)
    });
  }

  validarFormulario(): void {
    this.mensajeError = '';
    if (!this.nuevaDireccion.direccion1.trim() ||
        !this.nuevaDireccion.distrito.trim() ||
        !this.nuevaDireccion.provincia.trim() ||
        !this.nuevaDireccion.departamento.trim()) {
      this.mensajeError = 'Por favor, completa todos los campos obligatorios.';
      return;
    }
    this.agregarDireccion();
  }

  agregarDireccion(): void {
    this.direccionService.createDireccion(this.nuevaDireccion).subscribe({
      next: () => {
        this.cargarDirecciones();
        this.cerrarModal();
        this.nuevaDireccion = {
          idPersona: this.usuarioId,
          direccion1: '',
          referencia: '',
          departamento: '',
          provincia: '',
          distrito: '',
          codigoPostal: '',
          esPredeterminada: false
        };

        Swal.fire({
          title: '✅ Dirección agregada',
          text: 'La dirección ha sido guardada correctamente.',
          icon: 'success',
          confirmButtonText: 'Ok'
        });
      },
      error: (err) => {
        console.error('Error al agregar dirección:', err);
        Swal.fire({
          title: '⚠️ Error',
          text: err.error || 'Hubo un problema al agregar la dirección.',
          icon: 'error',
          confirmButtonText: 'Ok'
        });
      }
    });
  }

  eliminarDireccion(idDireccion: number): void {
    Swal.fire({
      title: '¿Estás seguro?',
      text: 'Esta acción eliminará la dirección permanentemente.',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#d33',
      cancelButtonColor: '#3085d6',
      confirmButtonText: 'Sí, eliminar',
      cancelButtonText: 'Cancelar'
    }).then((result) => {
      if (result.isConfirmed) {
        this.direccionService.deleteDireccion(idDireccion).subscribe({
          next: () => {
            this.cargarDirecciones();
            Swal.fire({
              title: '✅ Dirección eliminada',
              icon: 'success',
              confirmButtonText: 'Ok'
            });
          },
          error: (err) => {
            let msg = 'Ocurrió un error al eliminar la dirección.';
            if (typeof err.error === 'string') { msg = err.error; }
            Swal.fire({
              title: '⚠️ No se puede eliminar',
              text: msg,
              icon: 'error',
              confirmButtonText: 'Ok'
            });
          }
        });
      }
    });
  }

  establecerPredeterminada(idDireccion: number): void {
    this.direccionService.setDireccionPredeterminada(idDireccion).subscribe({
      next: () => {
        this.cargarDirecciones();
        this.mostrarToast("✅ Dirección predeterminada actualizada correctamente.");
      },
      error: (err) => console.error('Error al establecer dirección predeterminada:', err)
    });
  }

  abrirModal(): void {
    this.mostrarModal = true;
    document.body.style.overflow = 'hidden';
    setTimeout(() => {
      this.inicializarMapa();
      if (typeof google !== 'undefined' && google.maps) {
        const mapaContenedor = document.getElementById('map');
        if (mapaContenedor) {
          google.maps.event.trigger(mapaContenedor, 'resize');
        }
      }
    }, 250);
  }

  cerrarModal(): void {
    this.mostrarModal = false;
    document.body.style.overflow = '';
    this.mensajeError = '';
    const pacContainer = document.querySelector('.pac-container');
    if (pacContainer) {
      pacContainer.remove();
    }
  }

  inicializarMapa(): void {
    const mapaContenedor = document.getElementById('map');
    const inputBusqueda = document.getElementById('map-search') as HTMLInputElement;

    if (!mapaContenedor) return;

    let coordenadasIniciales = { lat: -11.7772, lng: -75.5003 };

    const mapa = new google.maps.Map(mapaContenedor, {
      center: coordenadasIniciales,
      zoom: 15,
      mapTypeId: google.maps.MapTypeId.ROADMAP
    });

    const marcador = new google.maps.Marker({
      position: coordenadasIniciales,
      map: mapa,
      draggable: true
    });

    const geocoder = new google.maps.Geocoder();

    if (navigator.geolocation) {
      navigator.geolocation.getCurrentPosition(
        (pos) => {
          const loc = { lat: pos.coords.latitude, lng: pos.coords.longitude };
          mapa.setCenter(loc);
          marcador.setPosition(loc);
          this.obtenerDireccionDesdeCoordenadas(loc, geocoder);
        },
        () => this.obtenerDireccionDesdeCoordenadas(coordenadasIniciales, geocoder),
        { enableHighAccuracy: true, timeout: 5000 }
      );
    }

    if (inputBusqueda) {
      const autocomplete = new google.maps.places.Autocomplete(inputBusqueda, {
        fields: ['geometry', 'address_components', 'place_id', 'formatted_address'],
        types: ['geocode', 'establishment'],
        componentRestrictions: { country: 'pe' }
      });

      google.maps.event.addListenerOnce(mapa, 'idle', () => {
        autocomplete.bindTo('bounds', mapa);
      });

      autocomplete.addListener('place_changed', () => {
        const place = autocomplete.getPlace();

        if (!place || (!place.geometry && !place.place_id)) {
          this.mensajeError = "Debes seleccionar una opción del listado.";
          return;
        }

        this.mensajeError = '';

        if (!place.geometry?.location) {
          geocoder.geocode({ placeId: place.place_id }, (results: any, status: string) => {
            if (status === 'OK' && results[0]) {
              const geo = results[0];
              if (geo.geometry.viewport) {
                mapa.fitBounds(geo.geometry.viewport);
              } else {
                mapa.setCenter(geo.geometry.location);
                mapa.setZoom(16);
              }
              marcador.setPosition(geo.geometry.location);
              this.procesarComponentesDireccion(geo.address_components);
            }
          });
          return;
        }

        if (place.geometry.viewport) {
          mapa.fitBounds(place.geometry.viewport);
        } else {
          mapa.setCenter(place.geometry.location);
          mapa.setZoom(16);
        }
        marcador.setPosition(place.geometry.location);
        this.procesarComponentesDireccion(place.address_components);
      });
    }

    google.maps.event.addListener(mapa, 'click', (evento: any) => {
      marcador.setPosition(evento.latLng);
      this.obtenerDireccionDesdeCoordenadas(evento.latLng, geocoder);
    });

    google.maps.event.addListener(marcador, 'dragend', () => {
      this.obtenerDireccionDesdeCoordenadas(marcador.getPosition(), geocoder);
    });
  }

  procesarComponentesDireccion(componentesDireccion: any[]): void {
    if (!componentesDireccion) return;

    let calleNumero = '';
    let distrito = '';
    let provincia = '';
    let departamento = '';
    let codigoPostal = '';

    for (const componente of componentesDireccion) {
      const tipos = componente.types;

      if (tipos.includes('route')) {
        calleNumero = componente.long_name;
      } else if (tipos.includes('street_number')) {
        calleNumero += ' ' + componente.long_name;
      } else if (tipos.includes('locality') || tipos.includes('sublocality_level_1')) {
        distrito = componente.long_name;
      } else if (tipos.includes('administrative_area_level_2')) {
        provincia = componente.long_name;
      } else if (tipos.includes('administrative_area_level_1')) {
        let deptoTexto = componente.long_name;
        if (deptoTexto.toLowerCase().includes('gobierno regional de')) {
          deptoTexto = deptoTexto.replace(/gobierno regional de/i, '').trim();
        } else if (deptoTexto.toLowerCase().includes('gobierno regional del')) {
          deptoTexto = deptoTexto.replace(/gobierno regional del/i, '').trim();
        }
        departamento = deptoTexto;
      } else if (tipos.includes('postal_code')) {
        codigoPostal = componente.long_name;
      }
    }

    this.nuevaDireccion.direccion1 = calleNumero.trim() ? calleNumero.trim() : `Zona céntrica de ${distrito || provincia || 'la región'}`;
    this.nuevaDireccion.distrito = distrito;
    this.nuevaDireccion.provincia = provincia;
    this.nuevaDireccion.departamento = departamento;
    this.nuevaDireccion.codigoPostal = codigoPostal;

    this.cdr.detectChanges();
  }

  obtenerDireccionDesdeCoordenadas(latLng: any, geocoder: any): void {
    geocoder.geocode({ location: latLng }, (resultados: any, estado: string) => {
      if (estado === 'OK' && resultados[0]) {
        this.procesarComponentesDireccion(resultados[0].address_components);
        if (!this.nuevaDireccion.direccion1 || this.nuevaDireccion.direccion1.startsWith('Zona céntrica de')) {
          this.nuevaDireccion.direccion1 = resultados[0].formatted_address.split(',')[0];
          this.cdr.detectChanges();
        }
      }
    });
  }

  cerrarModalAlFondo(event: MouseEvent): void {
    if ((event.target as HTMLElement).classList.contains('modal-overlay')) {
      this.cerrarModal();
    }
  }

  mostrarToast(mensaje: string): void {
    const toast = document.createElement("div");
    toast.innerText = mensaje;
    toast.className = "fixed bottom-5 right-5 bg-green-500 text-white px-4 py-2 rounded shadow-md z-50 transition-opacity duration-300";
    document.body.appendChild(toast);
    
    setTimeout(() => {
      toast.style.opacity = '0';
      setTimeout(() => toast.remove(), 300);
    }, 3000);
  }
}