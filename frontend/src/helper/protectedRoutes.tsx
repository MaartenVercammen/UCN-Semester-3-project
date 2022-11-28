import React from 'react';
import { Navigate, Outlet } from 'react-router-dom';
import { Role } from '../types';

type Props = {
  redirectPath: string;
  isAllowed: Array<Role>;
};

function ProtectedRoutes({ redirectPath, isAllowed }: Props) {
  const isLoggedIn = () => {
    const tokenString = sessionStorage.getItem('user');
    if (tokenString != null) {
      const user = JSON.parse(tokenString);
      if (isAllowed.includes(user.role)) return true;
      return false;
    }
    return false;
  };

  return isLoggedIn() ? <Outlet /> : <Navigate to={redirectPath} />;
}

export default ProtectedRoutes;
