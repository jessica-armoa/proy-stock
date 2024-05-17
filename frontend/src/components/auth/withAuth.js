"use client"; // Marca el componente como Client Component

import { useNavigate } from 'react-router-dom';
import { useEffect, useState } from 'react';

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

export default withAuth