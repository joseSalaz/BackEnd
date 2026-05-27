import { Injectable } from '@angular/core';
import { AuthConfig, OAuthEvent, OAuthService } from 'angular-oauth2-oidc';
import { UsuarioGoogle } from '../Interface/usuario';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Router } from '@angular/router';
import { BehaviorSubject } from 'rxjs';
import { environment } from '../../environments/environment';


@Injectable({
  providedIn: 'root',
})
export class AuthService {

  private endPoint:string = environment.endPoint; 
  private apiUrl:string = this.endPoint + "Persona";

  private sesionSource = new BehaviorSubject<any>(null);

  sesion$ = this.sesionSource.asObservable();

  constructor(
    private oauthService: OAuthService,
    private http: HttpClient,
    private router: Router,
    )  
    {
    this.initLogin();
    this.oauthService.events.subscribe((event) => {
      if (event.type === 'token_received') {
        this.sesionSource.next(this.getProfile());
      } else if (event.type === 'logout') {
        this.sesionSource.next(null);
      }
    });
  }

  initLogin() {
    if (typeof window !== 'undefined') {
      const config: AuthConfig = {
        issuer: 'https://accounts.google.com',
        strictDiscoveryDocumentValidation: false,
        clientId:
          '682250868280-o0sg453iebedbuuria9clb4t3v2kna8m.apps.googleusercontent.com',
        redirectUri: window.location.origin + '/inicio',
        scope: 'openid profile email',
      };

      this.oauthService.configure(config);
      this.oauthService.setupAutomaticSilentRefresh();
      this.oauthService.loadDiscoveryDocumentAndTryLogin().then((result) => {
        if (result) {
          const profile = this.getProfile();
          if (profile) {
            localStorage.setItem('userProfile', JSON.stringify(profile));
            this.sesionSource.next(profile);
          }
        } else {
          this.restoreProfile();
        }
      });
    }
  }

  restoreProfile() {
    const profile = localStorage.getItem('userProfile');
    if (profile) {
      this.sesionSource.next(JSON.parse(profile));
    }
  }

  login() {
    const attemptedRoute = localStorage.getItem('redirectAfterLogin') || '/';
    this.oauthService.initLoginFlow();
    this.oauthService.events.subscribe((event) => {
      if (event.type === 'token_received') {
        // ... código para manejar la autenticación exitosa
        const redirectRoute = localStorage.getItem('redirectAfterLogin') || '/';
        localStorage.removeItem('redirectAfterLogin'); // Limpia la ruta de redirección
        this.router.navigate([redirectRoute]);
      }
    });
  }

  logout() {
    this.oauthService.logOut();
    localStorage.removeItem('userProfile');
    this.sesionSource.next(null);
  }

  getProfile() {
    const claims = this.oauthService.getIdentityClaims();
    if (!claims) {
      return null;
    }
    // Almacena todo el objeto de claims directamente en `usu`
    return {
      usu: [claims]  // Almacena todo el objeto de claims en un array dentro de `usu`
    };
  }
  public obtenerPerfilUsuario(): Observable<UsuarioGoogle> {
    const claims = this.oauthService.getIdentityClaims();
    if (!claims) {
      throw new Error('No hay claims de identidad disponibles');
    }
    const usuario: UsuarioGoogle = {
      correo: claims['email'],
      sub: claims['sub'],
    };
    return this.http.post<UsuarioGoogle>(`${this.apiUrl}/verificar`, usuario);
  }
  verificarUsuario(usuario: UsuarioGoogle): Observable<any> {
    return this.http.post(`${this.apiUrl}/verificar`, usuario);
  }
  getUsuarioId(): number {
    const usuarioData = localStorage.getItem('usuarioData');
    if (usuarioData) {
        const usuario = JSON.parse(usuarioData);
        return usuario.idPersona || 0;  // Devuelve 0 si no hay idPersona
    }
    return 0; // Devuelve 0 si no hay usuarioData en localStorage
}

}