"use client";
import React, { useEffect, useState } from "react";
//import { useRouter } from "next/navigation";
import { Button, TextInput } from "@tremor/react";
import EncargadosConfig from "../controladores/EncargadosConfig.jsx";

export default function FormularioEncargados({
  closeDialog = false,
  saveAction = false,
}) {
  //const router = useRouter();

  const [userName, setUserName] = useState("");
  const [nombreEncargado, setNombreEncargado] = useState("");
  const [email, setEmail] = useState("");

  const [password, setPassword] = useState("");
  const [repeatPassword, setRepeatPassword] = useState("");

  const [isPasswordMatch, setIsPasswordMatch] = useState(true);

  const handlePasswordChange = (e) => {
    setPassword(e.target.value);
    setIsPasswordMatch(e.target.value === repeatPassword);
  };

  const handleRepeatPasswordChange = (e) => {
    setRepeatPassword(e.target.value);
    setIsPasswordMatch(e.target.value === password);
  };

  const handleSubmit = async (event) => {
    event.preventDefault();
    try {

      const encargado = { userName, email, password };
      const response = await EncargadosConfig.postEncargado(encargado);
      //
      //const newID = response.data.id ?? "crear";
      saveAction(userName);
      closeDialog();
    } catch (error) {
      console.error("Error al enviar los datos del formulario: ", error);
    }
  };

  const handleCancelClick = () => {
    closeDialog();
  };

  return (
    <form onSubmit={handleSubmit}>
        <div>
      <div className="col-span-full">
        <h5 className="font-semibold text-tremor-content-strong dark:text-dark-tremor-content-strong mb-2">
          Crear nuevo Encargado
        </h5>
      </div>
      {/*<div className="col-span-full sm:col-span-3 m-5">
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
      </div>*/}
      <div className="col-span-full sm:col-span-3 m-5">
        <label
          htmlFor="userName"
          className="text-tremor-default font-medium text-tremor-content-strong dark:text-dark-tremor-content-strong"
        >
          Nombre de Usuario del Encargado
          <span className="text-red-500">*</span>
        </label>
        <TextInput
          type="text"
          id="userName"
          name="userName"
          autoComplete="userName"
          placeholder="Nombre de usuario del encargado"
          className="mt-2"
          value={userName}
          onChange={(e) => setUserName(e.target.value)}
          required
        />
      </div>
      <div className="col-span-3 m-5">
        <label
          htmlFor="email"
          className="text-tremor-default font-medium text-tremor-content-strong dark:text-dark-tremor-content-strong"
        >
          Email del Encargado
          <span className="text-red-500">*</span>
        </label>
        <TextInput
          type="email"
          id="email"
          name="email"
          autoComplete="email"
          placeholder="Email del encargado"
          className="mt-2"
          value={email}
          onChange={(e) => setEmail(e.target.value)}
          required
        />
      </div>
      <div className="col-span-3 m-5">
        <label
          htmlFor="password"
          className="text-tremor-default font-medium text-tremor-content-strong dark:text-dark-tremor-content-strong"
        >
          Contraseña del Encargado
          <span className="text-red-500">*</span>
        </label>
        <TextInput
          type="password"
          id="password"
          name="password"
          autoComplete="password"
          placeholder="Contraseña del encargado"
          className="mt-2"
          value={password}
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
      </div>
      <div className="text-right">
        <Button
          className="mt-8 mr-5"
          variant="secondary"
          type="button"
          color="blue"
          onClick={handleCancelClick}
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
        >
          Guardar
        </Button>
      </div>
    </form>
  );
}
