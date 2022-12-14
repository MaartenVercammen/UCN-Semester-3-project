import React, { lazy } from 'react';
import ReactDOM from 'react-dom';
import { BrowserRouter } from 'react-router-dom';
import { createRoot } from "react-dom/client";
import './css/index.css';
import Spinner from './helper/spinner/spinner';

const App = lazy(() => import('./App'));

createRoot(document.getElementById('root') || new HTMLElement()).render(
  <React.StrictMode>
    <React.Suspense fallback={<Spinner />}>
      <BrowserRouter>
        <App />
      </BrowserRouter>
    </React.Suspense>
  </React.StrictMode>
)
