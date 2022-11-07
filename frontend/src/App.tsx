import React from 'react';
import { Routes, Route } from 'react-router-dom';
import Index from './component';
import './css/index.css';

function App() {
  return (
    <Routes>
      <Route
        path="/"
        element={(
          <Index/>
  )}
      />
    </Routes>
  );
}

export default App;
