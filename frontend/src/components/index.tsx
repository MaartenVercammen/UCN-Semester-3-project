import React, { lazy } from 'react'
const Main = lazy(() => import('./MainPage'));
const Navbar = lazy(() => import('./Navbar'));
const Footer = lazy(() => import('./Footer'));


const Index: React.FC = () => {
  
  return (
    <div><Navbar/>
    <Main/>
    <Footer/></div>
  )
}

export default Index
