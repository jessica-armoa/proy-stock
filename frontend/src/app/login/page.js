// src/app/login/page.js
"use client";

import { useState } from 'react';
import { useRouter } from 'next/navigation';
import AuthController from '../../libs/UsuariosController';

const Login = () => {
  console.log(process.env.NEXT_PUBLIC_API_URL)
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');
  const [error, setError] = useState('');
  const router = useRouter();
  const handleSubmit = async (e) => {
    e.preventDefault();

    // Verificar que los campos no estén vacíos
    if (!username || !password) {
      setError('Por favor, complete todos los campos.');
      return;
    }

    try {
      const response = await AuthController.login({ "username": username, "password": password });
      console.log(response.data);
      localStorage.setItem("Token", response.data.token);
      localStorage.setItem("isAuthenticated", true);
      //navigate('/productos');
      router.push("/productos");
    } catch (err) {
      if (err.response) {
        setError(err.response.data);
      } else if (err.request) {
        setError('No hay respuesta del Servidor');
      } else {
        setError('Error en la petición');
      }
    }
  };

  return (
    <div className="min-h-screen flex items-center justify-center bg-gray-100">
      <div className="bg-white p-8 rounded-lg shadow-lg max-w-md w-full">
        <h1 className="text-2xl font-bold mb-6">Inicio De Sesión</h1>
        <form onSubmit={handleSubmit}>
          <div className="mb-4">
            <label htmlFor="username" className="block text-gray-700 mb-2">Usuario:</label>
            <input
              type="text"
              id="username"
              className="w-full p-3 border rounded-lg shadow-sm focus:outline-none focus:border-indigo-500"
              value={username}
              onChange={(e) => setUsername(e.target.value)}
            />
          </div>
          <div className="mb-4">
            <label htmlFor="password" className="block text-gray-700 mb-2">Contraseña:</label>
            <input
              type="password"
              id="password"
              className="w-full p-3 border rounded-lg shadow-sm focus:outline-none focus:border-indigo-500"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
            />
          </div>
          {error && (
            <div className="mb-4 text-red-600">
              {error}
            </div>
          )}
          <button
            type="submit"
            className="w-full bg-indigo-500 text-white p-3 rounded-lg font-semibold hover:bg-indigo-600"
          >
            Iniciar Sesión
          </button>
        </form>
      </div>
    </div>
  );
};

export default Login;
