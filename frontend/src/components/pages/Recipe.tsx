import React, {lazy} from 'react';
import Swipe from '../swipe/swipe';
import style from './Home.module.css';
const Header = lazy(() => import('./Header'));
const Navbar = lazy(() => import('./Navbar'));
const GetRecipe = lazy(() => import('../recipe/GetRecipe'));

const Recipe: React.FC = () => {
  return (
    <>
      <div className={style.homePage}>
        <Header />
        <div className={style.homeContent}>
          <GetRecipe />
        </div>
        <Navbar />
      </div>
    </>
  );
};

export default Recipe;
