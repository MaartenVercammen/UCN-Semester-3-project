import React from 'react';
import Swipe from '../swipe/swipe';
import Header from './Header';
import style from './Home.module.css';
import Navbar from './Navbar';
import GetRecipes from '../recipe/GetRecipes';

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
