"use client";
import React, { useEffect, useState } from "react";
import { useRouter } from "next/navigation";
import { Button, TextInput, SearchSelect, SearchSelectItem } from "@tremor/react";
import MarcasConfig from "../../../controladores/MarcasConfig";
import ProveedoresConfig from "../../../controladores/ProveedoresConfig";

export default function FormularioMarcas({
  type = "form",
  closeDialog = false,
  saveAction = false,
}) {
  const router = useRouter();

  const [str_nombre, setStr_nombre] = useState("");
  const [proveedorId, setProveedorId] = useState(0);
  const [proveedores, setProveedores] = useState([]);


  useEffect(() => {
    const extraccionProveedores = async () => {
      try {
        const respuesta = await ProveedoresConfig.getProveedor();
        setProveedores(respuesta.data);
      } catch (error) {
        console.error("Error al obtener lista de proveedores: ", error);
      }
    };
    extraccionProveedores();
  }, []);

  const handleSubmit = async (event) => {
    event.preventDefault();
    try {
      const marca = { str_nombre };
      const response = await MarcasConfig.postMarca(proveedorId, marca);
      setStr_nombre("");
      setProveedorId(0);
      if (type === "form") {
        router.push("/marcas");
      } else {
        const newID = response.data.id ?? "crear";
        saveAction(newID);
        closeDialog();
      }
    } catch (error) {
      console.error("Error al enviar los datos del formulario: ", error);
    }
  };

  const handleCancelClick = () => {
    if (type === "modal") {
      closeDialog();
    } else {
      setStr_nombre("");
      setProveedorId(0);
    }
  };

  return (
    <form onSubmit={handleSubmit}>
      <h4 className="font-semibold text-tremor-content-strong dark:text-dark-tremor-content-strong">
        Nueva Marca
      </h4>

      <div className="grid grid-cols-1 gap-x-4 gap-y-6 sm:grid-cols-6">
        <div className="col-span-full sm:col-span-3">
          <label
            htmlFor="str_nombre"
            className="text-tremor-default font-medium text-tremor-content-strong dark:text-dark-tremor-content-strong"
          >
            Nombre de la Marca
            <span className="text-red-500">*</span>
          </label>
          <TextInput
            type="text"
            id="str_nombre"
            name="str_nombre"
            autoComplete="str_nombre"
            placeholder="Nueva Marca"
            className="mt-2"
            value={str_nombre}
            onChange={(e) => setStr_nombre(e.target.value)}
            required
          />
        </div>

        <div className="col-span-full sm:col-span-3">
          <label
            htmlFor="proveedorId"
            className="text-tremor-default font-medium text-tremor-content-strong dark:text-dark-tremor-content-strong"
          >
            Proveedor
            <span className="text-red-500">*</span>
          </label>

          <SearchSelect
            id="proveedorId"
            className="mt-2"
            placeholder="Proveedor"
            value={proveedorId}
            onValueChange={(value) => setProveedorId(parseInt(value))}
          >
            {proveedores.map((proveedor) => (
              <SearchSelectItem key={proveedor.id} value={proveedor.id}>
                {proveedor.str_nombre}
              </SearchSelectItem>
            ))}
          </SearchSelect>
        </div>

        <div className="col-span-full flex justify-center space-x-4">
          <Button
            className="mt-8"
            variant="secondary"
            type="button"
            onClick={handleCancelClick}
          >
            Cancelar
          </Button>
          <Button className="mt-8" variant="primary" type="submit">
            Guardar
          </Button>
        </div>
      </div>
    </form>
  );
}
