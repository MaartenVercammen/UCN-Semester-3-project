import React, { lazy } from 'react';
import style from './Home.module.css';
const Header = lazy(() => import('./Header'));
const Navbar = lazy(() => import('./Navbar'));
const Swipe = lazy(() => import('../swipe/swipe'));


const Home: React.FC = () => {

  return (
    <div className={style.homePage}>
      <Header />
      <div className={style.swipeCard}>
        <Swipe />
      </div>
      <Navbar />
    </div>
  );
};

export default Home;
