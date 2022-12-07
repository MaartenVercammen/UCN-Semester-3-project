import React, {lazy} from 'react';
import style from './Create.module.css';
const Header = lazy(() => import('./Header'));
const Navbar = lazy(() => import('./Navbar'));
const GetRecipes = lazy(() => import('../recipe/GetRecipes'));

const ViewRecipes: React.FC = () => {
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

export default ViewRecipes;
