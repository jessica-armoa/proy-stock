"use client";

import React from "react";
import { Button } from "@tremor/react";
import DataTable from "@/components/table";
import Sidebar from "@/components/sidebar";
import { useNavigate } from "react-router-dom";
import { useState, useEffect } from "react";
import ProductsController from "../../libs/ProductsController";
import withAuth from "@/components/auth/withauth";

const Productos = () => {
  const navigate = useNavigate();
  const [products, setProducts] = useState([]);

  const columns = [
    {
      accessorKey: "id",
      header: "Cód.",
      search: false
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
      accessorKey: "marcaId",
      header: "Marca",
      widthClass: "w-medium",
    },
    {
      accessorKey: "proveedorId",
      header: "Proveedor",
    },
    {
      accessorKey: "int_cantidad_actual",
      header: "Cant.",
      numericInputType: "range",
      inputClass: "w-small",
      //widthClass: "w-medium",
    },
    {
      accessorKey: "depositoId",
      header: "Depósito",
    },
    {
      accessorKey: "dec_costo_PPP",
      header: "Costo",
    },
    {
      accessorKey: "dec_precio_mayorista",
      header: "Mayorista",
    },
    {
      accessorKey: "dec_precio_minorista",
      header: "Minorista",
    },
    {
      header: "Acciones",
      search: false      
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
          <div className="mt-8 flex items-center justify-end space-x-2">
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
              <DataTable data={products} columns={columns} />
            )}
          </div>
        </div>
      </div>
    </div>
  );
};

export default withAuth(Productos);
