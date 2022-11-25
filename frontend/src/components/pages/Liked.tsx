import React from 'react';
import Swipe from '../swipe/swipe';
import Header from './Header';
import style from './Home.module.css';
import Navbar from './Navbar';
import GetRecipe from '../recipe/GetRecipe';
import GetLikedRecipes from '../recipe/GetLikedRecipes';

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
