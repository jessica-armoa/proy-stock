"use client";
import dynamic from "next/dynamic";
import withAuth from "@/components/auth/withAuth";
import ReportesConfig from "@/controladores/ReportesConfig";
//import VistaNR from "./movimientos/VistaNR";
import {
  Card,
  List,
  ListItem,
  ProgressBar,
  DonutChart,
  Legend,
  Button,
} from "@tremor/react";
import { useState, useEffect } from "react";
import { useRouter } from "next/navigation";

// Dynamic imports to prevent build-time errors
const Sidebar = dynamic(() => import("@/components/barraNavegacion/Sidebar"), {
  ssr: false,
});

function CardStockCritico() {
  const titulo = "Productos en Stock Crítico";

  const [stockCritico, setStockCritico] = useState([]);
  const [currentPage, setCurrentPage] = useState(1);
  const itemsPerPage = 12; // Número de elementos por página
  const router = useRouter();

  const listaStockCritico = async () => {
    try {
      const respuesta = await ReportesConfig.getStockCritico();
      setStockCritico(respuesta.data);
      //console.log(respuesta.data);
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
  //console.log(currentItems)

  const totalPages = Math.ceil(stockCritico.length / itemsPerPage);

  return (
    <div className="max-w-4xl mx-auto">
      <Card
        className="shadow-lg rounded-lg border border-gray-200 p-3"
        style={{ width: "450px" }}
      >
        <h3 className="text-xl font-bold text-gray-800 mb-4">{titulo}</h3>
        <div className="overflow-x-auto">
          <List className="mt-2 divide-y divide-gray-200">
            <ListItem
              className="grid grid-cols-3 py-2 font-semibold text-gray-700"
              style={{ gridTemplateColumns: "4fr 1fr 2fr" }}
            >
              <span className="font-medium">Producto</span>
              <span className="font-medium text-left">Cant.</span>
              <span className="font-medium">Depósito</span>
            </ListItem>
            {currentItems.map((item) => (
              <ListItem
                key={item.id}
                className="grid grid-cols-3 py-2 hover:bg-gray-50"
                style={{ gridTemplateColumns: "4fr 1fr 2fr" }}
              >
                <span className="text-gray-800">{item.str_nombre}</span>
                <span className="font-semibold text-center">
                  {item.int_cantidad_actual}
                </span>
                <span className="text-gray-600">
                  {item.depositoNombre ?? "Depósito Central"}
                </span>
              </ListItem>
            ))}
          </List>
        </div>
        <div className="flex justify-between items-center mt-4">
          <Button
            variant="light"
            color="blue"
            onClick={() => router.push("/productos")}
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
            <span className="text-gray-700">
              {currentPage} de {totalPages}
            </span>
            <Button
              variant="light"
              color="blue"
              onClick={() =>
                setCurrentPage((prev) => Math.min(prev + 1, totalPages))
              }
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
  const titulo = "Productos con mayor rotación";
  const [masVendidos, setMasVendidos] = useState([]);
  const [currentPage, setCurrentPage] = useState(1);
  const itemsPerPage = 15; // Número de elementos por página
  const router = useRouter();

  const listaMasvendidos = async () => {
    try {
      const respuesta = await ReportesConfig.getMasVendidos();
      setMasVendidos(respuesta.data);
      //console.log(respuesta.data);
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
      <Card
        className="shadow-lg rounded-lg border border-gray-200 p-3"
        style={{ width: "350px" }}
      >
        <h3 className="text-xl font-bold text-gray-800 mb-4">{titulo}</h3>
        <div className="overflow-x-auto">
          <List className="mt-2 divide-y divide-gray-200">
            <ListItem
              className="grid grid-cols-3 py-2 font-semibold text-gray-700"
              style={{ gridTemplateColumns: "4fr 1fr 2fr" }}
            >
              <span className="font-medium">Producto</span>
              <span className="font-medium text-left">Cant.</span>
              <span className="font-medium">Depósito</span>
            </ListItem>
            {currentItems.map((item) => (
              <ListItem
                key={item.id}
                className="grid grid-cols-3 py-2 hover:bg-gray-50"
                style={{ gridTemplateColumns: "3fr 1fr 2fr" }}
              >
                <span className="text-gray-800">{item.str_nombre}</span>
                <span className="font-semibold text-center">
                  {item.int_cantidad_actual}
                </span>
                <span className="text-gray-600">
                  {item.depositoNombre ?? "Depósito Central"}
                </span>
              </ListItem>
            ))}
          </List>
        </div>
        
      </Card>
    </div>
  );
}

function CardPerdidasPorDeposito() {
  const titulo = "Cantidad de pérdidas por depósito";
  const [masVendidos, setMasVendidos] = useState([]);
  const [currentPage, setCurrentPage] = useState(1);
  const [totalItemsQuantity, setTotalItemsQuantity] = useState(1);
  const itemsPerPage = 6; // Número de elementos por página
  const router = useRouter();

  const listaMasvendidos = async () => {
    try {
      const respuesta = await ReportesConfig.getPerdidasPorDeposito();
      setMasVendidos(respuesta.data.depositos || []);
      setTotalItemsQuantity(respuesta.data.total);
      //console.log("response",respuesta.data);
    } catch (error) {
      console.error("Error al obtener lista de movimientos: ", error);
    }
  };

  useEffect(() => {
    listaMasvendidos();
  }, []);

  // Validar que masVendidos sea un array
  const validMasVendidos = Array.isArray(masVendidos) ? masVendidos : [];

  // Calcular los datos paginados
  const indexOfLastItem = currentPage * itemsPerPage;
  const indexOfFirstItem = indexOfLastItem - itemsPerPage;
  const currentItems = validMasVendidos.slice(indexOfFirstItem, indexOfLastItem);
  //console.log("current",currentItems);
  const totalPages = Math.ceil(validMasVendidos.length / itemsPerPage);

  return (
    <div className="max-w-4xl mx-auto">
      <Card
        className="shadow-lg rounded-lg border border-gray-200 p-3"
        style={{ width: "350px" }}
      >
        <h3 className="text-xl font-bold text-gray-800 mb-4">{titulo}</h3>
        <div className="overflow-x-auto">
          <List className="mt-2 divide-y divide-gray-200">
            <ListItem
              className="grid grid-cols-3 py-2 font-semibold text-gray-700"
              style={{ gridTemplateColumns: "4fr 1fr 2fr" }}
            >
              <span className="font-medium">Depósito</span>
              <span className="font-medium text-left">Total pérdidas.</span>
            </ListItem>
            {currentItems.map((item,index) => (
              <ListItem
                key={index}
                className="grid grid-cols-3 py-2 hover:bg-gray-50"
                style={{ gridTemplateColumns: "3fr 1fr 2fr" }}
              >
                <span className="text-gray-800">{item.name ?? "Depósito Central"}</span>
                <span className="font-semibold text-center">
                  {item.quantity}
                </span>
              </ListItem>
            ))}
          </List>
        </div>
        <div className="flex justify-between items-center mt-4">
          <Button
            variant="light"
            color="blue"
            onClick={() => router.push("/movimientos")}
          >
            Ver Movimientos
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
            <span className="text-gray-700">
              {currentPage} de {totalPages}
            </span>
            <Button
              variant="light"
              color="blue"
              onClick={() =>
                setCurrentPage((prev) => Math.min(prev + 1, totalPages))
              }
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
  const titulo = "Productos con menor rotación";
  const [menosVendidos, setMenosVendidos] = useState([]);
  const [currentPage, setCurrentPage] = useState(1);
  const itemsPerPage = 15; // Número de elementos por página
  const router = useRouter();

  const listaMenosvendidos = async () => {
    try {
      const respuesta = await ReportesConfig.getMenosVendidos();
      setMenosVendidos(respuesta.data);
      //console.log(respuesta.data);
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
      <Card
        className="shadow-lg rounded-lg border border-gray-200 p-3"
        style={{ width: "450" }}
      >
        <h3 className="text-xl font-bold text-gray-800 mb-4">{titulo}</h3>
        <div className="overflow-x-auto">
          <List className="mt-2 divide-y divide-gray-200">
            <ListItem
              className="grid grid-cols-3 py-2 font-semibold text-gray-700"
              style={{ gridTemplateColumns: "4fr 1fr 2fr" }}
            >
              <span className="font-medium">Producto</span>
              <span className="font-medium text-left">Cant.</span>
              <span className="font-medium">Depósito</span>
            </ListItem>
            {currentItems.map((item) => (
              <ListItem
                key={item.id}
                className="grid grid-cols-3 py-2 hover:bg-gray-50"
                style={{ gridTemplateColumns: "4fr 1fr 2fr" }}
              >
                <span className="text-gray-800">{item.str_nombre}</span>
                <span className="font-semibold text-center">
                  {item.int_cantidad_actual}
                </span>
                <span className="text-gray-600">
                  {item.depositoNombre ?? "Depósito Central"}
                </span>
              </ListItem>
            ))}
          </List>
        </div>
        
      </Card>
    </div>
  );
}

function ProgressCard() {
  const titulo = "Pérdidas por productos";
  const [perdidasPorProducto, setPerdidasPorProducto] = useState([]);
  const listaPerdidas = async () => {
    try {
      const respuesta = await ReportesConfig.getPerdidasPorProducto();
      setPerdidasPorProducto(respuesta.data);
      //console.log(respuesta.data);
    } catch (error) {
      console.error("Error al obtener lista de movimientos: ", error);
    }
  };

  useEffect(() => {
    listaPerdidas();
    //console.log(listaPerdidas())
  }, []);

  let valor_total = perdidasPorProducto.total;
  let porcentaje = "32% del objetivo mensual";
  let valor_objetivo = 200000;

  return (
    <div className="max-w-4xl mx-auto">
      <Card className="shadow-lg rounded-lg border border-gray-200 p-3">
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
    </div>
  );
}

const categoriasPerdidas = (perdidas) => {
  return perdidas.map((p) => ({
    name: p.motivo,
    sales: p.quantity,
  }));
};

function GraficoPerdidasProductos() {
  const titulo = "Pérdidas por productos";
  const [perdidasPorProducto, setPerdidasPorProducto] = useState([]);
  const [data, setData] = useState([]);
  const [categories, setCategories] = useState([]);
  const percentageFormatter = (number) => `${number}%`;
  const colors = ["blue", "cyan", "indigo", "violet", "fuchsia"];

  const listaPerdidas = async () => {
    try {
      const respuesta = await ReportesConfig.getPerdidasPorProducto();
      setPerdidasPorProducto(respuesta.data);
      //console.log(respuesta.data);
    } catch (error) {
      console.error("Error al obtener lista de movimientos: ", error);
    }
  };

  useEffect(() => {
    listaPerdidas();
    //console.log(listaPerdidas())
  }, []);

  useEffect(() => {
    const arrayData = (perdidasPorProducto.perdidas || []).map((p) => {
      return {
        motivo: p.motivo,
        quantity: p.quantity,
      };
    });
    setData(arrayData);
    const categoriesData = arrayData.map((p) => p.motivo);
    setCategories(categoriesData);
  }, [perdidasPorProducto]);

  return (
    <>
      {!data.length ? (
        <div>Cargando...</div>
      ) : (
        <div className="max-w-xl mx-auto">
          <Card className="shadow-lg rounded-lg border border-gray-200 p-3">
            <h3 className="text-xl font-bold text-gray-800 mb-4">{titulo}</h3>
            <div className="flex-col justify-between space-x-6 w-full flex mt-1 justify-center items-center">
              <DonutChart
                data={data}
                category="quantity"
                index="motivo"
                colors={colors}
                className="w-40"
              />
            </div>
            <Legend
              categories={categories}
              colors={colors}
              className="max-w-xs mt-4"
            />
          </Card>
        </div>
      )}
    </>
  );
}

const Dashboard = () => {
  return (
    <div className="flex h-screen w-full bg-ui-background p-2 text-ui-text">
      <Sidebar />
      <div className="w-full h-full p-5 rounded-lg bg-ui-cardbg">
        <div className="flex flex-row justify-between">
          <div className="flex flex-wrap justify-between">
            <CardMasVendidos></CardMasVendidos>
            <CardMenosVendidos></CardMenosVendidos>
            <GraficoPerdidasProductos></GraficoPerdidasProductos>
            <CardPerdidasPorDeposito></CardPerdidasPorDeposito>
          </div>
          <div className="flex flex-col justify-between">
            <CardStockCritico></CardStockCritico>
          </div>
        </div>
      </div>
    </div>
  );
};

export default withAuth(Dashboard);
