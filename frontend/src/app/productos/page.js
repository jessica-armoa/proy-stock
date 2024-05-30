"use client";

import React, { useState, useEffect } from "react";
import { Button } from "@tremor/react";
import ProductsController from "../../libs/ProductsController";
import withAuth from "@/components/auth/withAuth";
import ExportPDF from "@/components/exportpdf";
import { useRouter } from 'next/navigation';
import Swal from 'sweetalert2';
import dynamic from 'next/dynamic'; // Dynamic imports

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
      title: '¿Estás seguro?',
      text: "No podrás revertir esto!",
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Sí, borrar!',
      cancelButtonText: 'Cancelar'
    }).then((result) => {
      if (result.isConfirmed) {
        ProductsController.deleteProduct(id)
          .then(() => {
            setProducts(products.filter(product => product.id !== id));
            Swal.fire(
              'Borrado!',
              'El producto ha sido borrado.',
              'success'
            );
          })
          .catch((error) => {
            console.error("Error deleting product:", error);
            Swal.fire(
              'Error!',
              'Hubo un problema al borrar el producto.',
              'error'
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
    ProductsController.updateProduct(currentProduct.id, currentProduct)
      .then(() => {
        setProducts(products.map(product => (product.id === currentProduct.id ? currentProduct : product)));
        setModalIsOpen(false);
        Swal.fire(
          'Actualizado!',
          'El producto ha sido actualizado.',
          'success'
        );
      })
      .catch((error) => {
        console.error("Error updating product:", error);
        Swal.fire(
          'Error!',
          'Hubo un problema al actualizar el producto.',
          'error'
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
            className="px-4 py-2 bg-orange-500 text-white rounded hover:bg-orange-700"
            onClick={(event) => handleEdit(row.original, event)}
          >
            Editar
          </button>
          <button
            className="px-4 py-2 bg-red-500 text-white rounded hover:bg-red-700"
            onClick={(event) => handleDelete(row.original.id, event)}
          >
            Borrar
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
                <form>
                  <div className="mb-4">
                    <label className="block text-sm font-medium text-gray-700">
                      Nombre:
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
                      <input
                        type="number"
                        name="dec_costo_PPP"
                        value={currentProduct.dec_costo_PPP}
                        onChange={handleChange}
                        className="mt-1 block w-full border-gray-300 rounded-md shadow-sm"
                      />
                    </label>
                  </div>
                  <div className="mb-4">
                    <label className="block text-sm font-medium text-gray-700">
                      Precio Mayorista:
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
                className="px-4 py-2 bg-blue-500 text-white rounded hover:bg-blue-700 mr-2"
                onClick={handleSave}
              >
                Guardar
              </button>
              <button
                className="px-4 py-2 bg-gray-500 text-white rounded hover:bg-gray-700"
                onClick={() => setModalIsOpen(false)}
              >
                Cancelar
              </button>
            </div>
          </div>
        </div>
      )}
    </div>
  );
};

export default withAuth(Productos);
