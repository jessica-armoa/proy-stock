"use client";

import React, { useState, useEffect } from "react";
import { Card, Select, SelectItem, SearchSelect, SearchSelectItem, Table, TableBody, TableCell, TableHead, TableHeaderCell, TableRow, Button, TextInput } from "@tremor/react";
import { useRouter } from "next/navigation";
import dynamic from "next/dynamic";
import MovimientosConfig from "../../../../controladores/MovimientosConfig";
import ProductosConfig from "../../../../controladores/ProductosConfig";
import ProveedoresConfig from "../../../../controladores/ProveedoresConfig";
import withAuth from "@/components/auth/withAuth";
import MotivosPorTipoDeMovimientoConfig from "@/controladores/MotivosPorTipoDeMovimientoConfig";



// Importaciones dinámicas
const Sidebar = dynamic(() => import("@/components/barraNavegacion/Sidebar"), {
    ssr: false,
});
const DataTable = dynamic(() => import("@/components/tabla"), { ssr: false });
const Photo = dynamic(() => import("@/components/productos"), { ssr: false });

const VisualizarMovimiento = ({ params, cantElementos = 8 }) => {
    const navigate = useRouter();
    const [movimiento, setMovimiento] = useState({});
    const [producto, setProducto] = useState({});
    const [detallesMovimientos, setDetallesMovimientos] = useState([]);
    const [motivosPorTipoDeMovimiento, setMotivoPorTipoDeMovimiento] = useState([]);
    const [esEgreso, setEsEgreso] = useState(false);
    const [esIngreso, setEsIngreso] = useState(false);
    const [esCompra, setEsCompra] = useState(false);
    const [proveedor, setProveedor] = useState({});
    const router = useRouter();

    const { id } = params;

    useEffect(() => {
        const extraccionDeMovimiento = async () => {
            try {
                const respuestaMovimiento = await MovimientosConfig.getMovimientoById(id);
                const respuestaMotivosPorTipoDeMovimiento = await MotivosPorTipoDeMovimientoConfig.getMotivosPorTipoDeMovimiento();
                setMovimiento(respuestaMovimiento.data);
                setMotivoPorTipoDeMovimiento(respuestaMotivosPorTipoDeMovimiento.data);
                console.log(respuestaMovimiento.data);
            } catch (error) {
                console.log('Error al obtener el movimiento.');
            }
        };
        extraccionDeMovimiento();
    }, [id]);

    const [isDepositoOrigenVisible, setIsDepositoOrigenVisible] = useState(false);
    const [isDepositoDestinoVisible, setIsDepositoDestinoVisible] = useState(false);
    const [isTransferencia, setIsTransferencia] = useState(false);
    useEffect(() => {
        // Actualiza la visibilidad del depósito origen basado en el motivo seleccionado
        const selectedMotivo = motivosPorTipoDeMovimiento.find(motivo => motivo.id === movimiento.motivoportipodemovimientoId);
        const tipoMovimientoId = selectedMotivo ? selectedMotivo.tipodemovimientoId : null;
        setIsDepositoOrigenVisible(tipoMovimientoId === 2 || tipoMovimientoId === 3);
        setIsDepositoDestinoVisible(tipoMovimientoId === 1 || tipoMovimientoId === 3);
        setIsTransferencia(tipoMovimientoId === 3);
        setEsCompra(tipoMovimientoId === 1);
        setEsEgreso(tipoMovimientoId === 2);
        setEsIngreso(tipoMovimientoId === 1);
    }, [movimiento.motivoportipodemovimientoId, motivosPorTipoDeMovimiento]);

    useEffect(() => {
        if (movimiento.detallesDeMovimientos) {
            setDetallesMovimientos(movimiento.detallesDeMovimientos);
            console.log(movimiento.detallesDeMovimientos);
        }
    }, [movimiento]);

    return (
        <div className="max-w-4xl mx-auto p-8">
            <Card>
                <h2 className="text-2xl font-bold mb-6">Movimiento</h2>
                <form className="space-y-4">
                    <div className="grid grid-cols-1 md:grid-cols-2 gap-2">
                        <div>
                            <label htmlFor="responsable" className="block text-sm font-medium text-gray-700">Responsable</label>
                            <input
                                type="text"
                                id="responsable"
                                value={'Raul'}
                                onChange={(e) => setResponsable(e.target.value)}
                                className="mt-1 block w-full rounded-md border-gray-300 shadow-sm sm:text-sm"
                                disabled
                            />
                        </div>


                        <div>
                            <label htmlFor="fecha" className="block text-sm font-medium text-gray-700">Fecha</label>
                            <input
                                type="datetime-local"
                                id="fecha"
                                value={movimiento.date_fecha}
                                onChange={(e) => setFecha(e.target.value)}
                                className="mt-1 block w-full rounded-md border-gray-300 shadow-sm sm:text-sm"
                                disabled
                            />
                        </div>

                        <div>
                            <label
                                htmlFor="fk_motivo_por_tipo_de_movimiento"
                                className="block text-sm font-medium text-gray-700"
                            >
                                Motivo por Tipo de Movimiento
                            </label>
                            <input
                                type="text"
                                id="fk_motivo_por_tipo_de_movimiento"
                                value={movimiento.str_motivoPorTipoDeMovimiento}
                                className="mt-1 block w-full rounded-md border-gray-300 shadow-sm sm:text-sm"
                                disabled
                            />
                        </div>



                        <div className={`mb-4 ${isDepositoOrigenVisible ? 'visible' : 'invisible'}`}>
                            <label
                                htmlFor="depositoOrigen"
                                className="block text-sm font-medium text-gray-700"
                            >
                                Depósito Origen
                            </label>

                            <input
                                type="text"
                                id="depositoOrigen"
                                value={movimiento.str_depositoOrigen}
                                className="mt-1 block w-full rounded-md border-gray-300 shadow-sm sm:text-sm"
                                disabled
                            />
                        </div>

                        <div className={`mb-4 ${isDepositoDestinoVisible ? 'visible' : 'invisible'}`}>
                            <label htmlFor="fk_deposito_destino" className="block text-sm font-medium text-gray-700">Depósito Destino</label>
                            <input
                                type="text"
                                id="depositoOrigen"
                                value={movimiento.str_depositoDestino}
                                className="mt-1 block w-full rounded-md border-gray-300 shadow-sm sm:text-sm"
                                disabled
                            />
                        </div>

                        {/*{isTransferencia && (
                            <>

                                <div>
                                    <label htmlFor="timbradoRemision" className="block text-sm font-medium text-gray-700">Timbrado</label>
                                    <input
                                        type="text"
                                        id="timbradoRemision"
                                        value={timbradoRemision}
                                        onChange={(e) => setTimbradoRemision(e.target.value)}
                                        className="mt-1 block w-full rounded-md border-gray-300 shadow-sm sm:text-sm"
                                        required={isTransferencia}
                                    />
                                </div>

                                <div>
                                    <label htmlFor="numeroNotaRemision" className="block text-sm font-medium text-gray-700">Número de Nota de Remisión</label>
                                    <input
                                        type="text"
                                        id="numeroNotaRemision"
                                        value={numeroNotaRemision}
                                        onChange={(e) => setNumeroNotaRemision(e.target.value)}
                                        className="mt-1 block w-full rounded-md border-gray-300 shadow-sm sm:text-sm"
                                        required={isTransferencia}
                                    />
                                </div>



                                <div>
                                    <label htmlFor="datosVehiculo" className="block text-sm font-medium text-gray-700">Datos de Vehículo</label>
                                    <input
                                        type="text"
                                        id="datosVehiculo"
                                        value={datosVehiculo.modeloVehiculo + datosVehiculo.chapa}
                                        onChange={(e) => setDatosVehiculo(e.target.value)}
                                        className="mt-1 block w-full rounded-md border-gray-300 shadow-sm sm:text-sm"
                                        required={isTransferencia}
                                        disabled
                                    />
                                </div>

                                <div>
                                    <label htmlFor="conductor" className="block text-sm font-medium text-gray-700">Conductor</label>
                                    <input
                                        type="text"
                                        id="conductor"
                                        value={conductor.str_nombre_conductor + conductor.str_documento_conductor}
                                        onChange={(e) => setConductor(e.target.value)}
                                        className="mt-1 block w-full rounded-md border-gray-300 shadow-sm sm:text-sm"
                                        required={isTransferencia}
                                        disabled
                                    />
                                </div>
                            </>
                        )}*/}

                    </div>


                </form>
            </Card>


            <Table className="mt-8">
                <TableHead>
                    <TableRow>
                        <TableHeaderCell>#</TableHeaderCell>
                        <TableHeaderCell>Detalle</TableHeaderCell>
                        <TableHeaderCell>Cantidad</TableHeaderCell>
                        <TableHeaderCell>Precio Unitario</TableHeaderCell>
                        <TableHeaderCell>Precio Total</TableHeaderCell>
                        {/*<TableHeaderCell>Acciones</TableHeaderCell>*/}
                    </TableRow>
                </TableHead>

                <TableBody>
                    {detallesMovimientos.map((detalle) => (
                        <TableRow key={detalle.id}>
                            <TableCell>{detalle.productoId}</TableCell>
                            <TableCell>{detalle.str_producto}</TableCell>
                            <TableCell>{detalle.int_cantidad}</TableCell>
                            <TableCell>{detalle.dec_costo}</TableCell>
                            <TableCell>{detalle.int_cantidad * detalle.dec_costo}</TableCell>
                            {/*<TableCell>
                                <button
                                    type="button"
                                    onClick={() => manejarQuitarDetalle(detalle.idDetalle)}
                                    className="inline-flex items-center px-3 py-2 border border-transparent text-sm font-medium rounded-md shadow-sm text-white bg-red-600 hover:bg-red-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-red-500"
                                >
                                    Quitar
                                </button>
                            </TableCell>*/}
                        </TableRow>
                    ))}
                </TableBody>
            </Table>
            <div className="flex justify-end mt-6 gap-2">
                <Button type="button" variant="primary" color="gray" onClick={() => navigate.push('/movimientos')}>Volver</Button>
            </div>

        </div>
    );
};

export default withAuth(VisualizarMovimiento);