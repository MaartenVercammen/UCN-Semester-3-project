import React from 'react';
import ReactDOM from 'react-dom';
import { BrowserRouter } from 'react-router-dom';
import { createRoot } from "react-dom/client";
import App from './App';
import './css/index.css';

createRoot(document.getElementById('root') || new HTMLElement()).render(<React.StrictMode>
      <BrowserRouter>
          <App />
      </BrowserRouter>
  </React.StrictMode>)
