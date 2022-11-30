import React, {lazy} from 'react';
import style from './Home.module.css';
const Header = lazy(() => import('./Header'));
const Navbar = lazy(() => import('./Navbar'));
const GetLikedRecipes = lazy(() => import('../recipe/GetLikedRecipes'));

const Liked: React.FC = () => {
  return (
    <>
      <div className={style.homePage}>
        <Header />
        <div className={style.homeContent}>
          <GetLikedRecipes />
        </div>
        <Navbar />
      </div>
    </>
  );
};

export default Liked;
