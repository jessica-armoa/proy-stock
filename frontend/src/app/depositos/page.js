'use client'

import React from "react";
import { Button } from "@tremor/react";
//import DataTable from "@/components/table";
//import Sidebar from "@/components/sidebar";
import { useRouter } from 'next/navigation';
import { useState, useEffect } from "react";
import DepositosController from "../../libs/DepositosController";
//import withAuth from "@/components/auth/withAuth";

import dynamic from 'next/dynamic';// Dynamic imports
const Sidebar = dynamic(() => import("@/components/sidebar"), { ssr: false });
const DataTable = dynamic(() => import("@/components/table"), { ssr: false });

const Depositos = () => {
  const router = useRouter();
  const [depositos, setDepositos] = useState([]);

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
      //inputClass: "w-large",
      widthClass: "w-large",
    },
    {
      accessorKey: "str_direccion",
      header: "Dirección",
      widthClass: "w-medium",
    },
    {
      header: "Acciones",
      search: false
    },
  ];

  useEffect(() => {
    if (depositos.length <= 0) {
      DepositosController.getDepositos().then((response) => {
        setDepositos(response.data);
      });
    }
  }, []);


  return (
    <div className="flex h-screen w-full bg-ui-background p-2 text-ui-text">
        <Sidebar/>
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
      </div>

  )
}

export default Depositos;

