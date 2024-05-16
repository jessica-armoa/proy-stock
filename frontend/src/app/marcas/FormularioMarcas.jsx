import React, { useState } from 'react';
import { Button, NumberInput, TextInput } from '@tremor/react';
import MarcasConfig from './MarcasConfig';

export default function FormularioMarcas() {

    const [str_nombre, setStr_nombre] = useState('');

    const handleSubmit = async (event) => {
        event.preventDefault();
        try {
            // Aquí podrías realizar alguna acción con los datos del formulario, como enviarlos a un servidor

            /*const marca = {
                str_nombre
            };*/

            const marcaTojson = JSON.stringify(str_nombre);

            const response = await MarcasConfig.createMarca(marcaTojson);
            console.log({
                str_nombre
            });
            // También puedes reiniciar los valores de los campos del formulario
            setStr_nombre('');
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

                <Button variant="primary" type="submit">Guardar</Button>
                <Button variant="secondary" onClick={() => {
                    // Lógica para descartar
                    console.log("Formulario descartado");
                    // Reiniciar los valores del formulario
                    setStr_nombre('');
                }}>Descartar</Button>
            </div>
        </form>
    )
}