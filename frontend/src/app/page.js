"use client";
import dynamic from "next/dynamic";
import withAuth from "@/components/auth/withAuth";
import ReportesConfig from "@/controladores/ReportesConfig";
//import VistaNR from "./movimientos/VistaNR";
import { Card, List, ListItem, ProgressBar, DonutChart, Legend  } from "@tremor/react";
import { useState, useEffect } from "react";

// Dynamic imports to prevent build-time errors
const Sidebar = dynamic(() => import("@/components/barraNavegacion/Sidebar"), {
  ssr: false,
});

function CardStockCritico() {
  let titulo = "Productos en Stock Critico";

  const [stockCritico, setStockCritico] = useState([]);

  const listaStockCritico = async () => {
    try {
      const respuesta = await ReportesConfig.getStockCritico();
      setStockCritico(respuesta.data);
      console.log(respuesta.data);
    } catch (error) {
      console.error("Error al obtener lista de productos: ", error);
    }
  };

  useEffect(() => {
    listaStockCritico();
  }, []);

 

  return (
    <div>
      <div>
      <Card className="mx-auto max-w-md">
        <h3 className="text-tremor-content-strong dark:text-dark-tremor-content-strong font-medium">
       {titulo}
        </h3>
        <List className="mt-2">
          {stockCritico.map((item) => (
            <ListItem key={item.id}>
              <span>{item.str_nombre}</span>
              <span>{item.int_cantidad_actual}</span>
              <span>{item.depositoNombre ?? "Depósito Central"}</span>
            </ListItem>
          ))}
        </List>
      </Card>
      </div>
    </div>
  );
}

function CardMasVendidos() {
  let titulo = "Productos más vendidos";

  const [masVendidos, setMasVendidos] = useState([]);

  const listaMasvendidos = async () => {
    try {
      const respuesta = await ReportesConfig.getMasVendidos();
      setMasVendidos(respuesta.data);
      console.log(respuesta.data);
    } catch (error) {
      console.error("Error al obtener lista de productos: ", error);
    }
  };

  useEffect(() => {
    listaMasvendidos();
  }, []);

 

  return (
    <div>
         
      <div>
      <Card className="mx-auto max-w-md">
        <h3 className="text-tremor-content-strong dark:text-dark-tremor-content-strong font-medium">
        {titulo}
        </h3>
        <List className="mt-2">
          {masVendidos.map((item) => (
            <ListItem key={item.id}>
              <span>{item.str_nombre}</span>
              <span>{item.int_cantidad_actual}</span>
              <span>{item.depositoNombre ?? "Depósito Central"}</span>
            </ListItem>
          ))}
        </List>
      </Card>
      </div>
    </div>
  );
}

function CardMenosVendidos() {
  let titulo = "Productos menos vendidos";

  const [menosVendidos, setMenosVendidos] = useState([]);

  const listaMenosvendidos = async () => {
    try {
      const respuesta = await ReportesConfig.getMenosVendidos();
      setMenosVendidos(respuesta.data);
      console.log(respuesta.data);
    } catch (error) {
      console.error("Error al obtener lista de productos: ", error);
    }
  };

  useEffect(() => {
    listaMenosvendidos();
  }, []);

 

  return (
    <div>
         
      <div>
      <Card className="mx-auto max-w-auto">
        <h3 className="text-tremor-content-strong dark:text-dark-tremor-content-strong font-medium">
        {titulo}
        </h3>
        <List className="mt-2">
          {menosVendidos.map((item) => (
            <ListItem key={item.id}>
              <span>{item.str_nombre}</span>
              <span>{item.int_cantidad_actual}</span>
              <span>{item.depositoNombre ?? "Depósito Central"}</span>
            </ListItem>
          ))}
        </List>
      </Card>
      </div>
    </div>
  );
}

function ProgressCard() {
  let titulo = "Titulo del card";
  let valor_total = 123000;
  let porcentaje = "32% del objetivo mensual";
  let valor_objetivo = 200000;

  return (
    <Card className="mx-auto max-w-md max-h-auto">
      <h4 className="text-tremor-default text-tremor-content dark:text-dark-tremor-content">
        {titulo}
      </h4>
      <p className="text-tremor-metric font-semibold text-tremor-content-strong dark:text-dark-tremor-content-strong">
        Gs. {valor_total}
      </p>
      <p className="mt-4 flex items-center justify-between text-tremor-default text-tremor-content dark:text-dark-tremor-content">
        <span>{porcentaje}</span>
        <span>Gs. {valor_objetivo}</span>
      </p>
      <ProgressBar value={32} className="mt-2" />
    </Card>
  );
}


  const sales = [
    {
      name: 'New York',
      sales: 980,
    },
    {
      name: 'London',
      sales: 456,
    },
    {
      name: 'Hong Kong',
      sales: 390,
    },
    {
      name: 'San Francisco',
      sales: 240,
    },
    {
      name: 'Singapore',
      sales: 190,
    },
  ];
  
  const valueFormatter = (number) =>
    `$ ${Intl.NumberFormat('us').format(number).toString()}`;
  
  function DonutChartUsageExample() {
    return (
      <>
      <Card className="mx-auto max-w-xl max-h-auto">
        <div className="flex items-center justify-center space-x-6">
          <DonutChart
            data={sales}
            category="sales"
            index="name"
            valueFormatter={valueFormatter}
            colors={['blue', 'cyan', 'indigo', 'violet', 'fuchsia']}
            className="w-40"
          />
          <Legend
            categories={['New York', 'London', 'Hong Kong', 'San Francisco', 'Singapore']}
            colors={['blue', 'cyan', 'indigo', 'violet', 'fuchsia']}
            className="max-w-xs"
          />
        </div>
        </Card>
      </>
    );
  }  


const Dashboard = () => {
  return (
    <div className="flex h-screen w-full bg-ui-background p-2 text-ui-text">
      <Sidebar />
      <div className="w-full h-full p-5 rounded-lg bg-ui-cardbg overflow-y">
        <div className="flex justify-around">
        
          <CardMasVendidos></CardMasVendidos>
          <CardMenosVendidos></CardMenosVendidos>
          <CardStockCritico></CardStockCritico>
        </div>
      
          
      </div>
    </div>
  );
};

export default withAuth(Dashboard);
