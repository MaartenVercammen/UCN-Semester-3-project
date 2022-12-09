import React, {lazy} from 'react';
import Header from '../pages/Header';
import Navbar from '../pages/Navbar';
import style from '../pages/Home.module.css';
import UpdateRecipe from './UpdateRecipe';
const GetLikedRecipes = lazy(() => import('../recipe/GetLikedRecipes'));

const Liked: React.FC = () => {
  return (
    <>
      <div className={style.homePage}>
        <Header />
        <div className={style.homeContent}>
          <UpdateRecipe />
        </div>
        <Navbar />
      </div>
    </>
  );
};

export default Liked;
