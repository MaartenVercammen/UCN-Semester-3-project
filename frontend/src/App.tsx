import React from 'react';
import { Routes, Route } from 'react-router-dom';
import Index from './component';
import CreateRecipe from './component/recipe/CreateRecipe';
import './css/index.css';

const App:React.FC = () => {
  return (
    <Routes>
      <Route
        path="/"
        element={(
          <Index/>
  )}
      />
      <Route
      path='/createRecipe'
      element={<CreateRecipe/>}
      />
    </Routes>
  );
}

export default App;
