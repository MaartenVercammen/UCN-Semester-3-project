import React, { lazy } from 'react';
import style from './Create.module.css';
const Header = lazy(() => import('./Header'));
const Navbar = lazy(() => import('./Navbar'));
const CreateRecipe = lazy(() => import('../recipe/CreateRecipe'));

const Explore: React.FC = () => {
  return (
    <>
      <div className={style.homePage}>
        <Header />
        <div className={style.homeContent}>
          <h2 className={style.homeContentChild}>create recipe</h2>
          <h2 className={style.homeContentChild}>create bamboo session</h2>
        </div>
        <Navbar />
      </div>
    </>
  );
};

export default Explore;
