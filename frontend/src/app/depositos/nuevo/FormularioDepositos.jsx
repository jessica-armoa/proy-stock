"use client";
import React, { useEffect, useState } from 'react';
import { Button, Card, Dialog, DialogPanel, TextInput } from '@tremor/react';
import { RiCloseLine } from '@remixicon/react';
import DepositosConfig from '../../../controladores/DepositosConfig';
import FerreteriasConfig from '../../../controladores/FerreteriasConfig';
import { useNavigate } from 'react-router-dom';
import { useRouter } from 'next/navigation';

export default function FormularioDepositos() {
    const navigate = useRouter();

    const [isOpen, setIsOpen] = useState(true);

    const [str_nombre, setStr_nombre] = useState('');
    const [str_direccion, setStr_direccion] = useState('');
    const [str_telefono, setStr_telefono] = useState('');

    const [encargadoUsername, setEncargadoUsername] = useState('');
    const [encargadoEmail, setEncargadoEmail] = useState('');
    const [encargadoPassword, setEncargadoPassword] = useState('');
    const [repeatPassword, setRepeatPassword] = useState('');

    const [isPasswordMatch, setIsPasswordMatch] = useState(true);

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

    const handlePasswordChange = (e) => {
        setEncargadoPassword(e.target.value);
        setIsPasswordMatch(e.target.value === repeatPassword);
    };

    const handleRepeatPasswordChange = (e) => {
        setRepeatPassword(e.target.value);
        setIsPasswordMatch(e.target.value === encargadoPassword);
    };

    const handleSubmit = async (event) => {
        event.preventDefault();
        if (!isPasswordMatch) {
            return;
        }

        try {
            const deposito = {
                "str_nombre": str_nombre,
                "str_direccion": str_direccion,
                "str_telefono": str_telefono,
                "encargadoUsername": encargadoUsername,
                "encargadoEmail": encargadoEmail,
                "encargadoPassword": encargadoPassword
            };

            console.log(deposito);

            const response = await DepositosConfig.postDeposito(1, deposito);

            setStr_nombre('');
            setStr_direccion('');
            setStr_telefono('');
            setEncargadoUsername('');
            setEncargadoEmail('');
            setEncargadoPassword('');
            setRepeatPassword('');
            setFk_ferreteria(0);

            navigate.push('/depositos');
        } catch (error) {
            console.error('Error al enviar los datos del formulario: ', error);
        }
    };

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
                        onClick={() => {
                            setIsOpen(false);
                            navigate.push('/depositos');
                        }}
                        aria-label="Close"
                    >
                        <RiCloseLine
                            className="h-5 w-5 shrink-0"
                            aria-hidden={true}
                        />
                    </button>
                </div>

                <form onSubmit={handleSubmit}>
                    <h4 className="font-semibold text-tremor-content-strong dark:text-dark-tremor-content-strong mb-4">
                        Nuevo Depósito
                    </h4>
                    <div className="grid grid-cols-1 gap-x-4 gap-y-6 sm:grid-cols-6">
                        <div className="col-span-full">
                            <h5 className="font-semibold text-tremor-content-strong dark:text-dark-tremor-content-strong mb-2">
                                Datos del Depósito
                            </h5>
                        </div>
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
                                placeholder="Nombre de Depósito"
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
                                placeholder="Teléfono del Depósito"
                                className="mt-2"
                                value={str_telefono}
                                onChange={(e) => setStr_telefono(e.target.value)}
                                required
                            />
                        </div>

                        <div className="col-span-full">
                            <h5 className="font-semibold text-tremor-content-strong dark:text-dark-tremor-content-strong mt-6 mb-2">
                                Datos del Encargado
                            </h5>
                        </div>

                        <div className="col-span-full sm:col-span-3">
                            <label
                                htmlFor="encargadoUsername"
                                className="text-tremor-default font-medium text-tremor-content-strong dark:text-dark-tremor-content-strong"
                            >
                                Nombre de Usuario del Encargado
                                <span className="text-red-500">*</span>
                            </label>
                            <TextInput
                                type="text"
                                id="encargadoUsername"
                                name="encargadoUsername"
                                autoComplete="encargadoUsername"
                                placeholder="Nombre de usuario del encargado"
                                className="mt-2"
                                value={encargadoUsername}
                                onChange={(e) => setEncargadoUsername(e.target.value)}
                                required
                            />
                        </div>

                        <div className="col-span-full sm:col-span-3">
                            <label
                                htmlFor="encargadoEmail"
                                className="text-tremor-default font-medium text-tremor-content-strong dark:text-dark-tremor-content-strong"
                            >
                                Email del Encargado
                                <span className="text-red-500">*</span>
                            </label>
                            <TextInput
                                type="email"
                                id="encargadoEmail"
                                name="encargadoEmail"
                                autoComplete="encargadoEmail"
                                placeholder="Email del encargado"
                                className="mt-2"
                                value={encargadoEmail}
                                onChange={(e) => setEncargadoEmail(e.target.value)}
                                required
                            />
                        </div>

                        <div className="col-span-full sm:col-span-3">
                            <label
                                htmlFor="encargadoPassword"
                                className="text-tremor-default font-medium text-tremor-content-strong dark:text-dark-tremor-content-strong"
                            >
                                Contraseña del Encargado
                                <span className="text-red-500">*</span>
                            </label>
                            <TextInput
                                type="password"
                                id="encargadoPassword"
                                name="encargadoPassword"
                                autoComplete="encargadoPassword"
                                placeholder="Contraseña del encargado"
                                className="mt-2"
                                value={encargadoPassword}
                                onChange={handlePasswordChange}
                                required
                            />
                        </div>

                        <div className="col-span-full sm:col-span-3">
                            <label
                                htmlFor="repeatPassword"
                                className="text-tremor-default font-medium text-tremor-content-strong dark:text-dark-tremor-content-strong"
                            >
                                Repetir Contraseña
                                <span className="text-red-500">*</span>
                            </label>
                            <TextInput
                                type="password"
                                id="repeatPassword"
                                name="repeatPassword"
                                autoComplete="repeatPassword"
                                placeholder="Repetir contraseña"
                                className="mt-2"
                                value={repeatPassword}
                                onChange={handleRepeatPasswordChange}
                                required
                            />
                        </div>

                        {!isPasswordMatch && (
                            <div className="col-span-full text-red-500 text-center">
                                Las contraseñas no coinciden.
                            </div>
                        )}

                        <div className="col-span-full flex justify-center space-x-4 mt-4">
                            <Button variant="secondary" onClick={() => {
                                setStr_nombre('');
                                setStr_direccion('');
                                setStr_telefono('');
                                setEncargadoUsername('');
                                setEncargadoEmail('');
                                setEncargadoPassword('');
                                setRepeatPassword('');
                                setFk_ferreteria(0);
                                setIsOpen(false);
                                navigate.push('/depositos');
                            }}>Cancelar</Button>
                            <Button
                                variant="primary"
                                type="submit"
                                disabled={!isPasswordMatch}
                                onMouseOver={(e) => {
                                    if (!isPasswordMatch) {
                                        e.target.title = "Las contraseñas no coinciden.";
                                    } else {
                                        e.target.title = "";
                                    }
                                }}
                            >
                                Guardar
                            </Button>
                        </div>
                    </div>
                </form>
            </DialogPanel>
        </Dialog>
    )
}
