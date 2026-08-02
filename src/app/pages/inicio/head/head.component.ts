import { ChangeDetectorRef, Component, HostListener, OnInit } from '@angular/core';
import { AuthService } from '../../../Service/auth.service';
import { NavigationError, Router } from '@angular/router';
import { sesioncosntans } from '../../../constans/sesion.constans';
import { Console, error, log } from 'console';
import { CarroService } from '../../../Service/carro.service';
import { ItemCarrito } from '../../../Interface/carrito';
import { UsuarioGoogle } from '../../../Interface/usuario';
import { UsuarioRegistradoResponse } from '../../../Interface/usuarioRegistradoResponse';
import { Persona } from '../../../Interface/persona';


@Component({
  selector: 'app-head',
  templateUrl: './head.component.html',
  styleUrls: ['./head.component.scss']
})
export class HeadComponent implements OnInit {
  vernombre: boolean = false;
  displayname: string = "";
  mostrarCarrito = false;
  totalItems: number = 0;
  isMenuVisible: boolean = false;
  isProcessing: boolean = false;
  isMobileMenuOpen = false;


  constructor(
    private authService: AuthService,
    private router: Router,
    private carroService: CarroService,
  ) { }

  ngOnInit(): void {

    this.totalItems = this.carroService.obtenerCantidadProductos();
    this.carroService.itemsCarrito.subscribe((items: ItemCarrito[]) => {
      this.totalItems = items.reduce((acc, item) => acc + Number(item.cantidad), 0);
    });

    this.authService.sesion$.subscribe(userProfile => {
      if (userProfile && userProfile.usu && userProfile.usu.length > 0) {
        // Asumiendo que 'usu' es un arreglo y que los datos de perfil están en el primer elemento
        const profileData = userProfile.usu[0];
        this.vernombre = !!profileData.name;
        this.displayname = profileData.name || '';
        this.registrarOVerificarUsuario(profileData);
      } else {
        this.vernombre = false;
        this.displayname = '';
      }
    });

  }


  toggleMobileMenu(): void {
    this.isMobileMenuOpen = !this.isMobileMenuOpen;
  }


  registrarOVerificarUsuario(profileData: any) {
    if (this.isProcessing) return; // Previene ejecuciones duplicadas

    this.isProcessing = true; // Estado de carga activo
    const usuarioParaRegistrar = {
      correo: profileData.email,
      sub: profileData.sub,
    };

    this.authService.verificarUsuario(usuarioParaRegistrar).subscribe({
      next: (usuarioRegistrado: Persona) => {
        localStorage.setItem('usuarioData', JSON.stringify(usuarioRegistrado));
        // console.log('ID del usuario:', usuarioRegistrado.idPersona);
        this.isProcessing = false; // Restablece el estado de carga
      },
      error: (error) => {
        console.error('Hubo un error al registrar o verificar al usuario', error);
        this.isProcessing = false; // Restablece el estado de carga
      }
    });
  }


  checkSession(): void {
    const userProfile: any = this.authService.getProfile();
  }

  @HostListener('document:click', ['$event'])
  onDocumentClick(event: Event): void {
    this.isMenuVisible = false;
  }

  toggleMenu(event: MouseEvent): void {
    // Evita que el evento de clic se propague a elementos superiores
    event.stopPropagation();

    // Alterna la visibilidad del menú desplegable
    this.isMenuVisible = !this.isMenuVisible;
  }


  onSelectOption(option: string): void {
    this.isMenuVisible = false;
    if (option === 'miperfil') {
      this.router.navigate(['/user']);
    } else if (option === 'cerrarSesion') {
    
      localStorage.removeItem('usuarioData');
  
      this.authService.logout();
    
      this.router.navigate(['/inicio']).then(() => {
        window.location.reload();
      });
    }
  }


  toggleCarrito(event: Event) {
    event.stopPropagation();
    this.mostrarCarrito = !this.mostrarCarrito;
  }

  onClick() {
    
    this.authService.logout();
    this.router.navigate(['/inicio']).then(() => {
      window.location.reload();
    });
  }

}



