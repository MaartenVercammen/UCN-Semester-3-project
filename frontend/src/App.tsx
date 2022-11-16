import React from 'react';
import { Routes, Route } from 'react-router-dom';
import Index from './components';
import CreateRecipe from './components/recipe/CreateRecipe';
import GetRecipe from './components/recipe/GetRecipe';
import GetRecipes from './components/recipe/GetRecipes';
import Swipe from './components/swipe/swipe';
import './css/index.css';

const App: React.FC = () => {
  return (
    <Routes>
      <Route path="/" element={<Index />} />
      <Route path="/createRecipe" element={<CreateRecipe />} />
      <Route path="/createRecipe" element={<CreateRecipe />} />
      <Route path="/recipes" element={<GetRecipes />} />
      <Route path="/recipes/:id" element={<GetRecipe />} />
      <Route path="/swipe" element={<Swipe />} />
    </Routes>
  );
};

export default App;
