"use client";
import React, { useEffect, useState } from "react";
import {
  Button,
  Dialog,
  DialogPanel,
  TextInput,
  SearchSelect,
  SearchSelectItem,
} from "@tremor/react";
import { RiCloseLine } from "@remixicon/react";
import DepositosConfig from "../../../controladores/DepositosConfig";
import FerreteriasConfig from "../../../controladores/FerreteriasConfig";
import { useRouter } from "next/navigation";
import EncargadosConfig from "../../../controladores/EncargadosConfig.jsx";
import FormularioEncargados from "@/components/formularioEncargados";
import Swal from "sweetalert2";

export default function FormularioDepositos() {
  const navigate = useRouter();
  const [encargados, setEncargados] = useState([]);
  const [str_nombre, setStr_nombre] = useState("");
  const [str_direccion, setStr_direccion] = useState("");
  const [str_telefono, setStr_telefono] = useState("");
  const [fk_encargado, setFk_encargado] = useState(0);

  const [fk_ferreteria, setFk_ferreteria] = useState(0);
  const [ferreterias, setFerreterias] = useState([]);
  const [showCrearEncargado, setShowCrearEncargado] = useState(false);
  

  const handleCrearEncargado = () => {
    setShowCrearEncargado(true);
  };

  const handleCloseModal = () => {
    setShowCrearEncargado(false);
  };

  const handleEncargadoCreado = async (fk_encargado) => {
    //console.log("yes",marcaId);
    await listaUsuarios();
    setFk_encargado(fk_encargado);
  };

  const listaUsuarios = async () => {
    try {
      const listaUsuarios = await EncargadosConfig.getUsuarios();
      const usuariosfiltrados = listaUsuarios.data.filter(
        (encargado) => encargado.role === "Encargado"
      );

      setEncargados(usuariosfiltrados);
    } catch (error) {
      console.error("Error al obtener lista de encargados: ", error);
    }
  };

  useEffect(() => {
    listaUsuarios();
  }, []);

  useEffect(() => {
    const extraccionFerreterias = async () => {
      try {
        const respuesta = await FerreteriasConfig.getFerreteria();
        setFerreterias(respuesta.data);
      } catch (error) {
        console.error("Error al obtener lista de proveedores: ", error);
      }
    };
    extraccionFerreterias();
  }, []);

  const handleSubmit = async (event) => {
    event.preventDefault();
    
    try {
      const deposito = {
        str_nombre: str_nombre,
        str_direccion: str_direccion,
        str_telefono: str_telefono,
        encargadoUsername: fk_encargado,
      };

      console.log(deposito);

      const response = await DepositosConfig.postDeposito(1, deposito);

      
      Swal.fire(
        'Creado!',
        'El depósito ha sido creado.',
        'success'
      );
      navigate.push("/depositos");
    } catch (error) {
      Swal.fire(
        'Error!',
        'Hubo un problema al crear el depósito.',
        'error'
      );
      console.error("Error al enviar los datos del formulario: ", error);
    }
  };

  return (
    <>
      <form onSubmit={handleSubmit}>
        <div className="col">
          <h5 className="font-semibold text-tremor-content-strong dark:text-dark-tremor-content-strong mb-2">
            Datos del Depósito
          </h5>
          <div className="m-5">
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
              className="my-2 w-1/2 "
              value={str_nombre}
              onChange={(e) => setStr_nombre(e.target.value)}
              required
            />
          </div>

          <div className="m-5">
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
              className="my-2 w-1/2 "
              value={str_direccion}
              onChange={(e) => setStr_direccion(e.target.value)}
              required
            />
          </div>

          <div className="m-5">
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
              className="my-2 w-1/2 "
              value={str_telefono}
              onChange={(e) => setStr_telefono(e.target.value)}
              required
            />
          </div>

          <div className="m-5">
            <label
              htmlFor="str_encargadoId"
              className="text-tremor-default font-medium text-tremor-content-strong dark:text-dark-tremor-content-strong"
            >
              Encargado del Depósito
              <span className="text-red-500">*</span>
            </label>
            <SearchSelect
              id="fk_encargado"
              placeholder="Seleccionar Encargado"
              className="my-2 w-1/2 "
              value={fk_encargado}
              onValueChange={(value) => {
                value === "crear" ? false : setFk_encargado(value);
              }}
            >
              <SearchSelectItem value="crear" onClick={handleCrearEncargado} style={{ color: 'rgb(59, 130, 246)' }} >
                Crear Nuevo Encargado
              </SearchSelectItem>
              {encargados.length > 0 &&
                encargados.map((encargado) => (
                  <SearchSelectItem
                    key={encargado.userName}
                    value={encargado.userName}
                  >
                    {encargado.userName}
                  </SearchSelectItem>
                ))}
            </SearchSelect>
          </div>
        </div>
        
        <div className="flex items-center justify-end space-x-4">
          <Button
            variant="secondary"
            color="blue"
            type="button"
            onClick={() => navigate.push("/depositos")}
          >
            Cancelar
          </Button>
          <Button variant="primary" type="submit" color="blue">
            Guardar
          </Button>
        </div>

        
      </form>

      <form>
        <Dialog
          open={showCrearEncargado}
          onClose={handleCloseModal}
          static={true}
          className="z-[100]"
        >
          <DialogPanel className="sm:max-w-md">
            <div className="w-full text-right">
              <button>
                <RiCloseLine
                  className="h-5 w-5 shrink-0"
                  aria-hidden={true}
                  onClick={handleCloseModal}
                />
              </button>
            </div>

            <FormularioEncargados
              closeDialog={handleCloseModal}
              saveAction={handleEncargadoCreado}
            />
          </DialogPanel>
        </Dialog>
      </form>
    </>
  );
}
