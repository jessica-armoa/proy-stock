'use client'

import { useEffect, useState } from "react";
import { Card, Select, SelectItem, SearchSelect, SearchSelectItem, Table, TableBody, TableCell, TableHead, TableHeaderCell, TableRow, Button } from "@tremor/react";
import { useRouter } from 'next/navigation';
import DepositosConfig from "../../../controladores/DepositosConfig";
import ProductosConfig from "../../../controladores/ProductosConfig";
import MovimientosConfig from "../../../controladores/MovimientosConfig";
import TiposDeMovimientosConfig from "@/controladores/TiposDeMovimientosConfig";
import MotivosConfig from "@/controladores/MotivosConfig";
import MotivosPorTipoDeMovimientoConfig from "@/controladores/MotivosPorTipoDeMovimientoConfig";
import Swal from "sweetalert2";

let detalleIdCounter = 0;

export default function FormularioMovimientos() {
    const navigate = useRouter();

    const [timbrado, setTimbrado] = useState('');

    //Encabezado de Movimientos
    const [responsable, setResponsable] = useState('');
    const [fecha, setFecha] = useState('');
    //const [fk_motivoId, setFk_MotivoId] = useState(0);
    const [motivos, setMotivos] = useState([]);
    const [tiposDeMovimientos, setTiposDeMovimientos] = useState([]);
    const [motivosPorTipoDeMovimiento, setMotivosPorTipoDeMovimiento] = useState([]);
    const [fk_motivo_por_tipo_de_movimiento, setFk_motivo_por_tipo_de_movimiento] = useState(null);
   
    const [fk_tipo_de_movimiento, setFk_tipo_de_movimiento] = useState(0);


   

    //Este es el arreglo de detalles que se le va a pasar a detalles de movimientos
    const [detallesMovimientos, setDetallesMovimientos] = useState([]);
    const [depositoOrigen, setDepositoOrigen] = useState(0);
    const [iva, setIva] = useState(10);

    //Manejo de listas de productos y Depósitos de Origen y Destino para los movimientos
    const [fk_producto, setFk_producto] = useState(0);
    const [arregloProductos, setArreglo_productos] = useState([]);
    const [fk_deposito_origen, setFk_deposito_origen] = useState(0);
    const [fk_deposito_destino, setFk_deposito_destino] = useState(0);
    const [depositos, setDepositos] = useState([]);
    const [depositosDestinos, setDepositosDestinos] = useState([]);
    //evento para mostrar solo destinos que no sean igual que su deposito origen
    const opcionesFiltradas = depositosDestinos.filter(opcion => opcion.id !== fk_deposito_origen);
    //Con esta función devolvemos las listas de productos y la lista de depósitos tanto para origen como destino.
    useEffect(() => {
        const extraccionDepositos = async () => {
            try {
                const [respuestaDepositos, respuestaProductos] = await Promise.all([
                    DepositosConfig.getDepositos(),
                    ProductosConfig.getProductos()
                ]);
               
                setDepositos(respuestaDepositos.data);
                setDepositosDestinos(respuestaDepositos.data);
                setArreglo_productos(respuestaProductos.data);


                //console.log(respuestaTiposDeMovimientos);
            } catch (error) {
                console.error('Error al obtener lista de depositos: ', error);
            }
        }
        extraccionDepositos();
    }, []);

    const [userDepositoId, setUserDepositoId] = useState(null);
    const [rol, setRol] = useState(null);
    const [depositoEncargado, setDepositoEncargado] = useState(null);
    const [selectedItem, setSelectedItem] = useState('');
    useEffect(() => {
        const userData = localStorage.getItem("user");

        if (userData) {
            const user = JSON.parse(userData);
            setResponsable(user.userName);
            setRol(user.role);
            const userDeposito = depositos.find(deposito => deposito.encargadoUsername === user.userName);
            if (userDeposito) {
                setUserDepositoId(userDeposito.id);
            } else {
                setUserDepositoId(null);
            }
            console.log(userDeposito ? userDeposito.id : "No se encontró el depósito");
            //console.log(userDepositoId);
            //console.log(user.deposito);
        }

        const storedSelectedItem = localStorage.getItem('selectedItem');
        if (storedSelectedItem) {
            setSelectedItem(storedSelectedItem);
        }
    }, [depositos]);

    useEffect(() => {
        const extraccionDeMotivosPorTipoDeMovimiento = async () => {
            try {
    
                const respuestaTiposDeMovimientos = await TiposDeMovimientosConfig.getTiposDeMovimiento();
                const respuestaMotivos = await MotivosConfig.getMotivos();
                const respuestaMotivosPorTipoDeMovimiento = await MotivosPorTipoDeMovimientoConfig.getMotivosPorTipoDeMovimiento();
                setTiposDeMovimientos(respuestaTiposDeMovimientos.data);
                setMotivos(respuestaMotivos.data);
                setMotivosPorTipoDeMovimiento(respuestaMotivosPorTipoDeMovimiento.data);
                console.log(respuestaMotivosPorTipoDeMovimiento.data);    
                console.log(respuestaMotivos.data);
                console.log(respuestaTiposDeMovimientos.data);            
            } catch (error) {
                console.log('Error al obtener Motivos por tipo de Movimiento', error);
            }
        }
        extraccionDeMotivosPorTipoDeMovimiento();
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
        console.log(fk_motivo_por_tipo_de_movimiento);
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
                "date_fecha": fecha,
                "bool_borrado": false,
                "detallesDeMovimientos": detallesMovimientos.map(detalle => ({
                    "int_cantidad": detalle.cantidad,
                    "dec_costo": detalle.precio,
                    "productoId": detalle.idProducto,
                    "bool_borrado": false
                }))
            }
            console.log('Movimiento enviado', movimientoActual);

            const movimientoCreado = await MovimientosConfig.postMovimiento(fk_motivo_por_tipo_de_movimiento, fk_deposito_origen, fk_deposito_destino, movimientoActual).then(() => {
                Swal.fire('Guardado', 'El movimiento fue creado exitosamente.', 'success');
            });


        } catch (error) {
            console.error('Error al enviar los datos del formulario: ', error);
            Swal.fire(
                'Error',
                'Oops! ocurrió un error al intentar guardar el movimiento.',
                'error'
            );
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


    //const esTransferencia = fk_tipo_de_movimiento === 3;
    const [isDepositoOrigenVisible, setIsDepositoOrigenVisible] = useState(false);
    const [isDepositoDestinoVisible, setIsDepositoDestinoVisible] = useState(false);
    const [isTransferencia, setIsTransferencia] = useState(false);
    useEffect(() => {
        // Actualiza la visibilidad del depósito origen basado en el motivo seleccionado
        const selectedMotivo = motivosPorTipoDeMovimiento.find(motivo => motivo.id === fk_motivo_por_tipo_de_movimiento);
        const tipoMovimientoId = selectedMotivo ? selectedMotivo.tipodemovimientoId : null;
        setIsDepositoOrigenVisible(tipoMovimientoId === 2 || tipoMovimientoId === 3);
        setIsDepositoDestinoVisible(tipoMovimientoId === 1 || tipoMovimientoId === 3);
        setIsTransferencia(tipoMovimientoId === 3);
    }, [fk_motivo_por_tipo_de_movimiento, motivosPorTipoDeMovimiento]);

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
                                    type="datetime-local"
                                    id="fecha"
                                    value={fecha}
                                    onChange={(e) => setFecha(e.target.value)}
                                    className="mt-1 block w-full rounded-md border-gray-300 shadow-sm sm:text-sm"
                                    required
                                />
                            </div>

                            <div>
                                <label
                                    htmlFor="fk_motivo_por_tipo_de_movimiento"
                                    className="block text-sm font-medium text-gray-700"
                                >
                                    Motivo por Tipo de Movimiento
                                </label>
                                
                                <SearchSelect id="fk_motivo_por_tipo_de_movimiento" className='mt-2' placeholder='Motivo por Tipo de Movimiento' value={fk_motivo_por_tipo_de_movimiento} onValueChange={(value) => {
                                    setFk_motivo_por_tipo_de_movimiento(parseInt(value));
                                    console.log(fk_motivo_por_tipo_de_movimiento);
                                    //setMotivo('');
                                }}>
                                    {motivosPorTipoDeMovimiento.map(motivo_por_tipo_de_movimiento => (
                                        <SearchSelectItem key={motivo_por_tipo_de_movimiento.id} value={motivo_por_tipo_de_movimiento.id}>{motivo_por_tipo_de_movimiento.str_descripcion}</SearchSelectItem>
                                    ))}
                                </SearchSelect>
                            </div>

                          

                            <div className={`mb-4 ${isDepositoOrigenVisible ? 'visible' : 'invisible'}`}>
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

                            <div className={`mb-4 ${isDepositoDestinoVisible ? 'visible' : 'invisible'}`}>
                                <label htmlFor="fk_deposito_destino" className="block text-sm font-medium text-gray-700">Depósito Destino</label>
                                <SearchSelect id="fk_deposito_destino" className='mt-2' placeholder='Depósito' value={fk_deposito_destino} onValueChange={(value) => setFk_deposito_destino(parseInt(value))}>
                                    {opcionesFiltradas.map(depositoDestino => (

                                        <SearchSelectItem key={depositoDestino.id} value={depositoDestino.id}>{depositoDestino.str_nombre}</SearchSelectItem>
                                    ))}
                                </SearchSelect>
                            </div>

                            {isTransferencia && (
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
                                            value={datosVehiculo}
                                            onChange={(e) => setDatosVehiculo(e.target.value)}
                                            className="mt-1 block w-full rounded-md border-gray-300 shadow-sm sm:text-sm"
                                            required={isTransferencia}
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
                                            required={isTransferencia}
                                        />
                                    </div>
                                </>
                            )}

                        </div>

                        <div className="flex justify-end mt-6 gap-2">
                            <Button type="button" variant="secondary" color="blue" onClick={() => navigate.push('/movimientos')}>Cancelar</Button>
                            <Button
                                disabled={detallesMovimientos.length === 0}
                                type="submit" variant="primary" color="blue"
                            >
                                Guardar Movimiento
                            </Button>
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
