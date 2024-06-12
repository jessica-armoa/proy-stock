'use client'

import React from 'react'
import { Button } from '@tremor/react';
import Photo from '@/components/productos';
import { useRouter } from 'next/navigation'

import dynamic from 'next/dynamic';// Dynamic imports
const Sidebar = dynamic(() => import("@/components/barraNavegacion/Sidebar"), { ssr: false });

const Proveedores = () => {
    const router = useRouter();
    return (

        <div>
            <div className="flex h-screen w-full bg-ui-background p-2 text-ui-text">
                <Sidebar />
                <div className="flex flex-col w-full h-full p-5 rounded-lg bg-ui-cardbg">
                    <h1 className='mb-4 text-l font-semibold normal-case tracking-tight'>Productos</h1>
                    <div className='mt-8 flex items-center justify-end space-x-2'>
                        <Button variant="primary" color='blue' onClick={() => router.push('/proveedores/nuevo')}>Nuevo Proveedor</Button>
                    </div>
                    <div></div>
                </div>
            </div>

        </div>
    )
}

export default Proveedores;