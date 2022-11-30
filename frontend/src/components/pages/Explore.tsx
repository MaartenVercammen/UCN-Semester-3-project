import React, {lazy} from 'react';
import style from './Home.module.css';
const Header = lazy(() => import('./Header'));
const Navbar = lazy(() => import('./Navbar'));
const GetRecipes = lazy(() => import('../recipe/GetRecipes'));

const Explore: React.FC = () => {
  return (
    <>
      <div className={style.homePage}>
        <Header />
        <div className={style.homeContent}>
          <GetRecipes />
        </div>
        <Navbar />
      </div>
    </>
  );
};

export default Explore;
