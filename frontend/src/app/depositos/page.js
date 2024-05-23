'use client'

import React, { useState, useEffect } from "react";
import { Button } from "@tremor/react";
import { useRouter } from 'next/navigation';
import Swal from 'sweetalert2';
import DepositosController from "../../libs/DepositosController";
import dynamic from 'next/dynamic'; 
import DepositosConfig from "./DepositosConfig";

const Sidebar = dynamic(() => import("@/components/sidebar"), { ssr: false });
const DataTable = dynamic(() => import("@/components/table"), { ssr: false });

const Depositos = () => {
  const router = useRouter();
  const [depositos, setDepositos] = useState([]);
  const [modalIsOpen, setModalIsOpen] = useState(false);
  const [currentDeposito, setCurrentDeposito] = useState(null);

  useEffect(() => {
    if (depositos.length <= 0) {
      DepositosController.getDepositos().then((response) => {
        setDepositos(response.data);
      });
    }
  }, [depositos.length]);

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
        DepositosConfig.deleteDeposito(id)
          .then(() => {
            setDepositos(depositos.filter(deposito => deposito.id !== id));
            Swal.fire(
              'Borrado!',
              'El depósito ha sido borrado.',
              'success'
            );
          })
          .catch((error) => {
            console.error("Error deleting deposito:", error);
            Swal.fire(
              'Error!',
              'Hubo un problema al borrar el depósito.',
              'error'
            );
          });
      }
    });
  };

  const handleEdit = (deposito, event) => {
    event.stopPropagation(); // Detener la propagación del evento
    setCurrentDeposito(deposito);
    setModalIsOpen(true);
  };

  const handleSave = () => {
    console.log(currentDeposito)
    DepositosConfig.updateDeposito(currentDeposito.id, currentDeposito)
      .then(() => {
        setDepositos(depositos.map(deposito => (deposito.id === currentDeposito.id ? currentDeposito : deposito)));
        setModalIsOpen(false);
        Swal.fire(
          'Actualizado!',
          'El depósito ha sido actualizado.',
          'success'
        );
      })
      .catch((error) => {
        console.error("Error updating deposito:", error);
        Swal.fire(
          'Error!',
          'Hubo un problema al actualizar el depósito.',
          'error'
        );
      });
  };

  const handleChange = (e) => {
    const { name, value } = e.target;
    setCurrentDeposito({ ...currentDeposito, [name]: value });
  };

  const columns = [
    {
      accessorKey: "str_nombre",
      header: "Nombre del depósito",
      search: true
    },
    {
      accessorKey: "str_encargado",
      header: "Encargado",
    },
    {
      accessorKey: "str_ferreteriaTelefono",
      header: "Teléfono",
      widthClass: "w-large",
    },
    {
      accessorKey: "str_direccion",
      header: "Dirección",
      widthClass: "w-medium",
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
              class="size-5"
            >
              <path d="m5.433 13.917 1.262-3.155A4 4 0 0 1 7.58 9.42l6.92-6.918a2.121 2.121 0 0 1 3 3l-6.92 6.918c-.383.383-.84.685-1.343.886l-3.154 1.262a.5.5 0 0 1-.65-.65Z" />
              <path d="M3.5 5.75c0-.69.56-1.25 1.25-1.25H10A.75.75 0 0 0 10 3H4.75A2.75 2.75 0 0 0 2 5.75v9.5A2.75 2.75 0 0 0 4.75 18h9.5A2.75 2.75 0 0 0 17 15.25V10a.75.75 0 0 0-1.5 0v5.25c0 .69-.56 1.25-1.25 1.25h-9.5c-.69 0-1.25-.56-1.25-1.25v-9.5Z" />
            </svg>
         
          </button>
          <button
            className="px-4 py-2 bg-red-500 text-white rounded hover:bg-red-700"
            onClick={(event) => handleDelete(row.original.id, event)}
          >
            <svg
              xmlns="http://www.w3.org/2000/svg"
              viewBox="0 0 20 20"
              fill="currentColor"
              class="size-5"
            >
              <path
                fill-rule="evenodd"
                d="M8.75 1A2.75 2.75 0 0 0 6 3.75v.443c-.795.077-1.584.176-2.365.298a.75.75 0 1 0 .23 1.482l.149-.022.841 10.518A2.75 2.75 0 0 0 7.596 19h4.807a2.75 2.75 0 0 0 2.742-2.53l.841-10.52.149.023a.75.75 0 0 0 .23-1.482A41.03 41.03 0 0 0 14 4.193V3.75A2.75 2.75 0 0 0 11.25 1h-2.5ZM10 4c.84 0 1.673.025 2.5.075V3.75c0-.69-.56-1.25-1.25-1.25h-2.5c-.69 0-1.25.56-1.25 1.25v.325C8.327 4.025 9.16 4 10 4ZM8.58 7.72a.75.75 0 0 0-1.5.06l.3 7.5a.75.75 0 1 0 1.5-.06l-.3-7.5Zm4.34.06a.75.75 0 1 0-1.5-.06l-.3 7.5a.75.75 0 1 0 1.5.06l.3-7.5Z"
                clip-rule="evenodd"
              />
            </svg>
          </button>
        </div>
      ),
    },
  ];

  return (
    <div className="flex h-screen w-full bg-ui-background p-2 text-ui-text">
      <Sidebar />
      <div className="flex flex-col w-full h-full p-5 rounded-lg bg-ui-cardbg">
        <h1 className='mb-4 text-l font-semibold normal-case tracking-tight'>Depósitos</h1> 
        <div className="flex items-center justify-end space-x-2">
          <Button
            variant="primary"
            color="blue"
            onClick={() => router.push("/depositos/nuevo")}
          >
            Nuevo Depósito
          </Button>
        </div>
        <div>
          {depositos.length <= 0 ? (
            <p>No hay depósitos</p>
          ) : (
            <DataTable data={depositos} columns={columns} />
          )}
        </div>
      </div>

      {modalIsOpen && (
        <div className="fixed inset-0 z-50 flex items-center justify-center overflow-auto bg-gray-800 bg-opacity-50">
          <div className="bg-white rounded-lg shadow-lg w-3/4 max-w-lg">
            <div className="p-4 border-b">
              <h2 className="text-xl font-semibold">Editar Depósito</h2>
            </div>
            <div className="p-4">
              {currentDeposito && (
                <form>
                  <div className="mb-4">
                    <label className="block text-sm font-medium text-gray-700">
                      Nombre:
                      <span className="text-red-700">*
                      </span>
                      <input
                        type="text"
                        name="str_nombre"
                        value={currentDeposito.str_nombre}
                        onChange={handleChange}
                        className="mt-1 block w-full border-gray-300 rounded-md shadow-sm"
                      />
                    </label>
                  </div>
                  <div className="mb-4">
                    <label className="block text-sm font-medium text-gray-700">
                      Encargado:
                      <span className="text-red-700">*
                      </span>
                      <input
                        type="text"
                        name="str_encargado"
                        value={currentDeposito.str_encargado}
                        onChange={handleChange}
                        className="mt-1 block w-full border-gray-300 rounded-md shadow-sm"
                      />
                    </label>
                  </div>
                  <div className="mb-4">
                    <label className="block text-sm font-medium text-gray-700">
                      Teléfono:
                      <span className="text-red-700">*
                      </span>
                      <input
                        type="text"
                        name="str_ferreteriaTelefono"
                        value={currentDeposito.str_ferreteriaTelefono}
                        onChange={handleChange}
                        className="mt-1 block w-full border-gray-300 rounded-md shadow-sm"
                      />
                    </label>
                  </div>
                  <div className="mb-4">
                    <label className="block text-sm font-medium text-gray-700">
                      Dirección:
                      <span className="text-red-700">*
                      </span>
                      <input
                        type="text"
                        name="str_direccion"
                        value={currentDeposito.str_direccion}
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
}

export default Depositos;
