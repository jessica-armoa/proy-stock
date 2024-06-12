'use client'

import { useEffect, useState } from "react";
import { Card, Select, SelectItem, SearchSelect, SearchSelectItem, Table, TableBody, TableCell, TableHead, TableHeaderCell, TableRow } from "@tremor/react";
import { useRouter } from 'next/navigation';
import DepositosConfig from "../../../controladores/DepositosConfig";
import ProductosConfig from "../../../controladores/ProductosConfig";
import MovimientosConfig from "../../../controladores/MovimientosConfig";

let detalleIdCounter = 0;

export default function FormularioMovimientos() {
    const navigate = useRouter();

    //const [numeroDocumento, setNumeroDocumento] = useState('');

    const [timbrado, setTimbrado] = useState('');

    //Encabezado de Movimientos
    const [responsable, setResponsable] = useState('');
    const [fecha, setFecha] = useState('');
    const [fk_motivoId, setFk_MotivoId] = useState(0);
    const [motivos, setMotivos] = useState([]);
    const tiposDeMovimientos = [
        { id: 1, str_descripcion: 'ingreso' },
        { id: 2, str_descripcion: 'egreso' },
        { id: 3, str_descripcion: 'transferencia' }
    ];
    const [fk_tipo_de_movimiento, setFk_tipo_de_movimiento] = useState(0);
    const motivosIngreso = [
        { id: 2, str_motivo: 'Compra' },
        { id: 3, str_motivo: 'Devolucion de cliente' }
    ];

    const motivosEgreso = [
        { id: 1, str_motivo: 'Venta Cliente' },
        { id: 4, str_motivo: 'Devolucion a proveedor' },
        { id: 7, str_motivo: 'Perdida por deterioro' }
    ];

    const motivosTransferencia = [{ id: 8, str_motivo: 'transferencia' }];

    useEffect(() => {
        // Función para obtener los motivos según el tipo de movimiento seleccionado
        const obtenerMotivos = (tipoMovimientoId) => {
            switch (tipoMovimientoId) {
                case 1: // Ingreso
                    setMotivos(motivosIngreso);
                    break;
                case 2: // Egreso
                    setMotivos(motivosEgreso);
                    break;
                case 3: // Transferencia
                    setMotivos(motivosTransferencia);
                    break;
                default:
                    setMotivos([]); // Si el tipo de movimiento no coincide con ninguno de los anteriores, se establecen motivos vacíos
                    break;
            }
        };

        // Llamar a la función para establecer los motivos al principio y cada vez que cambie el tipo de movimiento seleccionado
        obtenerMotivos(fk_tipo_de_movimiento);
    }, [fk_tipo_de_movimiento]);
    //const [detallesMovimientos, setDetallesMovimientos] = useState([{ idDetalle: detalleIdCounter++, descripcion: '', cantidad: 1, precio: 0, total: 0 }])

    //Este es el arreglo de detalles que se le va a pasar a detalles de movimientos
    const [detallesMovimientos, setDetallesMovimientos] = useState([]);
    const [depositoOrigen, setDepositoOrigen] = useState(0);
    const [iva, setIva] = useState(10);

    //Manejo de listas de productos y Depósitos de Origen y Destino para los movimientos
    const [fk_producto, setFk_producto] = useState(0);
    const [arregloProductos, setArreglo_productos] = useState([]);
    const [fk_deposito_origen, setFk_deposito_origen] = useState(null);
    const [fk_deposito_destino, setFk_deposito_destino] = useState(null);
    const [depositos, setDepositos] = useState([]);
    const [depositosDestinos, setDepositosDestinos] = useState([]);
    //evento para mostrar solo destinos que no sean igual que su deposito origen
    const opcionesFiltradas = depositosDestinos.filter(opcion => opcion.id !== fk_deposito_origen);
    //Con esta función devolvemos las listas de productos y la lista de depósitos tanto para origen como destino.
    useEffect(() => {
        const extraccionDepositos = async () => {
            try {
                const respuestaDepositos = await DepositosConfig.getDepositos();
                const respuestaProductos = await ProductosConfig.getProductos();
                setDepositos(respuestaDepositos.data);
                setDepositosDestinos(respuestaDepositos.data);
                setArreglo_productos(respuestaProductos.data);
            } catch (error) {
                console.error('Error al obtener lista de depositos: ', error);
            }
        }
        extraccionDepositos();
    }, []);






    const [motivo, setMotivo] = useState('');

    //Estado para detalle temporal
    const [detalleTemp, setDetalleTemp] = useState({ idProducto: 0, descripcion: '', cantidad: 1, precio: 0, total: 0 });

    const manejarCambioDetalleTemp = (nombre, valor) => {
        setDetalleTemp(prevDetalle => ({
            ...prevDetalle,
            [nombre]: valor,
            total: nombre === 'precio' || nombre === 'cantidad' ? (nombre === 'precio' ? parseFloat(valor) : prevDetalle.precio) * (nombre === 'cantidad' ? parseInt(valor) : prevDetalle.cantidad) : prevDetalle.total
        }));
    };

    const manejarAgregarDetalle = () => {
        const productoSeleccionado = arregloProductos.find(producto => {
            if (producto.id === +fk_producto) {
                return producto;
            }
        });
        //console.log(detallesMovimientos);
        const nuevoId = detallesMovimientos.length;
        setDetallesMovimientos([...detallesMovimientos, { ...detalleTemp, idDetalle: nuevoId, idProducto: productoSeleccionado.id, descripcion: productoSeleccionado.str_nombre, cantidad: detalleTemp.cantidad, precio: detalleTemp.precio, total: detalleTemp.total }]);
        setDetalleTemp({ idProducto: 0, descripcion: '', cantidad: 1, precio: 0, total: 0 });
        setFk_producto(0); // Reset product selection after adding
    };

    useEffect(() => {
        console.log(detallesMovimientos);
    }, [detallesMovimientos]);

    const manejarQuitarDetalle = (id) => {
        setDetallesMovimientos(detallesMovimientos.filter(detalle => detalle.idDetalle !== id));
    };

    // Campos adicionales para transferencia
    const [numeroNotaRemision, setNumeroNotaRemision] = useState('');
    const [timbradoRemision, setTimbradoRemision] = useState('');
    const [depositoDestino, setDepositoDestino] = useState('');
    const [datosVehiculo, setDatosVehiculo] = useState('');
    const [conductor, setConductor] = useState('');

    //<-- Estas funciones agregaban producto pero lo que agregamos son detalles y no productos.-->
    //<-- Estas funciones no se están utilizando así que se van a eliminar más adelante.-->
    {/*const manejarCambioProducto = (id, nombre, valor) => {
        const detallesLista = detallesMovimientos.map(detalle =>
            detalle.idDetalle === id ? { ...detalle, [nombre]: valor, total: nombre === 'precio' || nombre === 'cantidad' ? (nombre === 'precio' ? detalle.cantidad * parseFloat(valor) : parseInt(valor) * detalle.precio) : detalle.total } : detalle
        );
        setDetallesMovimientos(detallesLista);
    };

    const manejarAgregarProducto = () => {
        setDetallesMovimientos([...detallesMovimientos, { idDetalle: 0, descripcion: '', cantidad: 1, precio: 0, total: 0 }]);
    };

    const manejarQuitarProducto = (id) => {
        setDetallesMovimientos(detallesMovimientos.filter(detalle => detalle.idDetalle != id));
    };*/}

    //Estas funciones se utilizan para manejar los detalles del movimientos
    // manejarAgregarDetalle




    const calcularSubTotal = () => {
        return detallesMovimientos.reduce((acumulador, detalle) => acumulador + detalle.total, 0);
    };

    const calcularIva = (subTotal) => {
        return (iva / 110) * subTotal;
    };

    const calcularTotal = (subTotal) => {
        return subTotal;
    };
let count = 100
    const handleSubmit = async (event) => {
        event.preventDefault();
        try {
            const movimientoActual = {
                "id": count++,
                "date_fecha": fecha,
                "tipoDeMovimientoId": fk_tipo_de_movimiento,
                "depositoOrigenId": fk_deposito_origen,
                "depositoDestinoId": fk_deposito_destino,
                "bool_borrado": false,
                "detallesDeMovimientos": detallesMovimientos.map(detalle => ({
                    "id": count++,
                    "int_cantidad": detalle.cantidad,
                    "productoId": detalle.idProducto,
                    "bool_borrado": false
                }))
            }
            console.log('Movimiento enviado', movimientoActual);
        
            const movimientoCreado = await MovimientosConfig.postMovimiento(movimientoActual);


        } catch (error) {
            console.error('Error al enviar los datos del formulario: ', error);
        }

    };

    const formatNumber = (number) => {
        return number.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ',');
    };

    const unformatNumber = (number) => {
        return number.replace(/,/g, '');
    };

    const subTotal = calcularSubTotal();
    const IVA = calcularIva(subTotal);
    const total = calcularTotal(subTotal);


    //const motivos = tipoMovimiento === 'transferencia' ? motivosTransferencia : ('ingreso' ? motivosIngreso : motivosEgreso);


    const esTransferencia = fk_tipo_de_movimiento === 3;

    return (
        <>
            <div className="max-w-4xl mx-auto p-8">
                <Card>
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
                                    htmlFor="fk_tipo_de_movimiento"
                                    className="block text-sm font-medium text-gray-700"
                                >
                                    Tipo de Movimiento
                                </label>
                                {/*<select
                                    id="fk_tipo_de_movimiento"
                                    name="fk_tipo_de_movimiento"
                                    value={fk_tipo_de_movimiento}
                                    onChange={(e) => {
                                        setFk_tipo_de_movimiento(e.target.value);
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
                                    <option value="transferencia">Transferencia</option>
                                </select>*/}
                                <SearchSelect id="fk_tipo_de_movimiento" className='mt-2' placeholder='Tipo de Movimiento' value={fk_tipo_de_movimiento} onValueChange={(value) => {
                                    setFk_tipo_de_movimiento(parseInt(value));
                                    setMotivo('');
                                }}>
                                    {tiposDeMovimientos.map(tiposMovimientos => (
                                        <SearchSelectItem key={tiposMovimientos.id} value={tiposMovimientos.id}>{tiposMovimientos.str_descripcion}</SearchSelectItem>
                                    ))}
                                </SearchSelect>
                            </div>

                            <div>
                                <label
                                    htmlFor="motivo"
                                    className="block text-sm font-medium text-gray-700"
                                >
                                    Motivo
                                </label>
                                {/*<select
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
                                        <option key={motivo.id} value={motivo.id}>
                                            {motivo.str}
                                        </option>
                                    ))}
                                </select>*/}
                                <SearchSelect id="fk_motivoId" className='mt-2' placeholder='Motivo' value={fk_motivoId} onValueChange={(value) => setFk_MotivoId(parseInt(value))}>
                                    {motivos.map(motivosSeleccionados => (
                                        <SearchSelectItem key={motivosSeleccionados.id} value={motivosSeleccionados.id}>{motivosSeleccionados.str_motivo}</SearchSelectItem>
                                    ))}
                                </SearchSelect>
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
                                        <label htmlFor="fk_deposito_destino" className="block text-sm font-medium text-gray-700">Depósito Destino</label>
                                        {/*<select
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
                                        </select>*/}
                                        <SearchSelect id="fk_deposito_destino" className='mt-2' placeholder='Depósito' value={fk_deposito_destino} onValueChange={(value) => setFk_deposito_destino(parseInt(value))}>
                                            {opcionesFiltradas.map(depositoDestino => (

                                                <SearchSelectItem key={depositoDestino.id} value={depositoDestino.id}>{depositoDestino.str_nombre}</SearchSelectItem>
                                            ))}
                                        </SearchSelect>
                                    </div>
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
                            <button type="button" onClick={() => navigate.push('/')} className="px-4 py-2 bg-gray-400 text-white rounded-md">Cancelar</button>
                            <button
                                disabled={detallesMovimientos.length === 0}
                                type="submit"
                                className="inline-flex items-center px-4 py-2 border border-transparent text-sm font-medium rounded-md shadow-sm text-white bg-green-600 hover:bg-green-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-green-500"
                            >
                                Guardar Movimiento
                            </button>
                        </div>
                    </form>
                </Card>

                <form id="formDetalle">
                    <div>
                        <h3 className="text-xl font-semibold mb-4">Detalles de movimiento</h3>
                    </div>
                    <div className="grid grid-cols-1 md:grid-cols-6 gap-4 items-end mb-4">
                        <div className="md:col-span-2">
                            <label htmlFor="descripcion" className="block text-sm font-medium text-gray-700">Descripción/Producto</label>

                            {/*<SearchSelect id="descripcion" value={detalleTemp.descripcion} placeholder="Seleccionar Producto" onValueChange={(value) => manejarCambioDetalleTemp('descripcion', value)} className='mt-2'>
                                {arregloProductos.map(producto => (
                                    <SearchSelectItem key={producto.id} value={producto.nombre}>{producto.nombre}</SearchSelectItem>
                                ))}
                            </SearchSelect>*/}
                            <SearchSelect id="fk_producto" className='mt-2' placeholder='Producto' value={fk_producto} onValueChange={(value) => setFk_producto(parseInt(value))}>
                                {arregloProductos.map(producto => (
                                    <SearchSelectItem key={producto.id} value={producto.id}>{producto.str_nombre}</SearchSelectItem>
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
                            <TableHeaderCell>#</TableHeaderCell>
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
                                <TableCell>{detalle.idProducto}</TableCell>
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