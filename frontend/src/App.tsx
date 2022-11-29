import React from 'react';
import { Routes, Route } from 'react-router-dom';
import Index from './components';
import Create from './components/pages/Create';
import Explore from './components/pages/Explore';
import Home from './components/pages/Home';
import Liked from './components/pages/Liked';
import Login from './components/pages/Login';
import Recipe from './components/pages/Recipe';
import SignUp from './components/pages/SignUp';
import UserTab from './components/pages/UserTab';
import GetRecipes from './components/recipe/GetRecipes';
import './css/index.css';
import Start from './components/pages/Start';
import EditUser from './components/user/EditUser';

const App: React.FC = () => {
  return (
    <Routes>
      <Route path="/" element={<Index />} />
      <Route path="/app" element={<Home />} />
      <Route path="/createRecipe" element={<Create />} />
      <Route path="/recipes" element={<GetRecipes />} />
      <Route path="/recipes/:id" element={<Recipe />} />
      <Route path="/explore" element={<Explore />} />
      <Route path="/user/:id/liked" element={<Liked />} />
      <Route path="/user/:id" element={<UserTab />} />
      <Route path="/login" element={<Login />} />
      <Route path='/signup' element={<SignUp />} />
      <Route path="/start" element={<Start />} />
      <Route path="/user/:id/edit" element={<EditUser />} />
    </Routes>
  );
};

export default App;
