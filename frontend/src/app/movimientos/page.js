"use client";
import React, { useEffect, useState } from "react";
import withAuth from "@/components/auth/withAuth";
import { Button } from "@tremor/react";
import { useRouter } from "next/navigation";

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
            <DataTable data={movimientos} columns={columns} />
          )}
        </div>
      </div>
    </div>
  );
};

export default withAuth(Movimientos);
