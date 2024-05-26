"use client";

import React from "react";
import { Button } from "@tremor/react";
import DataTable from "@/components/table";
import Sidebar from "@/components/sidebar";
import { useNavigate } from "react-router-dom";
import { useState, useEffect } from "react";
import ProductsController from "../../libs/ProductsController";
import withAuth from "@/components/auth/withAuth";
import ExportPDF from "@/components/exportpdf";


const Productos = () => {
  const navigate = useNavigate();
  const [products, setProducts] = useState([]);
  /*const headers= [
    "Cód.",
    "Producto",
    "Descripción",
    "Marca",
    "Proveedor",
    "Cant",
    "Depósito",
    "Costo",
    "Precio May.",
    "Precio Min.",
  ];*/
  const columns = [
    {
      accessorKey: "id",
      header: "Cód.",
      search: false,
    },
    {
      accessorKey: "str_nombre",
      header: "Producto",
    },
    {
      accessorKey: "str_descripcion",
      header: "Descripción",
      //inputClass: "w-large",
      widthClass: "w-large",
    },
    {
      accessorKey: "marcaNombre",
      header: "Marca",
      widthClass: "w-medium",
    },
    {
      accessorKey: "proveedorNombre",
      header: "Proveedor",
    },
    {
      accessorKey: "int_cantidad_actual",
      header: "Cant.",
      numericInputType: "range",
      inputClass: "w-small",
      widthClass: "w-medium",
    },
    {
      accessorKey: "depositoNombre",
      header: "Depósito",
    },
    {
      accessorKey: "dec_costo_PPP",
      header: "Costo",
      search: false,
    },
    {
      accessorKey: "dec_precio_mayorista",
      header: "Mayorista",
      search: false,
    },
    {
      accessorKey: "dec_precio_minorista",
      header: "Minorista",
      search: false,
    },
    {
      header: "Acciones",
      search: false,
    },
  ];

  useEffect(() => {
    if (products.length <= 0) {
      ProductsController.getProducts().then((response) => {
        setProducts(response.data);
      });
    }
  }, []);

  return (
    <div>
      <div className="flex h-screen w-full bg-ui-background p-2 text-ui-text">
        <Sidebar />
        <div className="flex flex-col w-content h-full p-5 rounded-lg bg-ui-cardbg">
          <h1 className="mb-4 text-l font-semibold normal-case tracking-tight">
            Productos
          </h1>
          
          <div className="flex items-center justify-end space-x-2">
          <ExportPDF data={products} whatToExport={columns} title={"Detalle de Stock"} fileName="reporte_stock"></ExportPDF>
            <Button
              variant="primary"
              color="blue"
              onClick={() => navigate("/productos/nuevo")}
            >
              Nuevo Producto
            </Button>
          </div>
          <div>
            {products.length <= 0 ? (
              <p>No hay productos</p>
            ) : (
              <DataTable
                data={products}
                columns={columns}
                pageurl={`/productos/detalle/`}
              />
            )}
          </div>
        </div>
      </div>
    </div>
  );
};

export default withAuth(Productos);
