import FormularioDepositos from "./FormularioDepositos";

import dynamic from 'next/dynamic';// Dynamic imports
const Sidebar = dynamic(() => import("@/components/sidebar/Sidebar"), { ssr: false });

export default function CrearDeposito() {
    return (
        <div className="flex h-screen w-full bg-ui-background p-2 text-ui-text">
            <Sidebar />
            <div className="flex flex-col w-full h-full p-5 rounded-lg bg-ui-cardbg">
                <h1 className='mb-4 text-l font-semibold normal-case tracking-tight'>Crear Deposito</h1>
                <FormularioDepositos />
            </div>
        </div>

    )
}