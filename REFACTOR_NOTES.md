# Refactor: UnitOfWork + inyección de dependencias real

## Qué cambió

### 1. Nuevo proyecto `UnitOfWork/`
- `IUnitOfWork.cs`: contrato con un repositorio por entidad + control de transacción
  (`BeginTransactionAsync`, `CommitAsync`, `RollbackAsync`, `ExecuteInTransactionAsync`).
- `UnitOfWork.cs`: implementación. Recibe **un solo** `LibreriaSaberContext` (inyectado por
  DI, Scoped) y crea cada repositorio pasándole ese mismo contexto, de forma perezosa.

### 2. `GenericRepository<TEntity>`
Ya NO hace `new LibreriaSaberContext()`. El contexto se recibe por constructor. Esto es lo
que permite que varios repositorios compartan conexión/transacción.

### 3. Los 27 repositorios concretos (`Repositorio/*.cs`)
Cada uno ahora tiene:
```csharp
public XRepository(DBModel.DB.LibreriaSaberContext context) : base(context) { }
```

### 4. 26 clases `Bussines`
Ya no hacen `_IXRepository = new XRepository();`. Reciben `IUnitOfWork unitOfWork` por
constructor y toman su repositorio de ahí: `_IXRepository = _unitOfWork.Prop;`. El resto del
código de cada clase **no cambió** (mismos nombres de campo, mismos métodos).

### 5. Ejemplo insignia de transacción/rollback real
`VentaBussines.RegistrarVentaConDetalleAsync(...)` reemplaza la lógica que antes vivía en
`DetalleVentaController.RegistrarVentaYDetalle` (persona → caja del día → venta → actualizar
caja → validar/descontar stock por ítem → crear detalle). Todo corre dentro de
`_unitOfWork.ExecuteInTransactionAsync(...)`: si cualquier paso falla (por ejemplo, stock
insuficiente en el ítem 3 de 5), se revierte TODO, incluyendo lo que ya se había guardado en
caja y en los ítems 1 y 2 del kardex dentro de esa misma llamada. El controller quedó como un
simple `try/catch` que llama a la capa de negocio (ya no toca repositorios).

### 6. `Program.cs` / `.csproj`
- `builder.Services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();`
- `Bussines.csproj` y `API.csproj` referencian el nuevo proyecto `UnitOfWork`.
- Proyecto agregado a `LibreriaSaber.sln`.

## Qué NO se tocó (pendiente, mismo patrón a replicar)

`MercadoPagoController` y `PaypalController` siguen inyectando `IKardexRepository` /
`ICajaRepository` directamente y actualizando stock/caja en bucle sin transacción — igual que
tenía `DetalleVentaController` antes del refactor. Van a compilar y funcionar igual que hoy
(la DI del `DbContext` los sigue resolviendo bien), pero tienen el mismo riesgo de dejar datos
a medias si algo falla a mitad de un webhook de pago.

**Recomendación:** mover esa lógica a un método en `VentaBussines` (o uno nuevo, p. ej.
`ConfirmarPagoYActualizarStockAsync`) envuelto en `_unitOfWork.ExecuteInTransactionAsync(...)`,
igual que se hizo en `RegistrarVentaConDetalleAsync`, y que el controller solo llame a ese
método. No lo hice en este pase porque son webhooks de pasarelas de pago reales (Mercado Pago /
PayPal) y preferí no tocarlos sin poder compilar y probar contra el flujo real de callbacks.

## Checklist para verificar localmente

1. `dotnet restore` en la solución.
2. `dotnet build` — revisar warnings de `using` sobrantes (quedaron algunos `using Repository;`
   sin uso en los `Bussines/*.cs`, son inofensivos pero se pueden limpiar).
3. Revisar `appsettings.json` / cadena de conexión (no se tocó).
4. Probar `POST /DetalleVenta/registrar-venta-detalle` con un carrito cuyo último ítem no tenga
   stock suficiente: la venta, la caja y los ítems anteriores del kardex NO deben quedar
   guardados (antes sí quedaban).
5. Si algo no compila por un `using` faltante en algún `Bussines/*.cs` puntual, es el único
   ajuste fino esperable: agregar `using UnitOfWork;` (ya se agrega automáticamente en los 26
   archivos tocados, pero si tienes otra clase Bussines con lógica similar, replica el patrón).
6. Opcional: quitar de `Program.cs` los `AddScoped<IXRepository, XRepository>()` que ya no use
   directamente ningún controller (los que siguen usando `MercadoPagoController` /
   `PaypalController` hay que dejarlos).

## Patrón a replicar en el resto de operaciones multi-entidad

```csharp
public async Task<TuResponse> TuOperacionAsync(TuRequest request)
{
    return await _unitOfWork.ExecuteInTransactionAsync(async () =>
    {
        // 1) _unitOfWork.RepoA.Create/Update/GetById(...)
        // 2) _unitOfWork.RepoB....
        // 3) si algo está mal: throw new InvalidOperationException("mensaje");
        // 4) return TuResponse construido con los resultados
    });
}
```

Cualquier `throw` dentro del bloque revierte automáticamente todo lo hecho en los pasos
anteriores (incluso los `SaveChanges` individuales de cada repo, porque ocurren dentro de la
misma transacción de base de datos abierta por `ExecuteInTransactionAsync`).
