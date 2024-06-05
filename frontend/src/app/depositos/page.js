'use client'

import React, { useState, useEffect } from "react";
import { Button } from "@tremor/react";
import { useRouter } from 'next/navigation';
import Swal from 'sweetalert2';
import DepositosController from "../../libs/DepositosController";
import dynamic from 'next/dynamic'; 

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
        DepositosController.deleteDeposito(id)
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
    DepositosController.updateDeposito(currentDeposito.id, currentDeposito)
    //console.log('current', currentDeposito)
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
        //console.log(depositos)
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
      accessorKey: "str_telefono",
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
}

export default Depositos;
