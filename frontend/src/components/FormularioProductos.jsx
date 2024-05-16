"use client";
import React, { useState } from 'react';
import { Button, NumberInput, TextInput } from '@tremor/react';
import { useNavigate } from 'react-router-dom';

export default function FormularioProductos() {
  // Definimos el estado para cada campo del formulario
  const [id_producto, setId_producto] = useState('');
  const [str_nombre, setStr_nombre] = useState('');
  const [str_descripcion, setStr_descripcion] = useState('');
  const [fk_marca, setFk_marca] = useState('');
  const [fk_categoria, setFk_categoria] = useState('');
  const [fk_proveedor, setFk_proveedor] = useState('');
  const [int_cantidad_actual, setInt_cantidad_actual] = useState('');
  const [int_cantidad_minima, setInt_cantidad_minima] = useState('');
  const [dc_costo_PPP, setDc_costo_PPP] = useState('');
  const [int_iva, setInt_iva] = useState('');
  const [dc_precio_mayorista, setDc_precio_mayorista] = useState('');
  const [dc_precio_minorista, setDc_precio_minorista] = useState('');
  const [fk_deposito, setFk_deposito] = useState('');


  // Función para manejar el envío del formulario
  const navigate = useNavigate()
  const handleSubmit = (event) => {
    event.preventDefault();
    // Aquí podrías realizar alguna acción con los datos del formulario, como enviarlos a un servidor
    console.log({
      id_producto,
      str_nombre,
      str_descripcion,
      fk_marca,
      fk_categoria,
      fk_proveedor,
      int_cantidad_actual,
      int_cantidad_minima,
      dc_costo_PPP,
      int_iva,
      dc_precio_mayorista,
      dc_precio_minorista,
      fk_deposito
    });
    // También puedes reiniciar los valores de los campos del formulario
    setId_producto('');
    setStr_nombre('');
    setStr_descripcion('');
    setFk_marca('');
    setFk_categoria('');
    setFk_proveedor('');
    setInt_cantidad_actual('');
    setInt_cantidad_minima('');
    setDc_costo_PPP('');
    setInt_iva('');
    setDc_precio_mayorista('');
    setDc_precio_minorista('');
    setFk_deposito('');
  };

  return (
    <form onSubmit={handleSubmit}>

      <div className="grid grid-cols-1 gap-x-4 gap-y-6 sm:grid-cols-6">
        <div className="col-span-full sm:col-span-3">
          <label
            htmlFor="id_producto"
            className="text-tremor-default font-medium text-tremor-content-strong dark:text-dark-tremor-content-strong"
          >
            Código
            <span className="text-red-500">*</span>
          </label>
          <TextInput
            type="text"
            id="id_producto"
            name="id_producto"
            autoComplete="id_producto"
            placeholder="Codigo"
            className="mt-2"
            value={id_producto}
            onChange={(e) => setId_producto(e.target.value)}
            required
          />
        </div>

        <div className="col-span-full sm:col-span-3">
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

        <div className="col-span-full sm:col-span-3">
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

        <div className="col-span-full sm:col-span-3">
          <label
            htmlFor="fk_marca"
            className="text-tremor-default font-medium text-tremor-content-strong dark:text-dark-tremor-content-strong"
          >
            Marca
            <span className="text-red-500">*</span>
          </label>
          <TextInput
            type="text"
            id="fk_marca"
            name="fk_marca"
            autoComplete="fk_marca"
            placeholder="Marca"
            className="mt-2"
            value={fk_marca}
            onChange={(e) => setFk_marca(e.target.value)}
            required
          />
        </div>

        <div className="col-span-full sm:col-span-3">
          <label
            htmlFor="fk_categoria"
            className="text-tremor-default font-medium text-tremor-content-strong dark:text-dark-tremor-content-strong"
          >
            Categoria
            <span className="text-red-500">*</span>
          </label>
          <TextInput
            type="text"
            id="fk_categoria"
            name="fk_categoria"
            autoComplete="fk_categoria"
            placeholder="Categoria"
            className="mt-2"
            value={fk_categoria}
            onChange={(e) => setFk_categoria(e.target.value)}
            required
          />
        </div>

        <div className="col-span-full sm:col-span-3">
          <label
            htmlFor="fk_proveedor"
            className="text-tremor-default font-medium text-tremor-content-strong dark:text-dark-tremor-content-strong"
          >
            Proveedor
            <span className="text-red-500">*</span>
          </label>
          <TextInput
            type="text"
            id="fk_proveedor"
            name="fk_proveedor"
            autoComplete="fk_proveedor"
            placeholder="Proveedor"
            className="mt-2"
            value={fk_proveedor}
            onChange={(e) => setFk_proveedor(e.target.value)}
            required
          />
        </div>

        <div className="col-span-full sm:col-span-3">
          <label
            htmlFor="int_cantidad_actual"
            className="text-tremor-default font-medium text-tremor-content-strong dark:text-dark-tremor-content-strong"
          >
            Cantidad
            <span className="text-red-500">*</span>
          </label>
          <NumberInput
            type="number"
            id="int_cantidad_actual"
            name="int_cantidad_actual"
            autoComplete="int_cantidad_actual"
            placeholder="Cantidad"
            className="mt-2"
            value={int_cantidad_actual}
            min={0}
            onChange={(e) => setInt_cantidad_actual(e.target.value)}
            required
          />
        </div>

        <div className="col-span-full sm:col-span-3">
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

        <div className="col-span-full sm:col-span-3">
          <label
            htmlFor="dc_costo_PPP"
            className="text-tremor-default font-medium text-tremor-content-strong dark:text-dark-tremor-content-strong"
          >
            Costo
            <span className="text-red-500">*</span>
          </label>
          <NumberInput enableStepper={false}
            id="dc_costo_PPP"
            name="dc_costo_PPP"
            autoComplete="dc_costo_PPP"
            placeholder="Gs."
            className="mt-2"
            value={dc_costo_PPP}
            min={0}
            onChange={(e) => setDc_costo_PPP(e.target.value)}
            required
          />
        </div>

        <div className="col-span-full sm:col-span-3">
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
            required
          />
        </div>

        <div className="col-span-full sm:col-span-3">
          <label
            htmlFor="dc_precio_mayorista"
            className="text-tremor-default font-medium text-tremor-content-strong dark:text-dark-tremor-content-strong"
          >
            Precio Mayorista
            <span className="text-red-500">*</span>
          </label>
          <NumberInput enableStepper={false}
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

        <div className="col-span-full sm:col-span-3">
          <label
            htmlFor="dc_precio_minorista"
            className="text-tremor-default font-medium text-tremor-content-strong dark:text-dark-tremor-content-strong"
          >
            Precio Minorista
            <span className="text-red-500">*</span>
          </label>
          <NumberInput enableStepper={false}
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

          <div className="col-span-full sm:col-span-3">
            <label
              htmlFor="fk_deposito"
              className="text-tremor-default font-medium text-tremor-content-strong dark:text-dark-tremor-content-strong"
            >
              Depósito
              <span className="text-red-500">*</span>
            </label>
            <NumberInput enableStepper={false}
              id="fk_deposito"
              name="fk_deposito"
              autoComplete="fk_deposito"
              placeholder="Deposito"
              className="mt-2"
              value={fk_deposito}
              min={0}
              onChange={(e) => setFk_deposito(e.target.value)}
              required
            />
          </div>

        </div>
      </div>




      <Button variant="primary" type="submit" color='blue'>Guardar</Button>
      <Button variant="secondary" color='blue' onClick={() => {
        // Lógica para descartar
        console.log("Formulario descartado");
        // Reiniciar los valores del formulario
        setId_producto('');
        setStr_nombre('');
        setStr_descripcion('');
        setFk_marca('');
        setFk_categoria('');
        setFk_proveedor('');
        setInt_cantidad_actual('');
        setInt_cantidad_minima('');
        setDc_costo_PPP('');
        setInt_iva('');
        setDc_precio_mayorista('');
        setDc_precio_minorista('');
        setFk_deposito('')
        navigate('/productos');
      }}>Descartar</Button>
    </form>
  );
}