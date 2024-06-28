"use client"
import dynamic from 'next/dynamic';// Dynamic imports
const Sidebar = dynamic(() => import("@/components/barraNavegacion/Sidebar"), { ssr: false });
import FormularioMovimientos from "./FormularioMovimientos";



export default function CrearMovimiento() {
    return (
        <div className="flex h-screen w-full bg-ui-background p-2 text-ui-text">
            <Sidebar />
            <div className="flex flex-col w-full h-full p-5 rounded-lg bg-ui-cardbg overflow-y">
                <h1 className='mb-4 text-l font-semibold normal-case tracking-tight'>Crear Movimiento</h1>
                <FormularioMovimientos/>
            </div>
        </div>

    )
}