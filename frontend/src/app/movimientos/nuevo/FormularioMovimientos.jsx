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
import TimbradosConfig from "@/controladores/TimbradosConfig";
import NotasDeRemisionConfig from "@/controladores/NotasDeRemisionConfig";
import FerreteriasConfig from "@/controladores/FerreteriasConfig";

let detalleIdCounter = 0;

export default function FormularioMovimientos() {
    const navigate = useRouter();
    const Toast = Swal.mixin({
        toast: true,
        position: 'bottom-end',
        iconColor: 'white',
        customClass: {
            popup: 'colored-toast',
        },
        showConfirmButton: false,
        timer: 3000,
        timerProgressBar: true,
    })




    //Encabezado de Movimientos
    const [responsable, setResponsable] = useState('');
    const [fecha, setFecha] = useState('');
    //const [fk_motivoId, setFk_MotivoId] = useState(0);
    const [motivos, setMotivos] = useState([]);
    const [tiposDeMovimientos, setTiposDeMovimientos] = useState([]);
    const [motivosPorTipoDeMovimiento, setMotivosPorTipoDeMovimiento] = useState([]);
    const [fk_motivo_por_tipo_de_movimiento, setFk_motivo_por_tipo_de_movimiento] = useState(null);
    const [esCompra, setEsCompra] = useState(false);
    const [esEgreso, setEsEgreso] = useState(false);
    const [esIngreso, setEsIngreso] = useState(false);
    const [ferreteria, setFerreteria] = useState(null);

    const [alertaCantidad, setAlertaCantidad] = useState('');

    const [fk_tipo_de_movimiento, setFk_tipo_de_movimiento] = useState(0);




    //Este es el arreglo de detalles que se le va a pasar a detalles de movimientos
    const [detallesMovimientos, setDetallesMovimientos] = useState([]);
    const [depositoOrigen, setDepositoOrigen] = useState(0);
    const [iva, setIva] = useState(10);

    //Manejo de listas de productos y Depósitos de Origen y Destino para los movimientos
    const [fk_producto, setFk_producto] = useState(0);
    const [arregloProductos, setArreglo_productos] = useState([]);
    const [productosFiltrados, setProductosFiltrados] = useState([]);
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
                const [respuestaDepositos, respuestaProductos, respuestaFerreteria, respuestaSiguienteNotaDeRemision] = await Promise.all([
                    DepositosConfig.getDepositos(),
                    ProductosConfig.getProductos(),
                    FerreteriasConfig.getFerreteriaId(1),
                    NotasDeRemisionConfig.getNotaDeRemisionSiguiente()
                ]);

                setDepositos(respuestaDepositos.data);
                setDepositosDestinos(respuestaDepositos.data);
                setArreglo_productos(respuestaProductos.data);
                setFerreteria(respuestaFerreteria.data);
                setNumeroNotaRemision(respuestaSiguienteNotaDeRemision.data);

                //console.log(respuestaTiposDeMovimientos);
            } catch (error) {
                console.error('Error al obtener lista de depositos: ', error);
            }
        }
        extraccionDepositos();
    }, []);

    useEffect(() => {
        // Filtra los productos en base al depósito de origen seleccionado
        if (fk_deposito_origen !== null && !esIngreso) {
            const productosEnDeposito = arregloProductos.filter(producto => producto.depositoId === fk_deposito_origen);
            setProductosFiltrados(productosEnDeposito);
        } else {
            const productosEnDepositoDestino = arregloProductos.filter(producto => producto.depositoId === 1);
            setProductosFiltrados(productosEnDepositoDestino);
        }
    }, [fk_deposito_origen, arregloProductos, esIngreso]);

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
                const respuestaTimbrado = await TimbradosConfig.getTimbradoActivo();
                const respuestaTiposDeMovimientos = await TiposDeMovimientosConfig.getTiposDeMovimiento();
                const respuestaMotivos = await MotivosConfig.getMotivos();
                const respuestaMotivosPorTipoDeMovimiento = await MotivosPorTipoDeMovimientoConfig.getMotivosPorTipoDeMovimiento();
                setTiposDeMovimientos(respuestaTiposDeMovimientos.data);
                setMotivos(respuestaMotivos.data);
                setMotivosPorTipoDeMovimiento(respuestaMotivosPorTipoDeMovimiento.data);
                setTimbradoRemision(respuestaTimbrado.data);
                console.log('Timbrado Activo', respuestaTimbrado.data);
                console.log(respuestaMotivosPorTipoDeMovimiento.data);
                console.log(respuestaMotivos.data);
                console.log(respuestaTiposDeMovimientos.data);
            } catch (error) {
                console.log('Error al obtener Motivos por tipo de Movimiento', error);
            }
        }
        extraccionDeMotivosPorTipoDeMovimiento();
    }, []);


    useEffect(() => {
        setDetallesMovimientos([]);
        setDetalleTemp({ idProducto: 0, descripcion: '', cantidad: 1, precio: 0, total: 0 });
    }, [fk_motivo_por_tipo_de_movimiento]);


    const [motivo, setMotivo] = useState('');

    //Estado para detalle temporal
    const [detalleTemp, setDetalleTemp] = useState({ idProducto: 0, descripcion: '', cantidad: 1, precio: 0, total: 0 });

    useEffect(() => {
        // Actualiza el precio en detalleTemp cuando se selecciona un producto
        if (fk_producto !== 0) {
            const productoSeleccionado = arregloProductos.find(producto => producto.id === +fk_producto);
            if (productoSeleccionado) {
                setDetalleTemp(prevDetalle => ({
                    ...prevDetalle,
                    idProducto: productoSeleccionado.id,
                    descripcion: productoSeleccionado.str_nombre,
                    precio: productoSeleccionado.dec_costo,
                    total: prevDetalle.cantidad * productoSeleccionado.dec_costo
                }));
            }
        }
    }, [fk_producto, arregloProductos]);

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

        if ((esEgreso || isTransferencia) && productoSeleccionado.int_cantidad_actual < detalleTemp.cantidad) {
            Toast.fire({
                icon: 'warning',
                title: `No se puede dar salida a más de ${productoSeleccionado.int_cantidad_actual} ${productoSeleccionado.str_nombre} del depósito origen.`,
            });
            return;
        }

        //console.log(detallesMovimientos);
        const nuevoId = detallesMovimientos.length;
        const precioProducto = esCompra ? detalleTemp.precio : productoSeleccionado.dec_costo;
        setDetallesMovimientos([...detallesMovimientos, { ...detalleTemp, idDetalle: nuevoId, idProducto: productoSeleccionado.id, descripcion: productoSeleccionado.str_nombre, cantidad: detalleTemp.cantidad, precio: precioProducto, total: detalleTemp.total }]);
        setDetalleTemp({ idProducto: 0, descripcion: '', cantidad: 1, precio: 0, total: 0 });
        setFk_producto(0); // Reset product selection after adding
        setAlertaCantidad('');
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
    const [timbradoRemision, setTimbradoRemision] = useState(null);
    const [depositoDestino, setDepositoDestino] = useState('');
    const [datosVehiculo, setDatosVehiculo] = useState({ modeloVehiculo: 'Modelo: HINO 500 1925 -', chapa: ' CHAPA: ABCD123' });
    const [conductor, setConductor] = useState({ str_nombre_conductor: 'Jose Luis Arzamendia Patiño', str_documento_conductor: ' C.I.No.: 7456123' });







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

    {/*Aqui es donde guardo el formulario para enviar al backend y se guarde todo.*/ }
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
            const fk_deposito_origen_API = fk_deposito_origen ? fk_deposito_origen : fk_deposito_destino;
            const fk_deposito_destino_API = fk_deposito_destino ? fk_deposito_destino : fk_deposito_origen;
            
            const movimientoCreado = await MovimientosConfig.postMovimiento(
                fk_motivo_por_tipo_de_movimiento, 
                fk_deposito_origen_API, 
                fk_deposito_destino_API, 
                movimientoActual
            );
    
            Swal.fire('Guardado', 'El movimiento fue creado exitosamente.', 'success');
    
            /*if (isTransferencia) {
                const movimientoId = movimientoCreado.id;
                //const notaDeRemisionSiguiente = await NotasDeRemisionConfig.getNotaDeRemisionSiguiente();
                const notaDeRemision = {
                    "timbradoId": timbradoRemision.id,
                    "str_numero": numeroNotaRemision,
                    "date_fecha_de_expedicion": fecha,
                    "date_fecha_de_vencimiento": timbradoRemision.date_fin_vigencia,
                    "movimientoId": movimientoId,
                    "empresaNombre": ferreteria.str_nombre,
                    "empresaDireccion": "Encarnacion",
                    "empresaTelefono": ferreteria.str_telefono,
                    "empresaSucursal": "Encarnacion",
                    "empresaActividad": "Construccion",
                    "ruc": ferreteria.ruc,
                    "destinatarioNombre": ferreteria.str_nombre,
                    "destinatarioDocumento": ferreteria.str_ruc,
                    "puntoPartida": fk_deposito_origen,
                    "puntoLlegada": fk_deposito_destino,
                    "trasladoFechaInicio": fecha,
                    "trasladoFechaFin": fecha,
                    "trasladoVehiculo": datosVehiculo.modeloVehiculo,
                    "trasladoRua": datosVehiculo.chapa,
                    "transportistaNombre": ferreteria.str_nombre,
                    "transportistaRuc": ferreteria.str_ruc,
                    "conductorNombre": conductor.str_nombre_conductor,
                    "conductorDocumento": conductor.str_documento_conductor,
                    "conductorDireccion": "Encarnacion",
                    "motivo": "Transferencia",
                    "motivoDescripcion": "Transferencia entre depositos",
                    "comprobanteVenta": numeroNotaRemision
                }
    
                console.log('Nota de Remision enviada', notaDeRemision);
                await NotasDeRemisionConfig.postNotaDeRemision(notaDeRemision);
            }*/
    
            navigate.push('/movimientos');
    
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
        setEsCompra(tipoMovimientoId === 1);
        setEsEgreso(tipoMovimientoId === 2);
        setEsIngreso(tipoMovimientoId === 1);

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
                                            value={timbradoRemision.str_timbrado}
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
                            <SearchSelect id="fk_producto" className='mt-2' placeholder='Producto' value={fk_producto} onValueChange={(value) => setFk_producto(parseInt(value))}>
                                {productosFiltrados.map(producto => (
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
                                disabled={!esCompra}
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
