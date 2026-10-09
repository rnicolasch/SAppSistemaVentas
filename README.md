# :computer: Sistema de Ventas en C#

## :clipboard: Descripción

El **Sistema de Ventas** es una aplicación desarrollada en **C# para consola**, orientada a la gestión de productos, usuarios y ventas de un negocio.

El sistema permite realizar el inicio de sesión de usuarios, registrar productos, administrar usuarios, registrar ventas y consultar diferentes reportes.

## :rocket: Funcionalidades

-   :lock: **Login de usuarios**
-   :package: **Creación de productos**
-   :bust_in_silhouette: **Creación de usuarios**
-   :shopping_cart: **Registro de ventas**
-   :bar_chart: **Reporte de productos**
-   :moneybag: **Reporte de ventas**
-   :busts_in_silhouette: **Reporte de usuarios**
-   :door: **Salir del sistema**

----------

## :lock: 1. Login

El sistema inicia mostrando una pantalla de autenticación donde el usuario debe ingresar sus credenciales.

### Datos solicitados

-   :bust_in_silhouette: Nombre de usuario
-   :key: Contraseña

Si las credenciales son correctas, el sistema permite acceder al menú principal.

### :computer: Ejemplo
----------

        SISTEMA DE VENTAS

Usuario: admin
Contraseña: 1234

Bienvenido, Administrador.

Presione una tecla para continuar...

Si las credenciales son incorrectas:

Usuario o contraseña incorrectos.
Intente nuevamente.` 


## :house: Menú Principal

Después de iniciar sesión correctamente, se muestra el menú principal del sistema.


        MENÚ PRINCIPAL


1. Registrar producto
2. Reporte de productos
3. Registrar venta
4. Reporte de ventas
5. Cerrar sesion

Seleccione una opción:` 

El usuario puede seleccionar cualquiera de las opciones disponibles.

----------

## :package: 1. Registrar Producto

Esta opción permite registrar nuevos productos en el sistema.

### Información del producto

-   :1234: Código
-   :label: Nombre
-   :moneybag: Precio
-   :1234: Stock

### :computer: Ejemplo

        CREAR PRODUCTO

Código: P001
Nombre: Teclado Mecánico
Precio: 150.00
Stock: 20

Producto registrado correctamente. 

El sistema valida que la información ingresada sea correcta antes de registrar el producto.

----------
## :bar_chart: 2. Reporte de Productos

Permite consultar todos los productos registrados en el sistema.

### :computer: Ejemplo


=========================================================
                 REPORTE DE PRODUCTOS                 
´=========================================================
Código    Nombre                  Precio       Stock
---------------------------------------------------------
P001      Laptop Lenovo        S/. 2500.00     10

P002      Mouse Logitech             S/. 80.00      20

P003      Teclado Gamer             S/. 15.00     15

=========================================================` 

Este reporte permite consultar el inventario disponible.

------------------------------------
## :shopping_cart: 3. Registro de Ventas

El módulo de ventas permite registrar las operaciones realizadas con los clientes.

### Información de la venta

-   :1234: Número de venta
-   :calendar: Fecha
-   :bust_in_silhouette: Usuario
-   :package: Producto
-   :1234: Cantidad
-   :moneybag: Precio
-   :chart_with_upwards_trend: Total

### :computer: Ejemplo

=========================================================
                 REGISTRAR VENTA                 
´=========================================================

Número de venta: 1

Producto: Laptop Lenovo
Precio: S/. 2500.00
Cantidad: 2

------------------------------------
Subtotal: S/. 5000.00
------------------------------------

Venta registrada correctamente.

Después de realizar la venta, el stock se actualiza automáticamente.

Stock anterior: 10
Cantidad vendida: 2
Stock actual: 8` 

----------
## :moneybag: 3. Reporte de Ventas

Permite consultar las ventas realizadas en el sistema.

### Información mostrada

-   :1234: Número de venta
-   :calendar: Fecha
-   :bust_in_silhouette: Usuario
-   :package: Producto
-   :1234: Cantidad
-   :moneybag: Total

### :computer: Ejemplo

----
## :bust_in_silhouette: 4. Creación de Usuarios

Esta funcionalidad permite registrar nuevos usuarios dentro del sistema.

### Información del usuario

-   :id: ID
-   :bust_in_silhouette: Nombre
-   :bust_in_silhouette: Apellido
-   :computer: Nombre de usuario
-   :key: Contraseña
-   :shield: Rol

### :computer: Ejemplo

----------

## :door: 5. Cerrar Sesion

La opción **Salir** permite finalizar la ejecución de la aplicación.

Antes de cerrar el sistema se puede solicitar una confirmación.

----------

## :building_construction: Estructura del Proyecto


El proyecto puede organizarse utilizando diferentes clases para separar las responsabilidades del sistema.

SistemaVentas/  
|  
|── Program.cs  
│  
|── Interfaces/  
│   ├── IProductoService.cs  
│   ├── IUsuarioService.cs  
│   └── IVentaService.cs  
|  
|── Modelos/  
│   ├── Usuario.cs  
│   ├── Producto.cs  
│   ├── DetalleVenta.cs  
│   └── Venta.cs  
│  
├── Services/  
│   ├── UsuarioService.cs  
│   ├── ProductoService.cs  
│   └── VentaService.cs  
│  
└── Presentacion/  
    └── Menu.cs  

