"use client";
import dynamic from "next/dynamic";
import withAuth from "@/components/auth/withAuth";
//import VistaNR from "./movimientos/VistaNR";
import { Card, List, ListItem, ProgressBar } from "@tremor/react";

// Dynamic imports to prevent build-time errors
const Sidebar = dynamic(() => import("@/components/barraNavegacion/Sidebar"), { ssr: false });

function ListCard() {
  let titulo = 'Productos mas vendidos'
  const data = [
    {
      producto: "Athens",
      cantidad: "2 open PR",
      deposito: "Deposito 1"
    },
  ];

  return (
    <Card className="mx-auto max-w-md">
      <h3 className="text-tremor-content-strong dark:text-dark-tremor-content-strong font-medium">
        {titulo}
      </h3>
      <List className="mt-2">
        {data.map((item) => (
          <ListItem key={item.producto}>
            <span>{item.producto}</span>
            <span>{item.cantidad}</span>
            <span>{item.deposito}</span>
          </ListItem>
        ))}
      </List>
    </Card>
  );
}

function ProgressCard() {
  let titulo = "Titulo del card"
  let valor_total = 123000
  let porcentaje = "32% del objetivo mensual"
  let valor_objetivo = 200000

  return (
    <Card className="mx-auto max-w-md">
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

const Dashboard = () => {
  return (
    <div className="flex h-screen w-full bg-ui-background p-2 text-ui-text">
      <Sidebar />
      <div className="flex flex-col w-full h-full p-5 rounded-lg bg-ui-cardbg overflow-y">
        <div className="mt-8 flex items-center space-x-2">
          <ProgressCard></ProgressCard>
          <ListCard></ListCard>
        </div>
      </div>
    </div>
  );
};

export default withAuth(Dashboard);
