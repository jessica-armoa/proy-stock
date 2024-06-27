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
import encargados from "../../../controladores/encargados.json";

export default function FormularioDepositos() {
  const navigate = useRouter();

  const [isOpen, setIsOpen] = useState(true);
  const [encargados, setEncargados] = useState([]);
  const [str_nombre, setStr_nombre] = useState("");
  const [str_direccion, setStr_direccion] = useState("");
  const [str_telefono, setStr_telefono] = useState("");
  const [fk_encargado, setFk_encargado] = useState("");
  const [encargadoUsername, setEncargadoUsername] = useState("");
  const [nombreEncargado, setNombreEncargado] = useState("");
  const [encargadoEmail, setEncargadoEmail] = useState("");
  const [encargadoPassword, setEncargadoPassword] = useState("");
  const [repeatPassword, setRepeatPassword] = useState("");

  const [isPasswordMatch, setIsPasswordMatch] = useState(true);
  
  const [fk_ferreteria, setFk_ferreteria] = useState(0);
  const [ferreterias, setFerreterias] = useState([]);
  const [showCrearEncargado, setShowCrearEncargado] = useState(false);
  const handleCrearEncargado = () => {
    setShowCrearEncargado(true);
  };

  const handleCloseModal = () => {
    setShowCrearEncargado(false);
  };

  const handleEncargadoCreado = async(fk_encargado) => {
    //console.log("yes",marcaId);
    await listaEncargados();
    setFk_encargado(fk_encargado);
  };

  const getListaEncargados = async () => {
    try {
      const listaEncargados = await EncargadosConfig.getEmpleados();
      setEncargados(listaEncargados.data);
    } catch (error) {
      console.error("Error al obtener lista de encargados: ", error);
    }
  };

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
        str_nombre: str_nombre,
        str_direccion: str_direccion,
        str_telefono: str_telefono,
        encargadoUsername: 'CosmeFulanito',
      };

      console.log(deposito);

      const response = await DepositosConfig.postDeposito(1, deposito);

      setStr_nombre("");
      setStr_direccion("");
      setStr_telefono("");
      setEncargadoUsername("");
      setEncargadoEmail("");
      setEncargadoPassword("");
      setRepeatPassword("");
      setFk_ferreteria(0);

      navigate.push("/depositos");
    } catch (error) {
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
                value === "crear" ? false : setFK_encargado(parseInt(value));
              }}
            >
              <SearchSelectItem value="crear" onClick={handleCrearEncargado}>
                <Button type="button" variant="light" color="blue">
                  Crear Nuevo Encargado
                </Button>
              </SearchSelectItem>
              {encargados.lenght > 0 &&
                encargados.map((encargado) => (
                  <SearchSelectItem key={encargado.id} value={encargado.id}>
                    {encargado.str_nombre}
                  </SearchSelectItem>
                ))}
            </SearchSelect>
          </div>
        </div>
        <div className="col-span-full flex justify-end space-x-4 mt-4">
              <Button
                variant="secondary"
                color="blue"
                type="button"
                //onClick={navigate.push("/depositos")}
              >
                Cancelar
              </Button>
              <Button
                variant="primary"
                color="blue"
                type="submit"
                
              >
                Guardar
              </Button>
            </div>
      </form>

                {/*
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
                    <FormularioMarcas type={'modal'} closeDialog={handleCloseModal} saveAction={handleMarcaCreada}/>
                  </DialogPanel>
                </Dialog>
                */}


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
            <div className="col-span-full">
              <h5 className="font-semibold text-tremor-content-strong dark:text-dark-tremor-content-strong mt-6 mb-2">
                Crear nuevo Encargado
              </h5>
            </div>
            <div className="col-span-full sm:col-span-3 m-5">
              <label
                htmlFor="nombreEncargado"
                className="text-tremor-default font-medium text-tremor-content-strong dark:text-dark-tremor-content-strong"
              >
                Nombre y apellido del Encargado
                <span className="text-red-500">*</span>
              </label>
              <TextInput
                type="text"
                id="nombreEncargado"
                name="nombreEncargado"
                autoComplete="nombreEncargado"
                placeholder="Nombre y apellido del encargado"
                className="mt-2"
                value={nombreEncargado}
                onChange={(e) => setNombreEncargado(e.target.value)}
                required
              />
            </div>
            <div className="col-span-full sm:col-span-3 m-5">
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
            <div className="col-span-3 m-5">
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
            <div className="col-span-3 m-5">
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
            <div className="col-span-3 m-5">
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
            
            <div className="col-span-full flex justify-end space-x-4 mt-4">
              <Button
                variant="secondary"
                type="button"
                color="blue"
                onClick={handleCloseModal}
              >
                Cancelar
              </Button>
              <Button
                variant="primary"
                color="blue"
                type="submit"
                disabled={!isPasswordMatch}
                onMouseOver={(e) => {
                  if (!isPasswordMatch) {
                    e.target.title = "Las contraseñas no coinciden.";
                  } else {
                    e.target.title = "";
                  }
                }}
                onClick={handleEncargadoCreado}
              >
                Guardar
              </Button>
            </div>
          </DialogPanel>
        </Dialog>
      </form>
    </>
  );
}
