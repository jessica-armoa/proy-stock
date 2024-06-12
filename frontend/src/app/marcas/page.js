'use client'
import React from 'react'
import { Button } from '@tremor/react';
import { useRouter } from 'next/navigation'

import dynamic from 'next/dynamic';// Dynamic imports
const Sidebar = dynamic(() => import("@/components/barraNavegacion/Sidebar"), { ssr: false });
//const DataTable = dynamic(() => import("@/components/tabla"), { ssr: false });

const Marcas = () => {
    const router = useRouter();
    return (

        <div>
            <div className="flex h-screen w-full bg-ui-background p-2 text-ui-text">
                <Sidebar />
                <div className="flex flex-col w-full h-full p-5 rounded-lg bg-ui-cardbg">
                    <h1 className='mb-4 text-l font-semibold normal-case tracking-tight'>Marcas</h1>
                    <div className='mt-8 flex items-center justify-end space-x-2'>
                        <Button
                            variant="primary"
                            color="blue"
                            onClick={() => router.push('/marcas/nuevo')}
                        >
                            Nueva Marca
                        </Button>
                    </div>
                    <div></div>
                </div>
            </div>

        </div>
    )
}

export default Marcas;