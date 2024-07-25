"use client";
import dynamic from "next/dynamic";
import withAuth from "@/components/auth/withAuth";
import ReportesConfig from "@/controladores/ReportesConfig";
//import VistaNR from "./movimientos/VistaNR";
import { Card, List, ListItem, ProgressBar, DonutChart, Legend, Button} from "@tremor/react";
import { useState, useEffect } from "react";
import { useRouter } from 'next/navigation'

// Dynamic imports to prevent build-time errors
const Sidebar = dynamic(() => import("@/components/barraNavegacion/Sidebar"), {
  ssr: false,
});

function CardStockCritico() {
  const titulo = "Productos en Stock Crítico";

  const [stockCritico, setStockCritico] = useState([]);
  const [currentPage, setCurrentPage] = useState(1);
  const itemsPerPage = 15; // Número de elementos por página
  const router = useRouter();

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

  // Calcular los datos paginados
  const indexOfLastItem = currentPage * itemsPerPage;
  const indexOfFirstItem = indexOfLastItem - itemsPerPage;
  const currentItems = stockCritico.slice(indexOfFirstItem, indexOfLastItem);

  const totalPages = Math.ceil(stockCritico.length / itemsPerPage);

  return (
    <div className="max-w-4xl mx-auto">
      <Card className="shadow-lg rounded-lg border border-gray-200 p-3" style={{ width: '450px' }}>
        <h3 className="text-xl font-bold text-gray-800 mb-4">{titulo}</h3>
        <div className="overflow-x-auto">
          <List className="mt-2 divide-y divide-gray-200">
            <ListItem 
              className="grid grid-cols-3 py-2 font-semibold text-gray-700"
              style={{ gridTemplateColumns: '3fr 1fr 2fr' }} 
            >
              <span className="font-medium">Producto</span>
              <span className="font-medium text-center">Cant.</span>
              <span className="font-medium">Depósito</span>
            </ListItem>
            {currentItems.map((item) => (
              <ListItem 
                key={item.id} 
                className="grid grid-cols-3 py-2 hover:bg-gray-50"
                style={{ gridTemplateColumns: '3fr 1fr 2fr' }} 
              >
                <span className="text-gray-800">{item.str_nombre}</span>
                <span className="font-semibold text-center">
                  {item.int_cantidad_actual}
                </span>
                <span className="text-gray-600">{item.depositoNombre ?? "Depósito Central"}</span>
              </ListItem>
            ))}
          </List>
        </div>
        <div className="flex justify-between items-center mt-4">
          <Button
            variant="light"
            color="blue"
            onClick={() => router.push('/productos')}
          >
            Ver Productos
          </Button>
          <div className="flex space-x-2 items-center">
            <Button
              variant="light"
              color="blue"
              onClick={() => setCurrentPage((prev) => Math.max(prev - 1, 1))}
              disabled={currentPage === 1}
            >
              Anterior
            </Button>
            <span className="text-gray-700">{currentPage} de {totalPages}</span>
            <Button
              variant="light"
              color="blue"
              onClick={() => setCurrentPage((prev) => Math.min(prev + 1, totalPages))}
              disabled={currentPage === totalPages}
            >
              Siguiente
            </Button>
          </div>
        </div>
      </Card>
    </div>
  );
}

