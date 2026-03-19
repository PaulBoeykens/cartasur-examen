CREATE DATABASE CartaSurExamen;
GO
USE CartaSurExamen;
GO

CREATE TABLE VENTAS (
    ID_VENTA                 INT           NOT NULL PRIMARY KEY,
    Fecha_venta              DATETIME,
    Dni_cliente              VARCHAR(10),
    Nombre_empleado          VARCHAR(100),
    Nombre_cliente           VARCHAR(100),
    Importe_total            DECIMAL(10,2),
    Direccion_envio_cliente  VARCHAR(100),
    Direccion_sucursal_venta VARCHAR(100),
    Nombre_sucursal_venta    VARCHAR(100),
    Producto                 VARCHAR(20),
    Cantidad                 INT
);
GO

INSERT INTO VENTAS VALUES
(1, '2024-10-15 10:00:00', '20123456', 'Juan Perez',  'Carlos Lopez',  1500.00, 'Av. Rivadavia 100', 'Av. Corrientes 500', 'Sucursal Centro', 'Notebook', 1),
(2, '2024-10-15 14:30:00', '27654321', 'Maria Gomez', 'Ana Martinez',  800.50,  'Calle Falsa 123',   'Av. Corrientes 500', 'Sucursal Centro', 'Mouse',    3),
(3, '2024-10-20 09:15:00', '30987654', 'Carlos Ruiz', 'Pedro Sanchez', 2200.00, 'Belgrano 200',      'San Martin 300',     'Sucursal Sur',    'Monitor',  2);
GO

-- Punto 2: fecha con más ventas
SELECT TOP 1
    CAST(Fecha_venta AS DATE) AS Fecha, --casteo para que coincidan dos fechas en el mismo día y con distinta hr
    COUNT(*)                  AS CantidadVentas 
FROM VENTAS
GROUP BY CAST(Fecha_venta AS DATE) --agrupa todas las ventas con la misma fecha
ORDER BY CantidadVentas DESC;
GO


USE CartaSurExamen;
GO

CREATE TABLE CLIENTES (
    ID_CLIENTE  INT          NOT NULL PRIMARY KEY IDENTITY(1,1),
    DNI         VARCHAR(10)  NOT NULL UNIQUE,
    Nombre      VARCHAR(100) NOT NULL,
    Direccion   VARCHAR(100)
);

CREATE TABLE EMPLEADOS (
    ID_EMPLEADO INT          NOT NULL PRIMARY KEY IDENTITY(1,1),
    Nombre      VARCHAR(100) NOT NULL
);

CREATE TABLE SUCURSALES (
    ID_SUCURSAL INT          NOT NULL PRIMARY KEY IDENTITY(1,1),
    Nombre      VARCHAR(100) NOT NULL,
    Direccion   VARCHAR(100)
);

CREATE TABLE PRODUCTOS (
    ID_PRODUCTO INT         NOT NULL PRIMARY KEY IDENTITY(1,1),
    Nombre      VARCHAR(20) NOT NULL
);

CREATE TABLE VENTAS_NORMALIZADA (
    ID_VENTA      INT           NOT NULL PRIMARY KEY IDENTITY(1,1),
    Fecha_venta   DATETIME      NOT NULL,
    ID_CLIENTE    INT           NOT NULL REFERENCES CLIENTES(ID_CLIENTE),
    ID_EMPLEADO   INT           NOT NULL REFERENCES EMPLEADOS(ID_EMPLEADO),
    ID_SUCURSAL   INT           NOT NULL REFERENCES SUCURSALES(ID_SUCURSAL),
    ID_PRODUCTO   INT           NOT NULL REFERENCES PRODUCTOS(ID_PRODUCTO),
    Importe_total DECIMAL(10,2),
    Cantidad      INT
);
GO