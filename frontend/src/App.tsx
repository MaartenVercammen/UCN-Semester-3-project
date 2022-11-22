import React from 'react';
import { Routes, Route } from 'react-router-dom';
import Index from './components';
import Explore from './components/pages/Explore';
import Home from './components/pages/Home';
import Liked from './components/pages/Liked';
import Recipe from './components/pages/Recipe';
import CreateRecipe from './components/recipe/CreateRecipe';
import GetRecipes from './components/recipe/GetRecipes';
import './css/index.css';

const App: React.FC = () => {
  return (
    <Routes>
      <Route path="/" element={<Index />} />
      <Route path="/app" element={<Home />} />
      <Route path="/createRecipe" element={<CreateRecipe />} />
      <Route path="/createRecipe" element={<CreateRecipe />} />
      <Route path="/recipes" element={<GetRecipes />} />
      <Route path="/recipes/:id" element={<Recipe />} />
      <Route path="/explore" element={<Explore />} />
      <Route path="/user/:id/liked" element={<Liked />} />
    </Routes>
  );
};

export default App;
