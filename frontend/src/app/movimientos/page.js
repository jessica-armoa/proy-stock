'use client'
import React from 'react';
import withAuth from '@/components/auth/withAuth';
import { Button } from '@tremor/react';
import { useRouter } from 'next/navigation'

import dynamic from 'next/dynamic';// Dynamic imports
const Sidebar = dynamic(() => import("@/components/sidebar/Sidebar"), { ssr: false });
const DataTable = dynamic(() => import("@/components/table"), { ssr: false });

const Movimientos = () => {
  const router = useRouter();
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
            onClick={() => router.push('/depositos/nuevo')}
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
