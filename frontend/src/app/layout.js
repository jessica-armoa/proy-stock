"use client";

import { BrowserRouter as Router, Route, Routes } from "react-router-dom";
import Detalle from "../../src/app/productos/detalle/page";
import { Inter } from "next/font/google";
import "./globals.css";
import "./globalicons.css";
import Productos from "./productos/page";
import Dashboard from "./page";
import Movimientos from "./movimientos/page";
import Depositos from "./depositos/page";
import CrearProducto from "./productos/CrearProducto";

const inter = Inter({ subsets: ["latin"] });

export default function RootLayout({ children }) {
  return (
    <html lang="en">
      <Router>
        <Routes>
          <Route path="/productos/detalle/:id" element={<Detalle />} />
          <Route path="/productos" element={<Productos />} />
          <Route path="/" element={<Dashboard />} />
          <Route path="/movimientos" element={<Movimientos />} />
          <Route path="/depositos" element={<Depositos />} />
          <Route path="/productos/nuevo" element={<CrearProducto />} />
        </Routes>
      </Router>
      <body className={inter.className}></body>
    </html>
  );
}
