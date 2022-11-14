import React from 'react';
import { Routes, Route } from 'react-router-dom';
import Index from './component';
import CreateRecipe from './component/recipe/CreateRecipe';
import GetRecipe from './components/recipe/GetRecipe';
import GetRecipes from './components/recipe/GetRecipes';
import './css/index.css';

const App: React.FC = () => {
  return (
    <Routes>
      <Route path="/" element={<Index />} />
      <Route path="/createRecipe" element={<CreateRecipe />} />
      <Route path="/recipes" element={<GetRecipes />} />
      <Route path="/recipes/:id" element={<GetRecipe />} />
    </Routes>
  );
};

export default App;
