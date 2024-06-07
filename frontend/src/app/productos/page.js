"use client";

import React, { useState, useEffect } from "react";
import { Button } from "@tremor/react";
import ProductsController from "../../libs/ProductsController";
import withAuth from "@/components/auth/withAuth";
import ExportPDF from "@/components/exportpdf";
import { useRouter } from "next/navigation";
import Swal from "sweetalert2";
import dynamic from "next/dynamic"; // Dynamic imports
import { formatearPrecio } from "@/utils/format";

const Sidebar = dynamic(() => import("@/components/sidebar"), { ssr: false });
const DataTable = dynamic(() => import("@/components/table"), { ssr: false });

const Productos = () => {
  const router = useRouter();
  const [products, setProducts] = useState([]);
  const [modalIsOpen, setModalIsOpen] = useState(false);
  const [currentProduct, setCurrentProduct] = useState(null);

  useEffect(() => {
    if (products.length <= 0) {
      ProductsController.getProducts().then((response) => {
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
        ProductsController.deleteProduct(id)
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

  const handleSave = () => {
    console.log(currentProduct)
    ProductsController.updateProduct(currentProduct.id, currentProduct)
      .then(() => {
        setProducts(
          products.map((product) =>
            product.id === currentProduct.id ? currentProduct : product
          )
        );
        setModalIsOpen(false);
        Swal.fire(
          "Actualizado!",
          "El producto ha sido actualizado.",
          "success"
        );
      })
      .catch((error) => {
        console.error("Error updating product:", error);
        console.log(error.response.data);
        Swal.fire(
          "Error!",
          "Hubo un problema al actualizar el producto.",
          "error"
        );
      });
  };

  const handleChange = (e) => {
    const { name, value } = e.target;
    setCurrentProduct({ ...currentProduct, [name]: value });
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
        <div className="flex space-x-2">
          <button
            className="px-4 py-2 bg-cyan-400 text-white rounded hover:bg-cyan-500"
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
            className="px-4 py-2 bg-red-600 text-white rounded hover:bg-red-700"
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
        <div className="flex flex-col w-content h-full p-5 rounded-lg bg-ui-cardbg">
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
        <div className="fixed inset-0 z-50 flex items-center justify-center overflow-auto bg-gray-800 bg-opacity-50">
          <div className="bg-white rounded-lg shadow-lg w-3/4 max-w-lg">
            <div className="p-4 border-b">
              <h2 className="text-xl font-semibold">Editar Producto</h2>
            </div>
            <div className="p-4">
              {currentProduct && (
                <form className="grid grid-cols-1 md:grid-cols-2 gap-4">
                  <div className="mb-4">
                    <label className="block text-sm font-medium text-gray-700">
                      Nombre: 
                      <span className="text-red-700">*
                      </span>
                      <input
                        type="text"
                        name="str_nombre"
                        value={currentProduct.str_nombre}
                        onChange={handleChange}
                        className="mt-1 block w-full border-gray-300 rounded-md shadow-sm"
                      />
                    </label>
                  </div>
                  <div className="mb-4">
                    <label className="block text-sm font-medium text-gray-700">
                      Descripción:
                      <span className="text-red-700">*
                      </span>
                      <input
                        type="text"
                        name="str_descripcion"
                        value={currentProduct.str_descripcion}
                        onChange={handleChange}
                        className="mt-1 block w-full border-gray-300 rounded-md shadow-sm"
                      />
                    </label>
                  </div>
                  <div className="mb-4">
                    <label className="block text-sm font-medium text-gray-700">
                      Marca:
                      <span className="text-red-700">*
                      </span>
                      <input
                        type="text"
                        name="marcaNombre"
                        value={currentProduct.marcaNombre}
                        onChange={handleChange}
                        className="mt-1 block w-full border-gray-300 rounded-md shadow-sm"
                      />
                    </label>
                  </div>
                  <div className="mb-4">
                    <label className="block text-sm font-medium text-gray-700">
                      Proveedor:
                      <span className="text-red-700">*
                      </span>
                      <input
                        type="text"
                        name="proveedorNombre"
                        value={currentProduct.proveedorNombre}
                        onChange={handleChange}
                        className="mt-1 block w-full border-gray-300 rounded-md shadow-sm"
                      />
                    </label>
                  </div>
                  <div className="mb-4">
                    <label className="block text-sm font-medium text-gray-700">
                      Cantidad:
                      <span className="text-red-700">*
                      </span>
                      <input
                        type="number"
                        name="int_cantidad_actual"
                        value={currentProduct.int_cantidad_actual}
                        onChange={handleChange}
                        className="mt-1 block w-full border-gray-300 rounded-md shadow-sm"
                      />
                    </label>
                  </div>
                  <div className="mb-4">
                    <label className="block text-sm font-medium text-gray-700">
                      Depósito:
                      <span className="text-red-700">*
                      </span>
                      <input
                        type="text"
                        name="depositoNombre"
                        value={currentProduct.depositoNombre}
                        onChange={handleChange}
                        className="mt-1 block w-full border-gray-300 rounded-md shadow-sm"
                      />
                    </label>
                  </div>
                  <div className="mb-4">
                    <label className="block text-sm font-medium text-gray-700">
                      Costo:
                      <span className="text-red-700">*
                      </span>
                      <input
                        type="number"
                        name="dec_costo_PPP"
                        value={currentProduct.dec_costo_PPP }
                        onChange={handleChange}
                        className="mt-1 block w-full border-gray-300 rounded-md shadow-sm"
                      />
                    </label>
                  </div>
                  <div className="mb-4">
                    <label className="block text-sm font-medium text-gray-700">
                      Precio Mayorista:
                      <span className="text-red-700">*
                      </span>
                      <input
                        type="number"
                        name="dec_precio_mayorista"
                        value={currentProduct.dec_precio_mayorista}
                        onChange={handleChange}
                        className="mt-1 block w-full border-gray-300 rounded-md shadow-sm"
                      />
                    </label>
                  </div>
                  <div className="mb-4">
                    <label className="block text-sm font-medium text-gray-700">
                      Precio Minorista:
                      <span className="text-red-700">*
                      </span>
                      <input
                        type="number"
                        name="dec_precio_minorista"
                        value={currentProduct.dec_precio_minorista}
                        onChange={handleChange}
                        className="mt-1 block w-full border-gray-300 rounded-md shadow-sm"
                      />
                    </label>
                  </div>
                </form>
              )}
            </div>
            <div className="flex justify-end p-4 border-t">
              <button
                className="px-4 py-2 bg-gray-500 text-white rounded hover:bg-gray-700"
                onClick={() => setModalIsOpen(false)}
              >
                Cancelar
              </button>
              <button
                className="px-4 py-2 bg-blue-500 text-white rounded hover:bg-blue-700 ml-2"
                onClick={handleSave}
              >
                Guardar
              </button>
            </div>
          </div>
        </div>
      )}
    </div>
  );
};

export default withAuth(Productos);
