import React, { lazy } from 'react';
import { Link } from 'react-router-dom';
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
          <div className={style.createButtons}>
            <Link to={'/createRecipe'}><h2 className={style.homeContentChild} >create recipe</h2></Link>
            <Link to={'/createBamboo'}><h2 className={style.homeContentChild}>create bamboo session</h2></Link>
          </div>
        </div>
        <Navbar />
      </div>
    </>
  );
};

export default Explore;
