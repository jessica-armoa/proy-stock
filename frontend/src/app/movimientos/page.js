'use client';
import React from 'react';
import Sidebar from '@/components/sidebar';
import { useNavigate } from 'react-router-dom';
import Button from '@tremor/react';


const Movimientos = () => {
  const navigate = useNavigate();

  return (
    <div className="flex h-screen w-full bg-ui-background p-2 text-ui-text">
      <Sidebar />
      <div className="flex flex-col w-full h-full p-5 rounded-lg bg-ui-cardbg">
        <h1 className='mb-4 text-l font-semibold normal-case tracking-tight'>Movimientos</h1>
        <div className='mt-8 flex items-center justify-end space-x-2'>
          <Button variant="primary" color='blue' onClick={() => navigate('/movimientos/nuevo')}>Nuevo Movimiento</Button>
        </div>
      </div>
    </div>
  )
}

export default Movimientos;