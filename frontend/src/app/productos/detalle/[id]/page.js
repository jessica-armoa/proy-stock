"use client";

import React, { useState, useEffect } from "react";
import { useRouter, usePathname } from "next/navigation";
import dynamic from "next/dynamic";
import ProductosConfig from "../../../../controladores/ProductosConfig";
import withAuth from "@/components/auth/withAuth";
import MovimientosConfig from "../../../../controladores/MovimientosConfig";
import ProveedoresConfig from "@/controladores/ProveedoresConfig";

// Dynamic imports/
const Sidebar = dynamic(() => import("@/components/barraNavegacion/Sidebar"), {
  ssr: false,
});
const DataTable = dynamic(() => import("@/components/tabla"), { ssr: false });
const Photo = dynamic(() => import("@/components/productos"), { ssr: false });

const Detalle = ({ params, cantElementos = 8 }) => {
  //params lee los parametros de la url, en este caso de los subdirectorios, en este tenemos el dir [id]
  const [movimientosDetalle, setMovimientosDetalle] = useState([]);
  const [movimientos, setMovimientos] = useState([]);
  const router = useRouter();

  const { id } = params;

  //console.log("ID:", id);
  const [producto, setProducto] = useState([]);

  useEffect(() => {
    if (producto.length <= 0) {
      ProductosConfig.getProductoId(id).then((response) => {
        setProducto(response.data);
        if (response.data.detallesDeMovimientos != null) {
          setMovimientosDetalle(response.data.detallesDeMovimientos);
          //console.log(response.data.detallesDeMovimientos)
        }
      });
    }
  }, [id]);

  const [proveedor, setProveedor] = useState([]);

  useEffect(() => {
    if (producto.id != undefined) {
      console.log(producto,producto.proveedorId)
      ProveedoresConfig.getProveedorById(producto.proveedorId).then((response) => {
      setProveedor(response.data)
    })
    }
  }, [producto])

  useEffect(() => {
    if (movimientosDetalle.length > 0) {
      // Paso 1: Agrupar los movimientos por movimientoId
      const movimientosPorId = movimientosDetalle.reduce((acc, movimiento) => {
        const { movimientoId } = movimiento;
        if (!acc[movimientoId]) {
          acc[movimientoId] = [];
        }
        acc[movimientoId].push(movimiento);
        return acc;
      }, {});

      // Paso 2: Obtener los detalles de cada grupo de movimientoId
      const promesas = Object.keys(movimientosPorId).map((movimientoId) => 
        MovimientosConfig.getMovimientoById(movimientoId).then((response) => ({
          movimientoId,
          detalles: response.data,
          movimientos: movimientosPorId[movimientoId],
        }))
      );

      // Paso 3: Esperar a que todas las promesas se resuelvan y actualizar el estado
      Promise.all(promesas).then((resultados) => {
        const movimientosFiltrados = resultados.flatMap(({ movimientos, detalles }) =>
          movimientos.map(movimiento => ({
            date_fecha: detalles.date_fecha,
            tipoDeMovimientoId: detalles.tipoDeMovimientoId,
            int_cantidad: movimiento.int_cantidad,
            depositoOrigenId: detalles.depositoOrigenId,
            depositoDestinoId: detalles.depositoDestinoId,
          }))
        );
        setMovimientos(movimientosFiltrados);
      });
    }
  }, [movimientosDetalle]);

  const formatCurrency = (value) => {
    const formattedValue = Intl.NumberFormat('es-ES', {
      style: 'currency',
      currency: 'PYG',
      minimumFractionDigits: 0,
      useGrouping: true
    }).format(value);
    return formattedValue.replace('PYG', '');
  };
  
  const columns = [
    {
      accessorKey: "date_fecha",
      header: "Fecha",
    },
    {
      accessorKey: "tipoDeMovimientoId",
      header: "Movimiento",
    },
    {
      accessorKey: "int_cantidad",
      header: "Cantidad",
    },
    {
      accessorKey: "depositoOrigenId",
      header: "Depósito Origen",
    },
    {
      accessorKey: "depositoDestinoId",
      header: "Depósito Destino",
    },
  ];

  return (
    <div>
      <div className="flex h-screen bg-ui-background p-2 text-ui-text">
        <Sidebar />
        <div className="flex flex-col w-full h-full p-5 rounded-lg bg-ui-cardbg overflow-y">
          <div className="container mx-auto">
            <nav className="text-sm" aria-label="Breadcrumb">
              <ol className="list-none p-0 inline-flex space-x-1">
                <li className="flex items-center">
                  <div
                    className="clickable text-gray-500 flex items-center"
                    onClick={() => router.push("/productos")}
                  >
                    <span className="material-symbols-outlined">
                      chevron_left
                    </span>{" "}
                    Stock &gt;{" "}
                  </div>
                </li>
                <li className="flex items-center">
                  <span className="text-gray-500">Detalles del producto</span>
                </li>
              </ol>
            </nav>
            <div className=" grid grid-cols-3 mb-10">
              <div>
                <Photo src={producto.str_ruta_imagen}></Photo>
              </div>
              <div>

                <p>Codigo: {producto.id}</p>
                <p>Nombre:{producto.str_nombre} </p>
                <p>Descripcion: {producto.str_descripcion} </p>
                <p>Marca: {producto.marcaNombre} </p>
                <p>Proveedor: {producto.proveedorNombre}</p>
                <p>Contacto: {proveedor.str_telefono}</p>
              </div>
              <div>
                <p>Cantidad: {formatCurrency(producto.int_cantidad_actual)}</p>
                <p>Cant.Mínima: {producto.int_cantidad_minima}</p>
                <p>Costo: {formatCurrency(producto.dec_costo_PPP)}</p>
                <p>IVA: {producto.int_iva}</p>
                <p>Precio Mayorista: {formatCurrency(producto.dec_precio_mayorista)}</p>
                <p>Precio Minorista: {formatCurrency(producto.dec_precio_minorista)}</p>
              </div>
            </div>
            <div>
              <span className="text-l tracking-tight">
                Historial de movimientos del producto
              </span>
              {movimientosDetalle.length <= 0 ? (
                <p>No hay Movimientos</p>
              ) : (
                <DataTable
                data={movimientos}
                columns={columns}
                cantElementos={cantElementos}
                pageurl={`/movimientos/detalle/`}
                />
              )}
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default withAuth(Detalle);
