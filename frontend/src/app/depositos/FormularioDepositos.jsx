"use client";
import React, { useState } from 'react';
import { Button, NumberInput, TextInput } from '@tremor/react';
import DepositosConfig from './DepositosConfig';

export default function FormularioDepositos() {

    const [str_nombre, setStr_nombre] = useState('');
    const [str_direccion, setStr_direccion] = useState('');

    const handleSubmit = async (event) => {
        event.preventDefault();
        try {
            // Aquí juntamos los datos del deposito, para enviarlos al servidor

            const deposito = {
                "str_nombre": str_nombre,
                "str_direccion": str_direccion
            }

            console.log({
                str_nombre,
                str_direccion
            });

            const response = await DepositosConfig.createDeposito(1, deposito);
            

            // También puedes reiniciar los valores de los campos del formulario
            setStr_nombre('');
            setStr_direccion('');
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
                        Nombre del Depósito
                        <span className="text-red-500">*</span>
                    </label>
                    <TextInput
                        type="text"
                        id="str_nombre"
                        name="str_nombre"
                        autoComplete="str_nombre"
                        placeholder="Nombre de Deposito"
                        className="mt-2"
                        value={str_nombre}
                        onChange={(e) => setStr_nombre(e.target.value)}
                        required
                    />
                </div>

                <div className="col-span-full sm:col-span-3">
                    <label
                        htmlFor="str_direccion"
                        className="text-tremor-default font-medium text-tremor-content-strong dark:text-dark-tremor-content-strong"
                    >
                        Dirección del Depósito
                        <span className="text-red-500">*</span>
                    </label>
                    <TextInput
                        type="text"
                        id="str_direccion"
                        name="str_direccion"
                        autoComplete="str_direccion"
                        placeholder="Dirección del Depósito"
                        className="mt-2"
                        value={str_direccion}
                        onChange={(e) => setStr_direccion(e.target.value)}
                        required
                    />
                </div>

                <Button variant="primary" type="submit">Guardar</Button>
                <Button variant="secondary" onClick={() => {
                    // Lógica para descartar
                    console.log("Formulario descartado");
                    // Reiniciar los valores del formulario
                    setStr_nombre('');
                    setStr_direccion('');
                }}>Descartar</Button>
            </div>
        </form>
    )
}