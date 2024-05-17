'use client'

import React from "react";
import { Button } from "@tremor/react";
import DataTable from "@/components/table";
import Sidebar from "@/components/sidebar";
import { useNavigate } from "react-router-dom";
import { useState, useEffect } from "react";
import DepositosController from "../../libs/DepositosController";
import withAuth from "@/components/auth/withAuth";


const Depositos = () => {
  const navigate = useNavigate();
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
          <div className="mt-8 flex items-center justify-end space-x-2">
            <Button
              variant="primary"
              color="blue"
              onClick={() => navigate("/depositos/nuevo")}
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

export default withAuth(Depositos);

