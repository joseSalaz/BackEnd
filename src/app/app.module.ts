import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { BrowserModule, provideClientHydration } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { InicioComponent } from './pages/inicio/inicio.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { CarruselComponent } from './pages/inicio/carrusel/carrusel.component';
import { HeaderComponent } from './pages/inicio/header/header.component';
import { HeadComponent } from './pages/inicio/head/head.component';
import { DetalleProductoComponent } from './pages/detalle-producto/detalle-producto.component';
import { FooterComponent } from './pages/inicio/footer/footer.component';
import { SignUpComponent } from './pages/auth/sign-up/sign-up.component';
import { LogInComponent } from './pages/auth/log-in/log-in.component';
import { ReactiveFormsModule } from '@angular/forms';
import { initializeApp, provideFirebaseApp } from '@angular/fire/app';
import { getAuth, provideAuth } from '@angular/fire/auth';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';
import { AutorComponent } from './autor/autor.component';
import {  withFetch } from '@angular/common/http';
import { CarroComponent } from './pages/detalle-venta/carro/carro.component';
import { UserComponent } from './pages/user/user.component';
import { CategoriaComponent } from './pages/categoria/categoria.component';
import { OAuthModule } from 'angular-oauth2-oidc';
import { DetalleVentaComponent } from './pages/detalle-venta/detalle-venta.component';
import { PagoComponent } from './pages/detalle-venta/pago/pago.component';
import { RespuestasComponent } from './pages/detalle-venta/respuestas/respuestas.component';
import { ResBusquedadComponent } from './pages/inicio/head/res-busquedad/res-busquedad.component';
import { SubcategoriaComponent } from './pages/categoria/subcategoria/subcategoria.component';
import { FiltradorComponent } from './pages/inicio/head/filtrador/filtrador.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { register } from 'swiper/element/bundle';
import { CommonModule } from '@angular/common';
import { UserProfileComponent } from './pages/user/user-profile/user-profile.component';
import { UserOrdersComponent } from './pages/user/user-orders/user-orders.component';
import { UserAddressComponent } from './pages/user/user-address/user-address.component';
import { DetalleVentaModalComponent } from './pages/user/detalle-venta-modal/detalle-venta-modal.component';
import { EstadoPedidoModalComponent } from './pages/user/detalle-pedido/detalle-pedido.component';
import { FiltroComponent } from './pages/categoria/filtro/filtro.component';
import { UserFavComponent } from './pages/user/user-fav/user-fav.component';
// register Swiper custom elements
register();



@NgModule({ declarations: [
        AppComponent,
        InicioComponent,
        CarruselComponent,
        HeaderComponent,
        HeadComponent,
        SignUpComponent,
        LogInComponent,
        DetalleProductoComponent,
        FooterComponent,
        AutorComponent,
        CarroComponent,
        UserComponent,
        CategoriaComponent,
        DetalleVentaComponent,
        PagoComponent,
        RespuestasComponent,
        ResBusquedadComponent,
        SubcategoriaComponent,
        FiltradorComponent,
        DetalleVentaModalComponent,
        EstadoPedidoModalComponent,
        UserProfileComponent,
        UserOrdersComponent,
        UserAddressComponent,
        DetalleVentaModalComponent,
        FiltroComponent,
        UserFavComponent,
    ],
    bootstrap: [AppComponent], imports: [
        BrowserAnimationsModule,
        BrowserModule,
        AppRoutingModule,
        NgbModule,
        ReactiveFormsModule,
        FormsModule,
        
        OAuthModule.forRoot()], providers: [
        provideClientHydration(),
        provideAnimationsAsync(),
        provideHttpClient(withInterceptorsFromDi()),
        provideHttpClient(withFetch())
    ], 
    
    schemas:[CUSTOM_ELEMENTS_SCHEMA],
    })
export class AppModule { }