function CardMasVendidos() {
  const titulo = "Productos más vendidos";
  const [masVendidos, setMasVendidos] = useState([]);
  const [currentPage, setCurrentPage] = useState(1);
  const itemsPerPage = 15; // Número de elementos por página
  const router = useRouter();

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

  // Calcular los datos paginados
  const indexOfLastItem = currentPage * itemsPerPage;
  const indexOfFirstItem = indexOfLastItem - itemsPerPage;
  const currentItems = masVendidos.slice(indexOfFirstItem, indexOfLastItem);

  const totalPages = Math.ceil(masVendidos.length / itemsPerPage);

  return (
    <div className="max-w-4xl mx-auto">
      <Card className="shadow-lg rounded-lg border border-gray-200 p-3" style={{ width: '350px' }}>
        <h3 className="text-xl font-bold text-gray-800 mb-4">{titulo}</h3>
        <div className="overflow-x-auto">
          <List className="mt-2 divide-y divide-gray-200">
            <ListItem 
              className="grid grid-cols-3 py-2 font-semibold text-gray-700"
              style={{ gridTemplateColumns: '3fr 1fr 2fr' }} 
            >
              <span className="font-medium">Producto</span>
              <span className="font-medium text-center">Cant.</span>
              <span className="font-medium">Depósito</span>
            </ListItem>
            {currentItems.map((item) => (
              <ListItem 
                key={item.id} 
                className="grid grid-cols-3 py-2 hover:bg-gray-50"
                style={{ gridTemplateColumns: '3fr 1fr 2fr' }} 
              >
                <span className="text-gray-800">{item.str_nombre}</span>
                <span className="font-semibold text-center">
                  {item.int_cantidad_actual}
                </span>
                <span className="text-gray-600">{item.depositoNombre ?? "Depósito Central"}</span>
              </ListItem>
            ))}
          </List>
        </div>
        <div className="flex justify-between items-center mt-4">
          <Button
            variant="light"
            color="blue"
            onClick={() => router.push('/productos')}
          >
            Ver Productos
          </Button>
          <div className="flex space-x-2 items-center">
            <Button
              variant="light"
              color="blue"
              onClick={() => setCurrentPage((prev) => Math.max(prev - 1, 1))}
              disabled={currentPage === 1}
            >
              Anterior
            </Button>
            <span className="text-gray-700">{currentPage} de {totalPages}</span>
            <Button
              variant="light"
              color="blue"
              onClick={() => setCurrentPage((prev) => Math.min(prev + 1, totalPages))}
              disabled={currentPage === totalPages}
            >
              Siguiente
            </Button>
          </div>
        </div>
      </Card>
    </div>
  );
}

function CardMenosVendidos() {
  const titulo = "Productos menos vendidos";
  const [menosVendidos, setMenosVendidos] = useState([]);
  const [currentPage, setCurrentPage] = useState(1);
  const itemsPerPage = 15; // Número de elementos por página
  const router = useRouter();

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

  // Calcular los datos paginados
  const indexOfLastItem = currentPage * itemsPerPage;
  const indexOfFirstItem = indexOfLastItem - itemsPerPage;
  const currentItems = menosVendidos.slice(indexOfFirstItem, indexOfLastItem);

  const totalPages = Math.ceil(menosVendidos.length / itemsPerPage);

  return (
    <div className="max-w-4xl mx-auto">
      <Card className="shadow-lg rounded-lg border border-gray-200 p-3" style={{ width: '450' }}>
        <h3 className="text-xl font-bold text-gray-800 mb-4">{titulo}</h3>
        <div className="overflow-x-auto">
          <List className="mt-2 divide-y divide-gray-200">
            <ListItem 
              className="grid grid-cols-3 py-2 font-semibold text-gray-700"
              style={{ gridTemplateColumns: '3fr 1fr 2fr' }} 
            >
              <span className="font-medium">Producto</span>
              <span className="font-medium text-center">Cant.</span>
              <span className="font-medium">Depósito</span>
            </ListItem>
            {currentItems.map((item) => (
              <ListItem 
                key={item.id} 
                className="grid grid-cols-3 py-2 hover:bg-gray-50"
                style={{ gridTemplateColumns: '3fr 1fr 2fr' }} 
              >
                <span className="text-gray-800">{item.str_nombre}</span>
                <span className="font-semibold text-center">
                  {item.int_cantidad_actual}
                </span>
                <span className="text-gray-600">{item.depositoNombre ?? "Depósito Central"}</span>
              </ListItem>
            ))}
          </List>
        </div>
        <div className="flex justify-between items-center mt-4">
          <Button
            variant="light"
            color="blue"
            onClick={() => router.push('/productos')}
          >
            Ver Productos
          </Button>
          <div className="flex space-x-2 items-center">
            <Button
              variant="light"
              color="blue"
              onClick={() => setCurrentPage((prev) => Math.max(prev - 1, 1))}
              disabled={currentPage === 1}
            >
              Anterior
            </Button>
            <span className="text-gray-700">{currentPage} de {totalPages}</span>
            <Button
              variant="light"
              color="blue"
              onClick={() => setCurrentPage((prev) => Math.min(prev + 1, totalPages))}
              disabled={currentPage === totalPages}
            >
              Siguiente
            </Button>
          </div>
        </div>
      </Card>
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
        <div className="flex justify-between space-x-6">
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
        <div className="flex justify-between">
        
          <CardMasVendidos></CardMasVendidos>
          <CardMenosVendidos></CardMenosVendidos>
          <CardStockCritico></CardStockCritico>
        </div>
      
          
      </div>
    </div>
  );
};

export default withAuth(Dashboard);
