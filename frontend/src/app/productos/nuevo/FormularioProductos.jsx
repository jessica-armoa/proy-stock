"use client";
import React, { useEffect, useState } from "react";
import {
  Button,
  NumberInput,
  TextInput,
  SearchSelect,
  SearchSelectItem,
  Dialog,
  DialogPanel,
} from "@tremor/react";
import ProveedoresConfig from "../../../controladores/ProveedoresConfig";
import MarcasConfig from "../../../controladores/MarcasConfig";
import DepositosConfig from "../../../controladores/DepositosConfig";
import ProductosConfig from "../../../controladores/ProductosConfig";
import { useRouter } from "next/navigation";
import FormularioProveedores from "@/app/proveedores/nuevo/FormularioProveedores";
import FormularioMarcas from "@/app/marcas/nuevo/FormularioMarcas";
import { RiCloseLine } from "@remixicon/react";
import Swal from "sweetalert2";
import { black } from "tailwindcss/colors";

export default function FormularioProductos() {
  //crear marca y proveedores modales
  const [showCrearMarca, setShowCrearMarca] = useState(false);
  const [showCrearProveedor, setShowCrearProveedor] = useState(false);
  const [isOpen, setIsOpen] = useState(true);

  const handleCrearMarca = () => {
    setShowCrearMarca(true);
  };

  const handleCrearProveedor = () => {
    setShowCrearProveedor(true);
  };

  const handleCloseModal = () => {
    setShowCrearMarca(false);
    setShowCrearProveedor(false);
  };

  const handleMarcaCreada = async (marcaId) => {
    //console.log("yes",marcaId);
    await extraccionMarcas();
    setFk_marca(marcaId);
  };

  const handleProveedorCreado = async (proveedorId) => {
    await extraccionProveedores();
    setFk_proveedor(proveedorId);
  };

  // Definimos el estado para cada campo del formulario
  const navigate = useRouter();
  const [str_imagen, setStr_imagen] = useState("");
  const [str_nombre, setStr_nombre] = useState("");
  const [str_descripcion, setStr_descripcion] = useState("");
  //const [fk_marca, setFk_marca] = useState('');
  //const [fk_categoria, setFk_categoria] = useState(''); // no se tiene categoria en el api
  //const [fk_proveedor, setFk_proveedor] = useState('');
  const [int_cantidad_actual, setInt_cantidad_actual] = useState(0);
  const [int_cantidad_minima, setInt_cantidad_minima] = useState(0);
  const [dc_costo_PPP, setDc_costo_PPP] = useState(0);
  const [int_iva, setInt_iva] = useState(10);
  const [dc_precio_mayorista, setDc_precio_mayorista] = useState(0);
  const [dc_precio_minorista, setDc_precio_minorista] = useState(0);
  //const [fk_deposito, setFk_deposito] = useState(0);

  const [fk_proveedor, setFk_proveedor] = useState(0);
  const [proveedores, setProveedores] = useState([]);

  const extraccionProveedores = async () => {
    try {
      const respuestaProveedores = await ProveedoresConfig.getProveedor();
      setProveedores(respuestaProveedores.data);
    } catch (error) {
      console.error("Error al obtener lista de proveedores: ", error);
    }
  };

  useEffect(() => {
    extraccionProveedores();
  }, []);

  const [fk_marca, setFk_marca] = useState(0);
  const [marcas, setMarcas] = useState([]);

  const extraccionMarcas = async () => {
    try {
      const respuestaMarcas = await MarcasConfig.getMarca();
      setMarcas(respuestaMarcas.data);
    } catch (error) {
      console.error("Error al obtener lista de marcas: ", error);
    }
  };

  useEffect(() => {
    extraccionMarcas();
  }, []);

  const [fk_deposito, setFk_deposito] = useState(1);
  const [depositos, setDepositos] = useState([]);
  useEffect(() => {
    const extraccionDepositos = async () => {
      try {
        const respuestaDepositos = await DepositosConfig.getDepositos();
        setDepositos(respuestaDepositos.data);
        //console.log("respuesta", respuestaDepositos.data);
      } catch (error) {
        console.error("Error al obtener lista de depositos: ", error);
      }
    };
    extraccionDepositos();
  }, []);

  // Función para manejar el envío del formulario
  //const router = useRouter();
  const handleSubmit = async (event) => {
    event.preventDefault();
    try {
      // Aquí podrías realizar alguna acción con los datos del formulario, como enviarlos a un servidor
      console.log({
        str_imagen,
        str_nombre,
        str_descripcion,
        fk_marca,
        //fk_categoria,
        fk_proveedor,
        int_cantidad_actual,
        int_cantidad_minima,
        dc_costo_PPP,
        int_iva,
        dc_precio_mayorista,
        dc_precio_minorista,
      });
      /*
      {
        "str_ruta_imagen": "string",
        "str_nombre": "string",
        "str_descripcion": "string",
        "int_cantidad_minima": 100,
        "dec_costo": 0,
        "dec_costo_PPP": 0,
        "int_iva": 0,
        "dec_precio_mayorista": 0,
        "dec_precio_minorista": 0,
        "bool_borrado": true
      }*/
      const producto = {
        str_ruta_imagen: str_imagen,
        str_nombre: str_nombre,
        str_descripcion: str_descripcion,
        int_cantidad_minima: int_cantidad_minima,
        //dec_costo_PPP: dc_costo_PPP,
        int_iva: int_iva,
        dec_precio_mayorista: dc_precio_mayorista,
        dec_precio_minorista: dc_precio_minorista,
        bool_borrado: false,
      };

      /*
      "str_ruta_imagen": "string",
  "str_nombre": "string",
  "str_descripcion": "string",
  "int_cantidad_minima": 100,
  "int_iva": 0,
  "dec_precio_mayorista": 0,
  "dec_precio_minorista": 0
      */
      const productoAgregado = await ProductosConfig.postProducto(
        1,
        fk_proveedor,
        fk_marca,
        producto,
        console.log(producto)
      ).then(() => {
        Swal.fire(
          "Guardado",
          "El producto fue creado exitosamente.",
          "success"
        );
      });
      // También puedes reiniciar los valores de los campos del formulario
      setStr_imagen("");
      setStr_nombre("");
      setStr_descripcion("");
      setFk_marca(0);
      //setFk_categoria('');
      setFk_proveedor(0);
      setInt_cantidad_actual(0);
      setInt_cantidad_minima(0);
      setDc_costo_PPP(0);
      setInt_iva(0);
      setDc_precio_mayorista(0);
      setDc_precio_minorista(0);
      setFk_deposito(0);
      navigate.push("/productos");
    } catch (error) {
      console.error("Error al enviar los datos del formulario: ", error);
      Swal.fire("Error", "Hubo un problema al guardar el producto", "error");
    }
  };

  // Para guardar la imagen
  const handleImageChange = (event) => {
    const file = event.target.files[0];
    if (file) {
      const reader = new FileReader();
      reader.onloadend = () => {
        setStr_imagen(reader.result); // Guardar la imagen como una URL de datos
      };
      reader.readAsDataURL(file);
    }
  };

  return (
    <>
      <form onSubmit={handleSubmit}>
        <div className="space-y-4">
          <div className="flex">
            <div className="w-1/4">
              <div className="flex">
                <div>
                  <input
                    id="file-upload"
                    type="file"
                    style={{ display: "none" }}
                    onChange={handleImageChange}
                  />
                  <div
                    style={{
                      border: "1px solid #ccc",
                      padding: "5px",
                      marginBottom: "15px",
                      borderRadius: "4px",
                      minWidth: "300px",
                      maxWidth: "500px",
                      minHeight: "300px",
                      maxHeight: "500px",
                      display: "flex",
                      alignItems: "center",
                      justifyContent: "center",
                      backgroundSize: "cover"
                    }}
                  >
                    {str_imagen ? (
                      <img
                        src={str_imagen}
                        alt="Vista previa"
                      />
                    ) : (
                      <div>Selecciona una imagen</div>
                    )}
                  </div>
                  <label
                    htmlFor="file-upload"
                    className="button text-blue-500 ml-2"
                    style={{ cursor: "pointer" }}
                  >
                    Elegir imagen<span className="text-red-500 ">*</span>
                  </label>
                  
                </div>
              </div>
            </div>
<div className="ml-5 w-1/2">
            <div className=" pl-4 space-y-4">
              <div className="mx-auto ">
                <label
                  htmlFor="str_nombre"
                  className="text-tremor-default font-medium text-tremor-content-strong dark:text-dark-tremor-content-strong"
                >
                  Nombre
                  <span className="text-red-500">*</span>
                </label>
                <TextInput
                  type="text"
                  id="str_nombre"
                  name="str_nombre"
                  autoComplete="str_nombre"
                  placeholder="nombre"
                  className="mt-2"
                  value={str_nombre}
                  onChange={(e) => setStr_nombre(e.target.value)}
                  required
                />
              </div>

              <div className="mx-auto ">
                <label
                  htmlFor="str_descripcion"
                  className="text-tremor-default font-medium text-tremor-content-strong dark:text-dark-tremor-content-strong"
                >
                  Descripción
                  <span className="text-red-500">*</span>
                </label>
                <TextInput
                  type="text"
                  id="str_descripcion"
                  name="str_descripcion"
                  autoComplete="str_descripcion"
                  placeholder="Descripción"
                  className="mt-2"
                  value={str_descripcion}
                  onChange={(e) => setStr_descripcion(e.target.value)}
                  required
                />
              </div>

              <div className="mx-auto ">
                <label
                  htmlFor="fk_marca"
                  className="text-tremor-default font-medium text-tremor-content-strong dark:text-dark-tremor-content-strong"
                >
                  Marca
                  <span className="text-red-500">*</span>
                </label>

                <SearchSelect
                  id="fk_marca"
                  placeholder="Seleccionar Marca"
                  className="mt-2"
                  value={fk_marca}
                  onValueChange={(value) => {
                    value === "crear" ? false : setFk_marca(parseInt(value));
                  }}
                >
                  <SearchSelectItem value="crear" onClick={handleCrearMarca}>
                    <Button type="button" variant="light" color="blue">
                      Crear Nueva Marca
                    </Button>
                  </SearchSelectItem>
                  {marcas.map((marca) => (
                    <SearchSelectItem key={marca.id} value={marca.id}>
                      {marca.str_nombre}
                    </SearchSelectItem>
                  ))}
                </SearchSelect>
              </div>

              <div className="mx-auto ">
                <label
                  htmlFor="fk_proveedor"
                  className="text-tremor-default font-medium text-tremor-content-strong dark:text-dark-tremor-content-strong"
                >
                  Proveedor
                  <span className="text-red-500">*</span>
                </label>
                <SearchSelect
                  id="fk_proveedor"
                  value={fk_proveedor}
                  placeholder="Seleccionar Proveedor"
                  className="mt-2"
                  onValueChange={(value) => {
                    value === "crear"
                      ? false
                      : setFk_proveedor(parseInt(value));
                  }}
                >
                  <SearchSelectItem
                    value="crear"
                    onClick={handleCrearProveedor}
                  >
                    <Button type="button" variant="light" color="blue">
                      Crear Nuevo Proveedor
                    </Button>
                  </SearchSelectItem>
                  {proveedores.map((proveedor) => (
                    <SearchSelectItem key={proveedor.id} value={proveedor.id}>
                      {proveedor.str_nombre}
                    </SearchSelectItem>
                  ))}
                </SearchSelect>
              </div>
            </div>
            <div className=" pl-4 space-y-4">
              <div className="mx-auto ">
                <label
                  htmlFor="int_cantidad_minima"
                  className="text-tremor-default font-medium text-tremor-content-strong dark:text-dark-tremor-content-strong"
                >
                  Cantidad Mínima
                  <span className="text-red-500">*</span>
                </label>
                <NumberInput
                  type="number"
                  id="int_cantidad_minima"
                  name="int_cantidad_minima"
                  autoComplete="int_cantidad_minima"
                  placeholder="Cantidad Minima"
                  className="mt-2"
                  value={int_cantidad_minima}
                  min={0}
                  onChange={(e) => setInt_cantidad_minima(e.target.value)}
                  required
                />
              </div>
              <div className="mx-auto ">
                <label
                  htmlFor="int_iva"
                  className="text-tremor-default font-medium text-tremor-content-strong dark:text-dark-tremor-content-strong"
                >
                  IVA
                  <span className="text-red-500">*</span>
                </label>
                <NumberInput
                  id="int_iva"
                  name="int_iva"
                  autoComplete="int_iva"
                  value={int_iva}
                  min={0}
                  placeholder="IVA %"
                  className="mt-2"
                  onChange={(e) => setInt_iva(e.target.value)}
                  disabled
                  required
                />
              </div>
            </div>
            <div className=" pl-4 space-y-4">
              <div className="mx-auto ">
                <label
                  htmlFor="dc_precio_mayorista"
                  className="text-tremor-default font-medium text-tremor-content-strong dark:text-dark-tremor-content-strong"
                >
                  Precio Mayorista
                  <span className="text-red-500">*</span>
                </label>
                <NumberInput
                  enableStepper={false}
                  id="dc_precio_mayorista"
                  name="dc_precio_mayorista"
                  autoComplete="dc_precio_mayorista"
                  placeholder="Precio Mayorista"
                  className="mt-2"
                  value={dc_precio_mayorista}
                  min={0}
                  onChange={(e) => setDc_precio_mayorista(e.target.value)}
                  required
                />
              </div>
              <div className="mx-auto ">
                <label
                  htmlFor="dc_precio_minorista"
                  className="text-tremor-default font-medium text-tremor-content-strong dark:text-dark-tremor-content-strong"
                >
                  Precio Minorista
                  <span className="text-red-500">*</span>
                </label>
                <NumberInput
                  enableStepper={false}
                  id="dc_precio_minorista"
                  name="dc_precio_minorista"
                  autoComplete="dc_precio_minorista"
                  placeholder="Precio Minorista"
                  className="mt-2"
                  value={dc_precio_minorista}
                  min={0}
                  onChange={(e) => setDc_precio_minorista(e.target.value)}
                  required
                />
              </div>
            </div>
          </div>
          </div>
        </div>

        <div className="flex items-center justify-end space-x-4">
          <Button
            variant="secondary"
            color="blue"
            type="button"
            onClick={() => {
              navigate.push("/productos");
            }}
          >
            Cancelar
          </Button>
          <Button variant="primary" type="submit" color="blue">
            Guardar
          </Button>
        </div>
      </form>

      {/*controlar modales*/}

      <Dialog
        open={showCrearMarca}
        onClose={handleCloseModal}
        static={true}
        className="z-[100]"
      >
        <DialogPanel className="sm:max-w-md">
          <button>
            <RiCloseLine
              className="h-5 w-5 shrink-0"
              aria-hidden={true}
              onClick={handleCloseModal}
            />
          </button>

          <FormularioMarcas
            type={"modal"}
            closeDialog={handleCloseModal}
            saveAction={handleMarcaCreada}
          />
        </DialogPanel>
      </Dialog>
      <Dialog
        open={showCrearProveedor}
        onClose={handleCloseModal}
        static={true}
        className="z-[100]"
      >
        <DialogPanel>
          <div className="w-full text-right">
            <button>
              <RiCloseLine
                className="h-5 w-5 shrink-0 text-right"
                aria-hidden={true}
                onClick={handleCloseModal}
              />
            </button>
          </div>

          <FormularioProveedores
            type={"modal"}
            closeDialog={handleCloseModal}
            saveAction={handleProveedorCreado}
            /*isOpen={showCrearProveedor}
            onClose={handleCancelarCreacionProveedor}
            onProveedorCreado={handleProveedorCreado}*/
          />
        </DialogPanel>
      </Dialog>
    </>
  );
}
