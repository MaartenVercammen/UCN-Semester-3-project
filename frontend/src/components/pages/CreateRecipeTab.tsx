import React, { lazy } from 'react';
import { Link } from 'react-router-dom';
import style from './Create.module.css';
const Header = lazy(() => import('./Header'));
const Navbar = lazy(() => import('./Navbar'));
const CreateRecipe = lazy(() => import('../recipe/CreateRecipe'));

const CreateRecipeTab: React.FC = () => {
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

export default CreateRecipeTab;
