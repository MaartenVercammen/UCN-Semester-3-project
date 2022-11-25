import React from 'react';
import Swipe from '../swipe/swipe';
import Header from './Header';
import style from './Home.module.css';
import Navbar from './Navbar';
import GetRecipe from '../recipe/GetRecipe';

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
