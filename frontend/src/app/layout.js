"use client";

//import { BrowserRouter as Router, Route, Routes } from "react-router-dom";
//import Detalle from "./productos/detalle";
import { Inter } from "next/font/google";
import "./globals.css";
import "./globalicons.css";
import Dashboard from './page';
/*import Productos from './productos';
import Movimientos from './movimientos';
import Depositos from './depositos'
import CrearProducto from './productos/CrearProducto';
import Marcas from './marcas';
import CrearMarca from './marcas/CrearMarca';
import CrearFerreteria from './ferreterias/CrearFerreteria';
import Ferreterias from './ferreterias';
import Login from './login';
import CrearDeposito from "./depositos/CrearDeposito";
*/
const inter = Inter({ subsets: ["latin"] });

export default function RootLayout({ children }) {
  return (
    <html lang="en">
      <body className={inter.className}>
        <main>{children}</main>
      </body>
    </html>
  );
}
