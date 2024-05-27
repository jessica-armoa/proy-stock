"use client"; // Marca el componente como Client Component

import { useRouter } from 'next/navigation';
//import { useNavigate } from 'react-router-dom';
import { useEffect, useState } from 'react';

const withAuth = (WrappedComponent) => {
  const AuthHOC = (props) => {
    const router = useRouter();
    const [isAuthenticated, setIsAuthenticated] = useState(null);

    useEffect(() => {
      const authStatus = localStorage.getItem('isAuthenticated');
      if (!authStatus) {
        router.push('/login');
      } else {
        setIsAuthenticated(true);
      }
    }, [router]);

    if (isAuthenticated === null) {
      return null;
    }

    return <WrappedComponent {...props} />;
  };

  // Set a display name for the HOC for better debugging
  AuthHOC.displayName = `withAuth(${WrappedComponent.displayName || WrappedComponent.name || 'Component'})`;

  return AuthHOC;
};

export default withAuth;
