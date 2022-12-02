import React, { lazy } from 'react';
import style from './Home.module.css';
const Header = lazy(() => import('./Header'));
const Navbar = lazy(() => import('./Navbar'));
const CreateRecipe = lazy(() => import('../recipe/CreateRecipe'));

const Explore: React.FC = () => {
  return (
    <>
      <div className={style.homePage}>
        <Header />
        <div className={style.homeContent}>
          <CreateRecipe />
        </div>
        <Navbar />
      </div>
    </>
  );
};

export default Explore;
