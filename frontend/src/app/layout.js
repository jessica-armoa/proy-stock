"use client";

import { BrowserRouter as Router, Route, Routes } from "react-router-dom";
import Detalle from "../../src/app/productos/detalle/page";
import { Inter } from "next/font/google";
import "./globals.css";
import "./globalicons.css";
import Sidebar from "@/components/sidebar";
import Productos from './productos/page';
import Dashboard from './page';
import Movimientos from './movimientos/page';
import Depositos from './depositos/page';
import CrearProducto from './productos/CrearProducto';
import Marcas from './marcas/page';
import CrearMarca from './marcas/CrearMarca';
import CrearDeposito from './depositos/CrearDeposito';
import Proveedores from './proveedores/page';
import CrearProveedor from './proveedores/CrearProveedor';
import CrearFerreteria from './ferreterias/CrearFerreteria';
import Ferreterias from './ferreterias/page';
import Login from './login/page';
import CrearDeposito from "./depositos/CrearDeposito";

const inter = Inter({ subsets: ["latin"] });

export default function RootLayout({ children }) {
  return (
    <html lang="en">
      <Router>
        <Routes>
          <Route path="/depositos" element={< Depositos />} />
          <Route path="/depositos/nuevo" element={< CrearDeposito />} />

          <Route path="/ferreterias" element={< Ferreterias />} />
          <Route path="/ferreterias/nuevo" element={< CrearFerreteria />} />


          <Route path="/marcas" element={< Marcas />} />
          <Route path="/marcas/nuevo" element={< CrearMarca />} />

          <Route path="/movimientos" element={< Movimientos />} />

          <Route path="/productos/detalle/:id" element={<Detalle />} />
          <Route path="/productos" element={<Productos />} />
          <Route path="/" element={< Dashboard />} />
          <Route path="/movimientos" element={< Movimientos />} />
          <Route path="/depositos" element={< Depositos />} />
          <Route path="/depositos/nuevo" element={< CrearDeposito />} />
          <Route path="/productos/nuevo" element={< CrearProducto />} />
          <Route path="/login" element={<Login/>}/>
        </Routes>
      </Router>
      <body className={inter.className}></body>
    </html>
  );
}
