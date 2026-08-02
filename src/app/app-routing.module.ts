import { Component, NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { InicioComponent } from './pages/inicio/inicio.component';
import { SignUpComponent } from './pages/auth/sign-up/sign-up.component';
import { LogInComponent } from './pages/auth/log-in/log-in.component';
import { canActivate, redirectUnauthorizedTo } from '@angular/fire/auth-guard';
import { DetalleProductoComponent } from './pages/detalle-producto/detalle-producto.component';
import { AutorComponent } from './autor/autor.component';
import { UserComponent } from './pages/user/user.component';
import { DetalleVentaComponent } from './pages/detalle-venta/detalle-venta.component'
import { PagoComponent } from './pages/detalle-venta/pago/pago.component';
import { CategoriaComponent } from './pages/categoria/categoria.component';
import { PagoGuard } from './Guard/pago.guard';
import { RespuestasComponent } from './pages/detalle-venta/respuestas/respuestas.component';
import { SubcategoriaComponent } from './pages/categoria/subcategoria/subcategoria.component';
import { FiltradorComponent } from './pages/inicio/head/filtrador/filtrador.component';
import { EstadoPedidoModalComponent } from './pages/user/detalle-pedido/detalle-pedido.component';
import { DetalleVentaModalComponent } from './pages/user/detalle-venta-modal/detalle-venta-modal.component';




const routes: Routes = [
  { path: '', pathMatch: 'full', redirectTo: '/inicio' },
  { path: 'inicio', component: InicioComponent },
  {path:'detalle-producto/:id', component:DetalleProductoComponent},
  {path:'autor', component:AutorComponent},
  {path:'user',component:UserComponent},
  {path:'detalle-venta',component:DetalleVentaComponent},
  { path: 'detalle-venta/:paymentId/:PayerID', component: DetalleVentaComponent },
  {path:'pago',component:PagoComponent},
  {path: 'categoria/:idCategoria/libros',component: CategoriaComponent},
  {path:'subcategoria/:idSubCategoria/libros',component:SubcategoriaComponent},
  { path: 'pago', component: PagoComponent, canActivate: [PagoGuard] },
  {path:'respuesta',component:RespuestasComponent},
  { path: 'user/detalle-pedido', component: EstadoPedidoModalComponent },
  { path: 'user/detalle-venta-modal', component: DetalleVentaModalComponent },

  {
    path: 'auth',
    children: [
      { path: 'sign-up', component: SignUpComponent },
      { path: 'log-in', component: LogInComponent }
    ]
  },

  // Agregar una ruta para redirigir al inicio si la ruta no está definida
  { path: '**', redirectTo: '/inicio' }
  
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
