import React from 'react';
import { Routes, Route } from 'react-router-dom';
import Index from './component';
import CreateRecipe from './component/recipe/CreateRecipe';
import Swipe from './component/swipe/swipe';
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
      <Route
      path='/swipe'
      element={<Swipe/>}
    </Routes>
  );
}

export default App;
