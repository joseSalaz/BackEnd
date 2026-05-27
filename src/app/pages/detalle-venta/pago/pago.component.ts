import { Component, OnInit } from '@angular/core';
import { CarroService } from '../../../Service/carro.service';
import { HttpClient } from '@angular/common/http';
import { PagoService } from '../../../Service/pago.service';
import { ActivatedRoute, Router, Routes } from '@angular/router';
import { AuthService } from '../../../Service/auth.service';
import { PersonaService } from '../../../Service/persona.service';
import Swal from 'sweetalert2';
import Notiflix from 'notiflix';
@Component({
  selector: 'app-pago',
  templateUrl: './pago.component.html',
  styleUrls: ['./pago.component.scss'] // Asegúrate de que la extensión sea .scss si estás usando SASS/SCSS
})
export class PagoComponent implements OnInit {

  isLoading = false;
  mostrarOpcionesEnvio: boolean = false;
  idDireccionSeleccionada: number | null = null;
  constructor(
    private carroService: CarroService,
    private activatedRoute: ActivatedRoute,
    private http: HttpClient,
    private pago: PagoService,
    private router: Router,
    private authService: AuthService,
    private personaService: PersonaService,
  ) {
    Notiflix.Loading.init({
      svgColor: '#0d6efd',
      clickToClose: false,
    });
  }

  ngOnInit(): void {
    this.activatedRoute.queryParams.subscribe(params => {
      // Obtener la dirección del servicio
      const direccionSeleccionada = this.carroService.getDireccionSeleccionada();
      if (direccionSeleccionada && direccionSeleccionada.idDireccion) {
        this.idDireccionSeleccionada = Number(direccionSeleccionada.idDireccion);

      } else {
        this.idDireccionSeleccionada = null;

      }

      const paymentId = params['paymentId'];
      const payerId = params['PayerID'];
      const preferenceId = params['preferenceId'];

      // Lógica de procesamiento de pagos...
      if (paymentId && payerId) {
        this.isLoading = true;
        this.pago.executePayment(paymentId, payerId).subscribe({
          next: (response) => {
            this.isLoading = false;
          },
          error: (error) => {
            console.error('Error al confirmar el pago de PayPal:', error);
            this.router.navigate(['']);
            this.isLoading = false;
          }
        });
      } else if (preferenceId) {
        this.isLoading = true;
        this.pago.executePaymentMercadoPago(preferenceId, '').subscribe({
          next: (response) => {
            this.isLoading = false;
          },
          error: (error) => {
            console.error('Error al confirmar el pago de Mercado Pago:', error);
            this.router.navigate(['']);
            this.isLoading = false;
          }
        });
      }
    });
  }

  toggleOpcionesEnvio(): void {
    this.mostrarOpcionesEnvio = !this.mostrarOpcionesEnvio;
  }
  validarDatos(datos: any): boolean {
    return datos?.nombre && datos?.apellidoMaterno && datos?.apellidoPaterno && datos?.correo && datos?.telefono && datos?.numeroDocumento;
  }
  procesarPago(metodo: 'paypal' | 'mercadoPago'): void {
    Notiflix.Loading.hourglass('Confirmando pago...');
    const idPersona = this.authService.getUsuarioId();

    if (!this.idDireccionSeleccionada) {
      Swal.fire({
        title: '⚠️ Atención',
        text: 'Debes seleccionar una dirección antes de proceder con el pago.',
        icon: 'warning',
        confirmButtonText: 'Ok',
        confirmButtonColor: '#d33'
      });
      Notiflix.Loading.remove();
      return;
    }

    this.personaService.obtenerPersonaPorId(idPersona).subscribe(
      (datos) => {
        if (!this.validarDatos(datos)) {
          Swal.fire({
            title: '⚠️ Atención',
            text: 'Es necesario que primero completes todos tus datos de usuario antes de proceder con el pago.',
            icon: 'warning',
            confirmButtonText: 'Completar mis Datos',
            confirmButtonColor: '#d33'
          }).then(() => {
            this.router.navigate(['/user']);
          });
          Notiflix.Loading.remove();
          return;
        }

        // ✅ Si los datos son válidos, proceder con el pago
        if (metodo === 'paypal') {
          this.carroService.enviarCarritoAlBackend(this.idDireccionSeleccionada!).subscribe({
            next: (response) => {
              const approvalUrl = response.approvalUrl;
              window.location.href = approvalUrl;
            },
            error: (error) => {
              console.error('Error al procesar el pago con PayPal:', error);
              Notiflix.Loading.remove();
            }
          });
        } else if (metodo === 'mercadoPago') {
          this.carroService.enviarCarritoAlBackendParaMercadoPago(this.idDireccionSeleccionada!).subscribe({
            next: (response) => {
              const urlDePago = response.urlDePago;
              window.location.href = urlDePago;
            },
            error: (error) => {
              console.error('Error al procesar el pago con Mercado Pago:', error);
              Notiflix.Loading.remove();
            }
          });
        }
      },
      (error) => {
        console.error('Error obteniendo datos del usuario:', error);
        Notiflix.Loading.remove();
      }
    );
  }


}