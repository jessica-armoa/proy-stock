'use client'
import React from 'react';
import Sidebar from '@/components/sidebar';
import withAuth from '@/components/auth/withAuth';
import { Button } from '@tremor/react';
import { useNavigate } from 'react-router-dom'; 

const Movimientos = () => {
  const navigate = useNavigate();
  const movimientos = [];
  const columns = []; 
  return (
    <div className="flex h-screen w-full bg-ui-background p-2 text-ui-text">
      <Sidebar />
      <div className="flex flex-col w-full h-full p-5 rounded-lg bg-ui-cardbg">
        <h1 className='mb-4 text-l font-semibold normal-case tracking-tight'>Movimientos</h1>
        <div className="flex items-center justify-end space-x-2">
          <Button
            variant="primary"
            color="blue"
            onClick={() => navigate("/depositos/nuevo")}
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
