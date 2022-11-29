import React from 'react';
import { Routes, Route } from 'react-router-dom';
import Index from './components';
import Create from './components/pages/Create';
import Explore from './components/pages/Explore';
import Home from './components/pages/Home';
import Liked from './components/pages/Liked';
import Login from './components/pages/Login';
import Recipe from './components/pages/Recipe';
import UserTab from './components/pages/UserTab';
import GetRecipes from './components/recipe/GetRecipes';
import './css/index.css';
import ProtectedRoutes from './helper/protectedRoutes';
import { Role } from './types';

const App: React.FC = () => {
  return (
    <Routes>
      <Route path="/" element={<Index />} />
      <Route path="/login" element={<Login />} />

      <Route path='/' element={<ProtectedRoutes isAllowed={[Role.ADMIN, Role.VERIFIEDUSER, Role.USER ]} redirectPath="/login" />}>
        <Route path="/recipes" element={<GetRecipes />} />
        <Route path="/recipes/:id" element={<Recipe />} />
        <Route path="/explore" element={<Explore />} />
        <Route path="/user/:id/liked" element={<Liked />} />
        <Route path="/user/:id" element={<UserTab />} />
        <Route path="/app" element={<Home />} />
      </Route>

      <Route path='/' element={<ProtectedRoutes isAllowed={[Role.ADMIN, Role.VERIFIEDUSER]} redirectPath="/app" />}>
        <Route path="/createRecipe" element={<Create />} />
      </Route>

      <Route path='/' element={<ProtectedRoutes isAllowed={[Role.ADMIN]} redirectPath="/app" />}>
        //admin only routes
      </Route>
    </Routes>
  );
};

export default App;
