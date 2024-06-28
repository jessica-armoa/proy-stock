"use client";

import React, { useState, useEffect } from "react";
import { Button } from "@tremor/react";
import ProductosConfig from "@/controladores/ProductosConfig";
import withAuth from "@/components/auth/withAuth";
import ExportPDF from "@/components/exportpdf";
import { useRouter } from "next/navigation";
import Swal from "sweetalert2";
import dynamic from "next/dynamic"; // Dynamic imports
import { formatearPrecio } from "@/utils/format";
import EditProductModal from "@/components/productos/EditProductModal";


const Sidebar = dynamic(() => import("@/components/barraNavegacion/Sidebar"), { ssr: false });
const DataTable = dynamic(() => import("@/components/tabla"), { ssr: false });

const Productos = () => {
  const router = useRouter();
  const [products, setProducts] = useState([]);
  const [modalIsOpen, setModalIsOpen] = useState(false);
  const [currentProduct, setCurrentProduct] = useState(null);

  useEffect(() => {
    if (products.length <= 0) {
      ProductosConfig.getProductos().then((response) => {
        setProducts(response.data);
      });
    }
  }, []);

  const handleDelete = (id, event) => {
    event.stopPropagation(); // Detener la propagación del evento
    Swal.fire({
      title: "¿Estás seguro?",
      text: "No podrás revertir esto!",
      icon: "warning",
      showCancelButton: true,
      confirmButtonColor: "#3085d6",
      cancelButtonColor: "#d33",
      confirmButtonText: "Sí, borrar!",
      cancelButtonText: "Cancelar",
    }).then((result) => {
      if (result.isConfirmed) {
        ProductosConfig.deleteProducto(id)
          .then(() => {
            setProducts(products.filter((product) => product.id !== id));
            Swal.fire("Borrado!", "El producto ha sido borrado.", "success");
          })
          .catch((error) => {
            console.error("Error deleting product:", error);
            Swal.fire(
              "Error!",
              "Hubo un problema al borrar el producto.",
              "error"
            );
          });
      }
    });
  };

  const handleEdit = (product, event) => {
    event.stopPropagation();
    setCurrentProduct(product);
    setModalIsOpen(true);
  };


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
      cell: ({ row }) => (
        <div id={"btn-actions"+row.original.id} className="flex justify-evenly invisible">
          <button
            className="text-cyan-400 rounded  hover:text-blue-500"
            onClick={(event) => handleEdit(row.original, event)}
          >
            <svg
              xmlns="http://www.w3.org/2000/svg"
              viewBox="0 0 20 20"
              fill="currentColor"
              className="size-5"
            >
              <path d="m5.433 13.917 1.262-3.155A4 4 0 0 1 7.58 9.42l6.92-6.918a2.121 2.121 0 0 1 3 3l-6.92 6.918c-.383.383-.84.685-1.343.886l-3.154 1.262a.5.5 0 0 1-.65-.65Z" />
              <path d="M3.5 5.75c0-.69.56-1.25 1.25-1.25H10A.75.75 0 0 0 10 3H4.75A2.75 2.75 0 0 0 2 5.75v9.5A2.75 2.75 0 0 0 4.75 18h9.5A2.75 2.75 0 0 0 17 15.25V10a.75.75 0 0 0-1.5 0v5.25c0 .69-.56 1.25-1.25 1.25h-9.5c-.69 0-1.25-.56-1.25-1.25v-9.5Z" />
            </svg>
          </button>
          <button
            className="text-red-400 rounded hover:text-red-700"
            onClick={(event) => handleDelete(row.original.id, event)}
          >
            <svg
              xmlns="http://www.w3.org/2000/svg"
              viewBox="0 0 20 20"
              fill="currentColor"
              className="size-5"
            >
              <path
                fillRule="evenodd"
                d="M8.75 1A2.75 2.75 0 0 0 6 3.75v.443c-.795.077-1.584.176-2.365.298a.75.75 0 1 0 .23 1.482l.149-.022.841 10.518A2.75 2.75 0 0 0 7.596 19h4.807a2.75 2.75 0 0 0 2.742-2.53l.841-10.52.149.023a.75.75 0 0 0 .23-1.482A41.03 41.03 0 0 0 14 4.193V3.75A2.75 2.75 0 0 0 11.25 1h-2.5ZM10 4c.84 0 1.673.025 2.5.075V3.75c0-.69-.56-1.25-1.25-1.25h-2.5c-.69 0-1.25.56-1.25 1.25v.325C8.327 4.025 9.16 4 10 4ZM8.58 7.72a.75.75 0 0 0-1.5.06l.3 7.5a.75.75 0 1 0 1.5-.06l-.3-7.5Zm4.34.06a.75.75 0 1 0-1.5-.06l-.3 7.5a.75.75 0 1 0 1.5.06l.3-7.5Z"
                clipRule="evenodd"
              />
            </svg>
          </button>
        </div>
      ),
    },
  ];

  return (
    <div>
      <div className="flex h-screen w-full bg-ui-background p-2 text-ui-text">
        <Sidebar />
        <div className="flex flex-col w-content w-full h-full p-5 rounded-lg bg-ui-cardbg">
          <h1 className="text-l font-semibold normal-case tracking-tight">
            Productos
          </h1>


          <div className="flex items-center justify-end space-x-2">
            <Button
              variant="primary"
              color="blue"
              onClick={() => router.push("/productos/nuevo")}
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

      {modalIsOpen && (
        <EditProductModal
          isOpen={modalIsOpen}
          onClose={() => setModalIsOpen(false)}
          product={currentProduct}
          onSave={() => {
            // Refrescar los productos después de guardar los cambios
            ProductosConfig.getProductos().then((response) => {
              setProducts(response.data);
            });
            Swal.fire(
              "Actualizado!",
              "El producto ha sido actualizado con éxito.",
              "success"
            );
          }}
        />
      )}
    </div>
  );
};

export default withAuth(Productos);
