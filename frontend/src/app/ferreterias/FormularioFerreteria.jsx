"use client";
import React, { useState } from 'react';
import { Button, NumberInput, TextInput } from '@tremor/react';
import { useNavigate } from 'react-router-dom';
import FerreteriasConfig from './FerreteriasConfig';


export default function FormularioFerreteria() {

    const [str_nombre, setStr_nombre] = useState('');
    const [str_ruc, setStr_ruc] = useState('');
    const [str_telefono, setStr_telefono] = useState('');


    const handleSubmit = async (event) => {
        event.preventDefault();
        try {
            // Aquí podrías realizar alguna acción con los datos del formulario, como enviarlos a un servidor

            const ferreteria = {
                "str_nombre": str_nombre,
                "str_ruc": str_ruc,
                "str_telefono": str_telefono,
            };

            //const proveedorTojson = JSON.stringify(proveedor);
            console.log(ferreteria);

            const response = await FerreteriasConfig.createFerreteria(ferreteria);


            // También puedes reiniciar los valores de los campos del formulario

            setStr_nombre('');
            setStr_ruc('');
            setStr_telefono('');

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
                        Nombre de la Ferreteria
                        <span className="text-red-500">*</span>
                    </label>
                    <TextInput
                        type="text"
                        id="str_nombre"
                        name="str_nombre"
                        autoComplete="str_nombre"
                        placeholder="Nombre de Ferreteria"
                        className="mt-2"
                        value={str_nombre}
                        onChange={(e) => setStr_nombre(e.target.value)}
                        required
                    />
                </div>

                <div className="col-span-full sm:col-span-3">
                    <label
                        htmlFor="str_ruc"
                        className="text-tremor-default font-medium text-tremor-content-strong dark:text-dark-tremor-content-strong"
                    >
                        RUC de la Ferreteria
                        <span className="text-red-500">*</span>
                    </label>
                    <TextInput
                        type="text"
                        id="str_ruc"
                        name="str_ruc"
                        autoComplete="str_ruc"
                        placeholder="RUC de la Ferreteria"
                        className="mt-2"
                        value={str_ruc}
                        onChange={(e) => setStr_ruc(e.target.value)}
                        required
                    />
                </div>

                <div className="col-span-full sm:col-span-3">
                    <label
                        htmlFor="str_telefono"
                        className="text-tremor-default font-medium text-tremor-content-strong dark:text-dark-tremor-content-strong"
                    >
                        Teléfono de la Ferreteria
                        <span className="text-red-500">*</span>
                    </label>
                    <TextInput
                        type="text"
                        id="str_telefono"
                        name="str_telefono"
                        autoComplete="str_telefono"
                        placeholder="Telefono de la Ferreteria"
                        className="mt-2"
                        value={str_telefono}
                        onChange={(e) => setStr_telefono(e.target.value)}
                        required
                    />
                </div>


            </div>
            <Button variant="primary" type="submit">Guardar</Button>
            <Button variant="secondary" onClick={() => {
                // Lógica para descartar
                console.log("Formulario descartado");
                // Reiniciar los valores del formulario
                setStr_nombre('');
                setStr_ruc('');
                setStr_telefono('');
            }}>Descartar</Button>    
        </form >
    )
}