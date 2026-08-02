import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from '../../Service/auth.service';
import { VentaService } from '../../Service/venta.service';
import { DetalleVenta } from '../../Interface/detalle_venta';
import { DetalleVentaModalComponent } from './detalle-venta-modal/detalle-venta-modal.component';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.scss']
})
export class UserComponent implements OnInit {
  @ViewChild(DetalleVentaModalComponent) detalleVentaModalRef!: DetalleVentaModalComponent;
  
  usuarioId = 0;
  currentSection: string = 'profile';
  
  // Información de perfil de sesión
  vernombre: boolean = true;
  displayname: string = "";
  photoURL: string = "";
  email: string = "";

  // Estados compartidos para los modales globales de ventas
  detallesVenta: DetalleVenta[] = [];
  detalleVentaSeleccionado: number | null = null;

  constructor(
    private authService: AuthService,
    private ventaService: VentaService,
    private router: Router,
    private route: ActivatedRoute
  ) { }

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      if (params['section']) {
        this.currentSection = params['section'];
      }
    });

    const usuarioData = typeof window !== 'undefined' && localStorage.getItem('usuarioData')
      ? JSON.parse(localStorage.getItem('usuarioData') || '{}')
      : {};
    
    this.usuarioId = this.authService.getUsuarioId() || usuarioData.idPersona || 0;

    this.authService.sesion$.subscribe(userProfile => {
      if (userProfile && userProfile.usu && userProfile.usu.length > 0) {
        const profileData = userProfile.usu[0];
        this.vernombre = !!profileData.name;
        this.displayname = profileData.name || '';
        this.email = profileData.email;
        this.photoURL = profileData.picture;
      } else {
        this.vernombre = false;
        this.displayname = '';
      }
    });

    this.checkSession();
  }

  mostrarSeccion(seccion: string): void {
    this.currentSection = seccion;
  }

  checkSession(): void {
    const userProfile: any = this.authService.getProfile();
    if (userProfile && userProfile['name']) {
      this.vernombre = true;
      this.displayname = userProfile.name;
    }
  }

  prevpage(): void {
    this.router.navigate(['/inicio']);
  }

  // Captura el ID enviado por el hijo de pedidos y procesa la apertura del modal global
  abrirModalVentaGlobal(idVenta: number): void {
    this.ventaService.obtenerDetallesVenta(idVenta).subscribe({
      next: (detalles) => {
        this.detallesVenta = detalles;
        setTimeout(() => {
          if (this.detalleVentaModalRef) {
            this.detalleVentaModalRef.abrirModal();
          }
        }, 150);
      },
      error: (error) => console.error('🚨 Error al obtener detalles de venta:', error)
    });
  }

  irAEstadoPedido(idDetalleVentas: number | undefined): void {
    if (!idDetalleVentas) return;
    this.detalleVentaSeleccionado = idDetalleVentas;
  }

  onCerrarEstadoPedido(): void {
    this.detalleVentaSeleccionado = null;
  }
}