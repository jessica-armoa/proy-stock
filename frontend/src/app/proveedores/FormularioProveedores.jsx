"use client";
import React, { useState } from 'react';
import { Button, NumberInput, TextInput } from '@tremor/react';
import { useNavigate } from 'react-router-dom';
import axios from 'axios';
import ProveedoresConfig from './ProveedoresConfig';


export default function FormularioProveedores() {

    const [str_nombre, setStr_nombre] = useState('');
    const [str_telefono, setStr_telefono] = useState('');
    const [str_direccion, setStr_direccion] = useState('');
    const [str_correo, setStr_correo] = useState('');


    const handleSubmit = async (event) => {
        event.preventDefault();
        try {
            // Aquí podrías realizar alguna acción con los datos del formulario, como enviarlos a un servidor

            const proveedor = {
                "str_nombre": str_nombre,
                "str_telefono": str_telefono,
                "str_direccion": str_direccion,
                "str_correo": str_correo
            };

            //const proveedorTojson = JSON.stringify(proveedor);
            console.log({
                str_nombre,
                str_telefono,
                str_direccion,
                str_correo
            });

            const response = await ProveedoresConfig.createProveedor(proveedor);
            
            // También puedes reiniciar los valores de los campos del formulario
            setStr_nombre('');
            setStr_telefono('');
            setStr_direccion('');
            setStr_correo('');
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
                        Nombre del Proveedor
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
                        htmlFor="str_telefono"
                        className="text-tremor-default font-medium text-tremor-content-strong dark:text-dark-tremor-content-strong"
                    >
                        Teléfono del Proveedor
                        <span className="text-red-500">*</span>
                    </label>
                    <TextInput
                        type="text"
                        id="str_telefono"
                        name="str_telefono"
                        autoComplete="str_telefono"
                        placeholder="Telefono de Proveedor"
                        className="mt-2"
                        value={str_telefono}
                        onChange={(e) => setStr_telefono(e.target.value)}
                        required
                    />
                </div>

                <div className="col-span-full sm:col-span-3">
                    <label
                        htmlFor="str_direccion"
                        className="text-tremor-default font-medium text-tremor-content-strong dark:text-dark-tremor-content-strong"
                    >
                        Dirección del Proveedor
                        <span className="text-red-500">*</span>
                    </label>
                    <TextInput
                        type="text"
                        id="str_direccion"
                        name="str_direccion"
                        autoComplete="str_direccion"
                        placeholder="Direccion de Proveedor"
                        className="mt-2"
                        value={str_direccion}
                        onChange={(e) => setStr_direccion(e.target.value)}
                        required
                    />
                </div>

                <div className="col-span-full sm:col-span-3">
                    <label
                        htmlFor="str_correo"
                        className="text-tremor-default font-medium text-tremor-content-strong dark:text-dark-tremor-content-strong"
                    >
                        Email del Proveedor
                        <span className="text-red-500">*</span>
                    </label>
                    <TextInput
                        type="email"
                        id="str_correo"
                        name="str_correo"
                        autoComplete="str_correo"
                        placeholder="Correo de Proveedor"
                        className="mt-2"
                        value={str_correo}
                        onChange={(e) => setStr_correo(e.target.value)}
                        required
                    />
                </div>
                <Button variant="primary" type="submit">Guardar</Button>
                <Button variant="secondary" onClick={() => {
                    // Lógica para descartar
                    console.log("Formulario descartado");
                    // Reiniciar los valores del formulario
                    setStr_nombre('');
                    setStr_telefono('');
                    setStr_direccion('');
                    setStr_correo('');
                }}>Descartar</Button>
            </div>
        </form>
    )
}