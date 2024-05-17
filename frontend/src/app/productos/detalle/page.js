"use client";

import React from "react";
import Photo from "../../../components/productimg";
import DataTable from "@/components/table";
import { Link, useParams } from "react-router-dom";
import Sidebar from "@/components/sidebar";
import { useState, useEffect } from "react";
import ProductsController from "../../../libs/ProductsController";

const Detalle = () => {
  let { id } = useParams();
  console.log("ID:", id);
  const [product, setProduct] = useState({});
  useEffect(() => {
    if (product) {
      ProductsController.getProduct(id).then((response) => {
        setProduct(response.data);
      });
    }
  }, []);

  const data = [
    {
      date: "21/03/2024",
      movement: "Compra",
      quantity: 50,
      document: "001-001-000003",
      unitCost: 7500,
      origin: "Proveedor",
      destination: "Suc-Enc",
    },
    {
      date: "20/02/2024",
      movement: "Venta",
      quantity: 20,
      document: "001-001-000003",
      unitCost: 10000,
      origin: "Suc-Enc",
      destination: "Cliente",
    },
    {
      date: "11/02/2024",
      movement: "Transferencia",
      quantity: 5,
      document: "001-001-000003",
      unitCost: 7500,
      origin: "Suc-Enc",
      destination: "Suc-Asu",
    },
    {
      date: "15/01/2024",
      movement: "Salida",
      quantity: 5,
      document: "001-001-000003",
      unitCost: 7500,
      origin: "Suc-Enc",
      destination: "Reciclaje",
    },
    {
      date: "01/01/2024",
      movement: "Entrada",
      quantity: 5,
      document: "001-001-000009",
      unitCost: 7500,
      origin: "Suc-Asu",
      destination: "Suc-Enc",
    },
    {
      date: "07/12/2023",
      movement: "Entrada",
      quantity: 5,
      document: "001-001-000005",
      unitCost: 7500,
      origin: "Suc-Asu",
      destination: "Suc-Enc",
    },
    {
      date: "30/11/2023",
      movement: "Entrada",
      quantity: 5,
      document: "001-001-000003",
      unitCost: 7500,
      origin: "Suc-Asu",
      destination: "Suc-Enc",
    },
    {
      date: "15/11/2023",
      movement: "Entrada",
      quantity: 5,
      document: "001-001-000003",
      unitCost: 7500,
      origin: "Suc-Asu",
      destination: "Suc-Enc",
    },
  ];
  const columns = [
    {
      accessorKey: "date",
      header: "Cód.Fecha",
    },
    {
      accessorKey: "movement",
      header: "Movimiento",
    },
    {
      accessorKey: "quantity",
      header: "Cantidad",
    },
    {
      accessorKey: "document",
      header: "Documento",
    },
    {
      accessorKey: "unitCost",
      header: "Costo",
    },
    {
      accessorKey: "origin",
      header: "Origen",
    },
    {
      accessorKey: "destination",
      header: "Destino",
    },
  ];

  return (
    <div>
      <div className="flex h-screen w-full bg-ui-background p-2 text-ui-text">
        <Sidebar />
        <div className="flex flex-col w-content h-full p-5 rounded-lg bg-ui-cardbg">
          <div className="container mx-auto">
            <nav className="text-sm" aria-label="Breadcrumb">
              <ol className="list-none p-0 inline-flex space-x-1">
                <li className="flex items-center">
                  <Link to="/productos" className="text-gray-500">
                    {" "}
                    &lt; Stock &gt;{" "}
                  </Link>
                </li>
                <li className="flex items-center">
                  <span className="text-gray-500">Detalles del producto</span>
                </li>
              </ol>
            </nav>
            <div className=" grid grid-cols-3 mb-10">
              <div>
                <Photo src={product.str_ruta_imagen}></Photo>
              </div>
              <div>
                <p>Codigo: {product.id}</p>
                <p>Nombre:{product.str_nombre} </p>
                <p>Descripcion: {product.str_descripcion} </p>
                <p>Marca: {product.marcaId} </p>
                <p>Proveedor: {product.proveedorId}</p>
                <p>Contacto: 0984 235701</p>
              </div>
              <div>
                <p>Cantidad: {product.int_cantidad_actual}</p>
                <p>Cant.Mínima: {product.int_cantidad_minima}</p>
                <p>Costo: {product.dec_costo_PPP}</p>
                <p>IVA: {product.int_iva}</p>
                <p>Precio Mayorista: {product.dec_precio_mayorista}</p>
                <p>Precio Minorista: {product.dec_precio_minorista}</p>
              </div>
            </div>
            <div className="flex justify-between items-center">
              <span className="text-2xl mx-4 tracking-tight text-black">
                Historial de producto
              </span>
            </div>
            {data.length <= 0 ? (
              <p>No hay movimientos</p>
            ) : (
              <DataTable data={data} columns={columns} />
            )}
          </div>
        </div>
      </div>
    </div>
  );
};

export default Detalle;
