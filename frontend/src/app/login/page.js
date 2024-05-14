// pages/login.js
"use Client"
import { useState } from 'react';
import { useRouter } from 'next/router';

const Login = () => {
  const router = useRouter();
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');
  const [error, setError] = useState('');

  const handleLogin = async (e) => {
    e.preventDefault();
    //Realizar la lógica de autenticación, como enviar una solicitud al servidor para verificar las credenciales.
    if (username.trim() === '' || password.trim() === '') {
      setError('Por favor, introduce un nombre de usuario y contraseña.');
    } else {
      try {
        // Enviar una solicitud al servidor para verificar las credenciales
        const response = await fetch('/api/login', {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json',
          },
          body: JSON.stringify({ username, password }),
        });
  
        if (response.ok) {
          router.push('/home');
        } else {
          setError('Credenciales incorrectas. Por favor, inténtalo de nuevo.');
        }
      } catch (error) {
        console.error('Error al iniciar sesión:', error);
        setError('Error al iniciar sesión. Por favor, inténtalo de nuevo más tarde.');
      }
    }
  };
    

  return (
    <div>
      <h1>Iniciar sesión</h1>
      <form onSubmit={handleLogin}>
        <div>
          <label htmlFor="username">Nombre de usuario:</label>
          <input
            type="text"
            id="username"
            value={username}
            onChange={(e) => setUsername(e.target.value)}
          />
        </div>
        <div>
          <label htmlFor="password">Contraseña:</label>
          <input
            type="password"
            id="password"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
          />
        </div>
        {error && <p style={{ color: 'red' }}>{error}</p>}
        <button type="submit">Iniciar sesión</button>
      </form>
    </div>
  );
};

export default Login;
