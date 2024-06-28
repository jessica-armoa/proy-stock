"use client";
import React, { useEffect, useState } from "react";
import withAuth from "@/components/auth/withAuth";
import { Button } from "@tremor/react";
import { useRouter } from "next/navigation";
import Swal from "sweetalert2";
import dynamic from "next/dynamic"; // Dynamic imports
import MovimientosConfig from "../../controladores/MovimientosConfig";
import VistaNR from "./VistaNR";
const Sidebar = dynamic(() => import("@/components/barraNavegacion/Sidebar"), {
  ssr: false,
});
const DataTable = dynamic(() => import("@/components/tabla"), { ssr: false });

const Movimientos = () => {
  const router = useRouter();
  const [movimientos, setMovimientos] = useState([]);

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
        MovimientosConfig.deleteMovimiento(id)
          .then(() => {
            setMovimientos(movimientos.filter((movimiento) => movimiento.id !== id));
            Swal.fire("Borrado!", "El movimiento ha sido borrado.", "success");
          })
          .catch((error) => {
            console.error("Error deleting product:", error);
            Swal.fire(
              "Error!",
              "Hubo un problema al borrar el movimiento.",
              "error"
            );
          });
      }
    });
  };


  const columns = [
    /*"date_fecha": "2024-06-07T16:05:14.9366667",
    "tipoDeMovimientoId": 3,
    "depositoOrigenId": 1,
    "depositoDestinoId": 2,
    "bool_borrado": false,
    "detallesDeMovimientos": */
    {
      accessorKey: "id",
      header: "Cód.",
      search: false,
    },
    {
      accessorKey: "str_motivoPorTipoDeMovimiento",
      header: "Motivo",
      search: false,
      width: '10px'
    },
    {
      accessorKey: "date_fecha",
      header: "Fecha",
      search: false,
    },
    {
      accessorKey: "str_depositoOrigen",
      header: "Deposito Origen",
      search: false,
    },
    {
      accessorKey: "str_depositoDestino",
      header: "Deposito Destino",
      search: false,
    },
    {
      header: "Acciones",
      search: false,
      cell: ({ row }) => (
        <div
          id={"btn-actions" + row.original.id}
          className="flex justify-evenly invisible"
        >
          <VistaNR></VistaNR>
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

  useEffect(() => {
    if (movimientos.length <= 0) {
      MovimientosConfig.getMovimiento().then((response) => {
        setMovimientos(response.data);
      });
    }
  }, []);

  return (
    <div className="flex h-screen w-full bg-ui-background p-2 text-ui-text">
      <Sidebar />
      <div className="flex flex-col w-full h-full p-5 rounded-lg bg-ui-cardbg">
        <h1 className="mb-4 text-l font-semibold normal-case tracking-tight">
          Movimientos
        </h1>
        <div className="flex items-center justify-end space-x-2">
          <Button
            variant="primary"
            color="blue"
            onClick={() => router.push("/movimientos/nuevo")}
          >
            Nuevo Movimiento
          </Button>
        </div>
        <div>
          {movimientos.length <= 0 ? (
            <p>No hay movimientos</p>
          ) : (
            <DataTable data={movimientos} columns={columns} pageurl={`/movimientos/visualizarMovimiento/`} />
          )}
        </div>
      </div>
    </div>
  );
};

export default withAuth(Movimientos);
