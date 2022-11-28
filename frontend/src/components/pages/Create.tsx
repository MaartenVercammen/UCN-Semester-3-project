import React from 'react';
import Header from './Header';
import style from './Home.module.css';
import Navbar from './Navbar';
import CreateRecipe from '../recipe/CreateRecipe';

const Explore: React.FC = () => {
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

export default Explore;
