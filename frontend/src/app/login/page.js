// src/app/login/page.js
"use client";

import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
const Login = () => {
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');
  const navigate = useNavigate();

  const handleSubmit = async (e) => {
    e.preventDefault();
    if (username === 'user' && password === 'password') {
      localStorage.setItem('isAuthenticated', 'true');
      navigate("/productos");
    } else {
      alert('Credenciales inválidas');
    }
  };

  return (
    <div className="min-h-screen flex items-center justify-center bg-gray-100">
      <div className="bg-white p-8 rounded-lg shadow-lg max-w-md w-full">
        <h1 className="text-2xl font-bold mb-6">Login</h1>
        <form onSubmit={handleSubmit}>
          <div className="mb-4">
            <label htmlFor="username" className="block text-gray-700 mb-2">Username:</label>
            <input
              type="text"
              id="username"
              className="w-full p-3 border rounded-lg shadow-sm focus:outline-none focus:border-indigo-500"
              value={username}
              onChange={(e) => setUsername(e.target.value)}
            />
          </div>
          <div className="mb-4">
            <label htmlFor="password" className="block text-gray-700 mb-2">Password:</label>
            <input
              type="password"
              id="password"
              className="w-full p-3 border rounded-lg shadow-sm focus:outline-none focus:border-indigo-500"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
            />
          </div>
          <button
            type="submit"
            className="w-full bg-indigo-500 text-white p-3 rounded-lg font-semibold hover:bg-indigo-600"
          >
            Login
          </button>
        </form>
      </div>
    </div>
  );
};

export default Login;