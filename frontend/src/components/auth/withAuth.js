"use client"; // Marca el componente como Client Component

import { useNavigate } from 'react-router-dom';
import { useEffect, useState } from 'react';
/*
const withAuth = (WrappedComponent) => {
  return (props) => {
    const navigate = useNavigate();
    const [isAuthenticated, setIsAuthenticated] = useState(null);

    useEffect(() => {
      const authStatus = localStorage.getItem('isAuthenticated');
      if (!authStatus) {
        navigate('/login'); 
      } else {
        setIsAuthenticated(true);
      }
    }, [navigate]);

    if (isAuthenticated === null) {
      return null; 
    }

    return <WrappedComponent {...props} />;
  };
};
*/
const withAuth = (WrappedComponent) => {
  const AuthHOC = (props) => {
    const navigate = useNavigate();
    const [isAuthenticated, setIsAuthenticated] = useState(null);

    useEffect(() => {
      const authStatus = localStorage.getItem('isAuthenticated');
      if (!authStatus) {
        navigate('/login');
      } else {
        setIsAuthenticated(true);
      }
    }, [navigate]);

    if (isAuthenticated === null) {
      return null;
    }

    return <WrappedComponent {...props} />;
  };

  // Set a display name for the HOC for better debugging
  AuthHOC.displayName = `withAuth(${WrappedComponent.displayName || WrappedComponent.name || 'Component'})`;

  return AuthHOC;
};
export default withAuth