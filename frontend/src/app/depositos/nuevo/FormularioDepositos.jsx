"use client";
import React, { useEffect, useState } from 'react';
import { Button, Card, Dialog, DialogPanel, NumberInput, TextInput } from '@tremor/react';
import { RiArrowDownSLine, RiCloseLine } from '@remixicon/react';
import DepositosConfig from '../DepositosConfig';
import FerreteriasConfig from '../../ferreterias/FerreteriasConfig';

export default function FormularioDepositos() {

    const [isOpen, setIsOpen] = useState(true);

    const [str_nombre, setStr_nombre] = useState('');
    const [str_direccion, setStr_direccion] = useState('');

    const [str_empleado, setStr_empleado] = useState('');
    const [str_telefono, setStr_telefono] = useState('');

    const [isHabilitado, setIsHabilitado] = useState(true);


    const [fk_ferreteria, setFk_ferreteria] = useState(0);
    const [ferreterias, setFerreterias] = useState([]);

    useEffect(() => {
        const extraccionFerreterias = async () => {
            try {
                const respuesta = await FerreteriasConfig.getFerreteria();
                setFerreterias(respuesta.data);
            } catch (error) {
                console.error('Error al obtener lista de proveedores: ', error);
            }
        }
        extraccionFerreterias();
    }, []);

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
            setFk_ferreteria(0);
        } catch (error) {
            console.error('Error al enviar los datos del formulario: ', error);
        }
    }

    return (
        <Dialog open={isOpen}
            onClose={() => setIsOpen(false)}
            static={true}
            className="z-[100]">
            <DialogPanel className="sm:max-w-md">
                <div className="absolute right-0 top-0 pr-3 pt-3">
                    <button
                        type="button"
                        className="rounded-tremor-small p-2 text-tremor-content-subtle hover:bg-tremor-background-subtle hover:text-tremor-content dark:text-dark-tremor-content-subtle hover:dark:bg-dark-tremor-background-subtle hover:dark:text-tremor-content"
                        onClick={() => setIsOpen(false)}
                        aria-label="Close"
                    >
                        <RiCloseLine
                            className="h-5 w-5 shrink-0"
                            aria-hidden={true}
                        />
                    </button>
                </div>

                <form onSubmit={handleSubmit}>
                    <h4 className="font-semibold text-tremor-content-strong dark:text-dark-tremor-content-strong">
                        Nuevo Depósito
                    </h4>
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

                        {/*<div className="grid grid-cols-1 gap-x-4 gap-y-6 sm:grid-cols-6">*/}
                            <div className="col-span-full sm:col-span-3">
                                <label
                                    htmlFor="str_empleado"
                                    className="text-tremor-default font-medium text-tremor-content-strong dark:text-dark-tremor-content-strong"
                                >
                                    Encargado
                                    <span className="text-red-500">*</span>
                                </label>
                                <TextInput
                                    type="text"
                                    id="str_empleado"
                                    name="str_empleado"
                                    autoComplete="str_empleado"
                                    placeholder="Empleado encargado"
                                    className="mt-2"
                                    value={str_empleado}
                                    onChange={(e) => setStr_empleado(e.target.value)}
                                    required
                                />
                            </div>

                            <div className="col-span-full sm:col-span-3">
                                <label
                                    htmlFor="str_telefono"
                                    className="text-tremor-default font-medium text-tremor-content-strong dark:text-dark-tremor-content-strong"
                                >
                                    Teléfono del Depósito
                                    <span className="text-red-500">*</span>
                                </label>
                                <TextInput
                                    type="text"
                                    id="str_telefono"
                                    name="str_telefono"
                                    autoComplete="str_telefono"
                                    placeholder="Telefono del Depósito"
                                    className="mt-2"
                                    value={str_telefono}
                                    onChange={(e) => setStr_telefono(e.target.value)}
                                    required
                                />
                            </div>

                            {/* <div className="none col-span-full sm:col-span-3">
                    <label
                        htmlFor="fk_ferreteria"
                        className="text-tremor-default font-medium text-tremor-content-strong dark:text-dark-tremor-content-strong"
                    >
                        Ferreteria
                        <span className="text-red-500">*</span>
                    </label>
                    <select id="fk_ferreteria" value={fk_ferreteria} onChange={(e) => setFk_ferreteria(parseInt(e.target.value))}>
                        <option value={0}>Seleccionar Ferreteria</option>
                        {ferreterias.map(ferreteria => (
                            <option key={ferreteria.id} value={ferreteria.id}>{ferreteria.str_nombre}</option>
                        ))}
                    </select>
                </div>*/}


                            <div className="col-span-full flex justify-center space-x-4">
                                <Button variant="secondary" onClick={() => {
                                    // Lógica para descartar
                                    console.log("Formulario descartado");
                                    // Reiniciar los valores del formulario
                                    setStr_nombre('');
                                    setStr_direccion('');
                                    setFk_ferreteria(0);
                                    setIsOpen(false);
                                }}>Cancelar</Button>
                                <Button variant="primary" type="submit">Guardar</Button>
                            </div>
                        </div>
                </form>

            </DialogPanel>
        </Dialog>
    )
}