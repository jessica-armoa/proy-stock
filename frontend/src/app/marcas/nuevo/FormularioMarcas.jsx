"use client";
import React, { useEffect, useState } from 'react';
import { Button, NumberInput, TextInput } from '@tremor/react';
import MarcasConfig from '../MarcasConfig';
import ProveedoresConfig from '../../proveedores/ProveedoresConfig';

import { useRouter } from 'next/navigation';

export default function FormularioMarcas() {

    const [str_nombre, setStr_nombre] = useState('');
    const [proveedorId, setProveedorId] = useState(0);
    const [proveedores, setProveedores] = useState([]);

    const router = useRouter();

    useEffect(() => {
        const extraccionProveedores = async () => {
            try {
                const respuesta = await ProveedoresConfig.getProveedor();
                setProveedores(respuesta.data);
            } catch (error) {
                console.error('Error al obtener lista de proveedores: ', error);
            }
        }
        extraccionProveedores();
    }, []);
    //const [str_proveedor, setStr_proveedor] = ProveedoresConfig.getProveedor()

    const handleSubmit = async (event) => {
        event.preventDefault();
        try {
            // Aquí podrías realizar alguna acción con los datos del formulario, como enviarlos a un servidor

            const marca = {
                "str_nombre": str_nombre
            };

            const response = await MarcasConfig.createMarca(proveedorId, marca);
            console.log({
                str_nombre,
                proveedorId
            });
            // También puedes reiniciar los valores de los campos del formulario
            setStr_nombre('');
            setProveedorId(0);
        } catch (error) {
            console.error('Error al enviar los datos del formulario: ', error);
        }
    }

    return (
        <form onSubmit={handleSubmit}>
            <div className="grid grid-cols-1 gap-x-4 gap-y-6 sm:grid-cols-6">
                <div className="col-span-full sm:col-span-3">
                    <label
                        htmlFor="str_nombre"
                        className="text-tremor-default font-medium text-tremor-content-strong dark:text-dark-tremor-content-strong"
                    >
                        Nombre de la Marca
                        <span className="text-red-500">*</span>
                    </label>
                    <TextInput
                        type="text"
                        id="str_nombre"
                        name="str_nombre"
                        autoComplete="str_nombre"
                        placeholder="Nombre de Proveedor"
                        className="mt-2"
                        value={str_nombre}
                        onChange={(e) => setStr_nombre(e.target.value)}
                        required
                    />
                </div>

                <div className="col-span-full sm:col-span-3">
                    <label
                        htmlFor="proveedorId"
                        className="text-tremor-default font-medium text-tremor-content-strong dark:text-dark-tremor-content-strong"
                    >
                        Proveedor
                        <span className="text-red-500">*</span>
                    </label>
                    <select id="proveedorId" value={proveedorId} onChange={(e) => setProveedorId(parseInt(e.target.value))}>
                        <option value={0}>Seleccionar proveedor</option>
                        {proveedores.map(proveedor => (
                            <option key={proveedor.id} value={proveedor.id}>{proveedor.str_nombre}</option>
                        ))}
                    </select>

                    
                </div>

                <Button variant="primary" type="submit">Guardar</Button>
                <Button variant="secondary" type="button" onClick={() => {
                    // Lógica para descartar
                    console.log("Formulario descartado");
                    // Reiniciar los valores del formulario
                    setStr_nombre('');
                    router.push('/marcas');
                }}>Descartar</Button>
            </div>
        </form>
    )
}