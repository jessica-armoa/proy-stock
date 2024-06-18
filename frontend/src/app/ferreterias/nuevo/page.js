import FormularioFerreteria from "./FormularioFerreteria";

import dynamic from 'next/dynamic';// Dynamic imports
const Sidebar = dynamic(() => import("@/components/barraNavegacion/Sidebar"), { ssr: false });

export default function CrearFerreteria() {
    return (
        <div className="flex h-screen w-full bg-ui-background p-2 text-ui-text">
            <Sidebar />
            <div className="flex flex-col w-full h-full p-5 rounded-lg bg-ui-cardbg">
                <h1 className='mb-4 text-l font-semibold normal-case tracking-tight'>Crear Ferreteria</h1>
                <FormularioFerreteria />
            </div>
        </div>

    )
}