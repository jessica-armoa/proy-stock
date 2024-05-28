'use client'

import { useEffect, useState } from "react";
import { Select, SelectItem, SearchSelect, SearchSelectItem, Table, TableBody, TableCell, TableHead, TableHeaderCell, TableRow } from "@tremor/react";
import { useNavigate } from "react-router-dom";
import DepositosConfig from '../depositos/DepositosConfig';
//import { v4 as uuidv4 } from 'uuid';
let detalleIdCounter = 0;
let arregloProductos = [
    { id: 1, nombre: "Producto 1", precio: 10000 },
    { id: 2, nombre: "Producto 2", precio: 15000 },
    { id: 3, nombre: "Producto 3", precio: 20000 },
    { id: 4, nombre: "Producto 4", precio: 25000 }];

export default function FormularioMovimientos() {
    const navigate = useNavigate();

    //const [numeroDocumento, setNumeroDocumento] = useState('');
    const [timbrado, setTimbrado] = useState('');
    const [responsable, setResponsable] = useState('');
    const [fecha, setFecha] = useState('');
    const [tipoMovimiento, setTipoMovimiento] = useState('');
    //const [detallesMovimientos, setDetallesMovimientos] = useState([{ idDetalle: detalleIdCounter++, descripcion: '', cantidad: 1, precio: 0, total: 0 }])
    const [detallesMovimientos, setDetallesMovimientos] = useState([]);
    const [depositoOrigen, setDepositoOrigen] = useState(0);
    const [iva, setIva] = useState(10);


    const [fk_deposito_origen, setFk_deposito_origen] = useState(0);
    const [depositos, setDepositos] = useState([]);
    useEffect(() => {
        const extraccionDepositos = async () => {
            try {
                const respuestaDepositos = await DepositosConfig.getDeposito();
                setDepositos(respuestaDepositos.data);
            } catch (error) {
                console.error('Error al obtener lista de depositos: ', error);
            }
        }
        extraccionDepositos();
    }, []);

    const [motivo, setMotivo] = useState('');

    //Estado para detalle temporal
    const [detalleTemp, setDetalleTemp] = useState({ descripcion: '', cantidad: 1, precio: 0, total: 0 });

    const manejarCambioDetalleTemp = (nombre, valor) => {
        setDetalleTemp(prevDetalle => ({
            ...prevDetalle,
            [nombre]: valor,
            total: nombre === 'precio' || nombre === 'cantidad' ? (nombre === 'precio' ? parseFloat(valor) : prevDetalle.precio) * (nombre === 'cantidad' ? parseInt(valor) : prevDetalle.cantidad) : prevDetalle.total
        }));
    };

    // Campos adicionales para transferencia
    const [numeroNotaRemision, setNumeroNotaRemision] = useState('');
    const [timbradoRemision, setTimbradoRemision] = useState('');
    const [depositoDestino, setDepositoDestino] = useState('');
    const [datosVehiculo, setDatosVehiculo] = useState('');
    const [conductor, setConductor] = useState('');

    const manejarCambioProducto = (id, nombre, valor) => {
        const detallesLista = detallesMovimientos.map(detalle =>
            detalle.idDetalle === id ? { ...detalle, [nombre]: valor, total: nombre === 'precio' || nombre === 'cantidad' ? (nombre === 'precio' ? detalle.cantidad * parseFloat(valor) : parseInt(valor) * detalle.precio) : detalle.total } : detalle
        );
        setDetallesMovimientos(detallesLista);
    };

    const manejarAgregarProducto = () => {
        setDetallesMovimientos([...detallesMovimientos, { idDetalle: 0, descripcion: '', cantidad: 1, precio: 0, total: 0 }]);
    };

    const manejarAgregarDetalle = () => {
        const nuevoId = detallesMovimientos.length ? Math.max(detallesMovimientos.map(detalle => detalle.idDetalle)) + 1 : 1;
        setDetallesMovimientos([...detallesMovimientos, { ...detalleTemp, idDetalle: nuevoId }]);
        setDetalleTemp({ descripcion: '', cantidad: 1, precio: 0, total: 0 });
    };

    const manejarQuitarDetalle = (id) => {
        setDetallesMovimientos(detallesMovimientos.filter(detalle => detalle.idDetalle !== id));
    };

    const manejarQuitarProducto = (id) => {
        setDetallesMovimientos(detallesMovimientos.filter(detalle => detalle.idDetalle != id));
    };

    const calcularSubTotal = () => {
        return detallesMovimientos.reduce((acumulador, detalle) => acumulador + detalle.total, 0);
    };

    const calcularIva = (subTotal) => {
        return (iva / 110) * subTotal;
    };

    const calcularTotal = (subTotal) => {
        return subTotal;
    };

    const handleSubmit = (e) => {
        e.preventDefault();
        console.log('Factura enviada', { responsable, fecha, productos: detallesMovimientos, iva });
    };

    const subTotal = calcularSubTotal();
    const IVA = calcularIva(subTotal);
    const total = calcularTotal(subTotal);

    const motivosIngreso = [
        { value: 'compra', label: 'Compra' },
        { value: 'ajuste_stock', label: 'Ajuste de Stock' },
        { value: 'regalo', label: 'Regalo' }
    ];

    const motivosEgreso = [
        { value: 'venta', label: 'Venta' },
        { value: 'ajuste_stock', label: 'Ajuste de Stock' },
        { value: 'transferencia', label: 'Transferencia' },
        { value: 'fallo', label: 'Rotura/Fallo/Inservible' }
    ];

    const motivos = tipoMovimiento === 'ingreso' ? motivosIngreso : motivosEgreso;

    const esTransferencia = motivo === 'transferencia';

    return (
        <>
            <div className="max-w-4xl mx-auto p-8">
                <h2 className="text-2xl font-bold mb-6">Nuevo Movimiento</h2>
                <form onSubmit={handleSubmit} className="space-y-4">
                    <div className="grid grid-cols-1 md:grid-cols-2 gap-2">
                        <div>
                            <label htmlFor="responsable" className="block text-sm font-medium text-gray-700">Responsable</label>
                            <input
                                type="text"
                                id="responsable"
                                value={responsable}
                                onChange={(e) => setResponsable(e.target.value)}
                                className="mt-1 block w-full rounded-md border-gray-300 shadow-sm sm:text-sm"
                                required
                            />
                        </div>


                        <div>
                            <label htmlFor="fecha" className="block text-sm font-medium text-gray-700">Fecha</label>
                            <input
                                type="date"
                                id="fecha"
                                value={fecha}
                                onChange={(e) => setFecha(e.target.value)}
                                className="mt-1 block w-full rounded-md border-gray-300 shadow-sm sm:text-sm"
                                required
                            />
                        </div>

                        <div>
                            <label
                                htmlFor="tipoMovimiento"
                                className="block text-sm font-medium text-gray-700"
                            >
                                Tipo de Movimiento
                            </label>
                            <select
                                id="tipoMovimiento"
                                name="tipoMovimiento"
                                value={tipoMovimiento}
                                onChange={(e) => {
                                    setTipoMovimiento(e.target.value);
                                    setMotivo('');
                                }}
                                className="mt-1 block w-full pl-3 pr-10 py-2 text-base border-gray-300 focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 sm:text-sm rounded-md"
                                required
                            >
                                <option value="" disabled>
                                    Seleccione un tipo
                                </option>
                                <option value="ingreso">Ingreso</option>
                                <option value="egreso">Egreso</option>
                            </select>
                        </div>

                        <div>
                            <label
                                htmlFor="motivo"
                                className="block text-sm font-medium text-gray-700"
                            >
                                Motivo
                            </label>
                            <select
                                id="motivo"
                                name="motivo"
                                value={motivo}
                                onChange={(e) => setMotivo(e.target.value)}
                                className="mt-1 block w-full pl-3 pr-10 py-2 text-base border-gray-300 focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 sm:text-sm rounded-md"
                                required
                            >
                                <option value="" disabled>
                                    Seleccione un Motivo
                                </option>
                                {motivos.map(motivo => (
                                    <option key={motivo.value} value={motivo.value}>
                                        {motivo.label}
                                    </option>
                                ))}
                            </select>
                        </div>

                        <div>
                            <label
                                htmlFor="depositoOrigen"
                                className="block text-sm font-medium text-gray-700"
                            >
                                Depósito Origen
                            </label>
                            <SearchSelect id="fk_deposito_origen" className='mt-2' placeholder='Depósito' value={fk_deposito_origen} onValueChange={(value) => setFk_deposito_origen(parseInt(value))}>
                                {depositos.map(deposito => (
                                    <SearchSelectItem key={deposito.id} value={deposito.id}>{deposito.str_nombre}</SearchSelectItem>
                                ))}
                            </SearchSelect>
                        </div>



                        {esTransferencia && (
                            <>
                                <div>
                                    <label htmlFor="timbradoRemision" className="block text-sm font-medium text-gray-700">Timbrado</label>
                                    <input
                                        type="text"
                                        id="timbradoRemision"
                                        value={timbradoRemision}
                                        onChange={(e) => setTimbradoRemision(e.target.value)}
                                        className="mt-1 block w-full rounded-md border-gray-300 shadow-sm sm:text-sm"
                                        required={esTransferencia}
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
                                        required={esTransferencia}
                                    />
                                </div>

                                <div>
                                    <label htmlFor="depositoDestino" className="block text-sm font-medium text-gray-700">Depósito Destino</label>
                                    <select
                                        id="depositoDestino"
                                        name="depositoDestino"
                                        value={depositoDestino}
                                        onChange={(e) => setDepositoDestino(e.target.value)}
                                        className="mt-1 block w-full pl-3 pr-10 py-2 text-base border-gray-300 focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 sm:text-sm rounded-md"
                                        required={esTransferencia}
                                    >
                                        <option value="" disabled>Seleccione un Depósito</option>
                                        <option value="1">1</option>
                                        <option value="2">2</option>
                                    </select>
                                </div>

                                <div>
                                    <label htmlFor="datosVehiculo" className="block text-sm font-medium text-gray-700">Datos de Vehículo</label>
                                    <input
                                        type="text"
                                        id="datosVehiculo"
                                        value={datosVehiculo}
                                        onChange={(e) => setDatosVehiculo(e.target.value)}
                                        className="mt-1 block w-full rounded-md border-gray-300 shadow-sm sm:text-sm"
                                        required={esTransferencia}
                                    />
                                </div>

                                <div>
                                    <label htmlFor="conductor" className="block text-sm font-medium text-gray-700">Conductor</label>
                                    <input
                                        type="text"
                                        id="conductor"
                                        value={conductor}
                                        onChange={(e) => setConductor(e.target.value)}
                                        className="mt-1 block w-full rounded-md border-gray-300 shadow-sm sm:text-sm"
                                        required={esTransferencia}
                                    />
                                </div>
                            </>
                        )}

                    </div>

                    <div className="flex justify-end mt-6 gap-2">
                        <button type="button" onClick={() => navigate('/')} className="px-4 py-2 bg-gray-400 text-white rounded-md">Cancelar</button>
                        <button
                            type="submit"
                            className="inline-flex items-center px-4 py-2 border border-transparent text-sm font-medium rounded-md shadow-sm text-white bg-green-600 hover:bg-green-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-green-500"
                        >
                            Guardar Movimiento
                        </button>
                    </div>
                    <div>
                        <h3 className="text-xl font-semibold mb-4">Detalles de movimiento</h3>
                    </div>
                </form>

                <form id="formDetalle">
                    <div className="grid grid-cols-1 md:grid-cols-6 gap-4 items-end mb-4">
                        <div className="md:col-span-2">
                            <label htmlFor="descripcion" className="block text-sm font-medium text-gray-700">Descripción/Producto</label>

                            <SearchSelect id="descripcion" value={detalleTemp.descripcion} placeholder="Seleccionar Producto" onValueChange={(value) => manejarCambioDetalleTemp('descripcion', value)} className='mt-2'>
                                {arregloProductos.map(producto => (
                                    <SearchSelectItem key={producto.id} value={producto.nombre}>{producto.nombre}</SearchSelectItem>
                                ))}
                            </SearchSelect>
                        </div>

                        <div>
                            <label htmlFor="cantidad" className="block text-sm font-medium text-gray-700">Cantidad</label>
                            <input
                                type="number"
                                id="cantidad"
                                value={detalleTemp.cantidad}
                                onChange={(e) => manejarCambioDetalleTemp('cantidad', parseInt(e.target.value))}
                                className="mt-1 block w-full rounded-md border-gray-300 shadow-sm sm:text-sm"
                                min="1"
                                required
                            />
                        </div>

                        <div>
                            <label htmlFor="precio" className="block text-sm font-medium text-gray-700">Precio</label>
                            <input
                                type="number"
                                id="precio"
                                value={detalleTemp.precio}
                                onChange={(e) => manejarCambioDetalleTemp('precio', parseFloat(e.target.value))}
                                className="mt-1 block w-full rounded-md border-gray-300 shadow-sm sm:text-sm"
                                min="0"
                                required
                            />
                        </div>

                        <div>
                            <label htmlFor="total" className="block text-sm font-medium text-gray-700">Total</label>
                            <div
                                type="number"
                                id="total"
                                value={detalleTemp.total}

                                readOnly
                                className="mt-1 block w-full rounded-md border-gray-300 shadow-sm sm:text-sm bg-gray-100"
                            >{detalleTemp.total}</div>
                        </div>

                        <div className="flex justify-end mt-6">
                            <button
                                type="button"
                                onClick={manejarAgregarDetalle}
                                className="inline-flex items-center px-4 py-2 border border-transparent text-sm font-medium rounded-md shadow-sm text-white bg-indigo-600 hover:bg-indigo-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-indigo-500"
                            >
                                Agregar
                            </button>
                        </div>
                    </div>
                </form>
                <Table className="mt-8">
                    <TableHead>
                        <TableRow>
                            <TableHeaderCell>Detalle</TableHeaderCell>
                            <TableHeaderCell>Cantidad</TableHeaderCell>
                            <TableHeaderCell>Precio Unitario</TableHeaderCell>
                            <TableHeaderCell>Precio Total</TableHeaderCell>
                            <TableHeaderCell>Acciones</TableHeaderCell>
                        </TableRow>
                    </TableHead>

                    <TableBody>
                        {detallesMovimientos.map((detalle, index) => (
                            <TableRow key={detalle.idDetalle}>
                                <TableCell>{detalle.descripcion}</TableCell>
                                <TableCell>{detalle.cantidad}</TableCell>
                                <TableCell>{detalle.precio}</TableCell>
                                <TableCell>{detalle.total}</TableCell>
                                <TableCell>
                                    <button
                                        type="button"
                                        onClick={() => manejarQuitarDetalle(detalle.idDetalle)}
                                        className="inline-flex items-center px-3 py-2 border border-transparent text-sm font-medium rounded-md shadow-sm text-white bg-red-600 hover:bg-red-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-red-500"
                                    >
                                        Quitar
                                    </button>
                                </TableCell>
                            </TableRow>
                        ))}
                    </TableBody>
                </Table>

                {/*<div className="grid grid-cols-1 md:grid-cols-6 gap-4 items-end mb-4">
                    <div className="md:col-span-1">
                        <label htmlFor="iva" className="block text-sm font-medium text-gray-700">IVA (%)</label>
                        <input
                            type="number"
                            id="iva"
                            value={iva}
                            onChange={(e) => setIva(value)}
                            className="mt-1 block w-full rounded-md border-gray-300 shadow-sm sm:text-sm bg-gray-100"
                            min="0"
                            readOnly
                            required
                        />
                    </div>
                    <div>
                            <label className="block text-sm font-medium text-gray-700">Subtotal</label>
                            <p className="mt-1 block w-full rounded-md border-gray-300 shadow-sm sm:text-sm" onChange={subTotal}>{subTotal.toFixed(2)}</p>
                    </div>
                    <div>
                        <label className="block text-sm font-medium text-gray-700">Total IVA</label>
                        <p className="mt-1 text-gray-900">{IVA.toFixed(2)}</p>
                    </div>
                    <div>
                        <label className="block text-sm font-medium text-gray-700">Total</label>
                        <p className="mt-1 text-gray-900">{total.toFixed(2)}</p>
                    </div>
                </div>*/}
            </div>
        </>
    )
}