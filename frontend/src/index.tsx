import React, { lazy } from 'react';
import ReactDOM from 'react-dom';
import { BrowserRouter } from 'react-router-dom';
import { createRoot } from "react-dom/client";

import './css/index.css';

const App = lazy(() => import('./App'));

createRoot(document.getElementById('root') || new HTMLElement()).render(<React.StrictMode>
      <BrowserRouter>
          <App />
      </BrowserRouter>
  </React.StrictMode>)
