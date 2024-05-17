import Sidebar from "@/components/sidebar";
import FormularioProductos from "./FormularioProductos";


export default function CrearProducto() {
    return (
        <div className="flex h-screen w-full bg-ui-background p-2 text-ui-text">
            <Sidebar />
            <div className="flex flex-col w-full h-full p-5 rounded-lg bg-ui-cardbg">
                <h1 className='mb-4 text-l font-semibold normal-case tracking-tight'>Crear producto</h1>
                <FormularioProductos />
            </div>
        </div>

    )
}