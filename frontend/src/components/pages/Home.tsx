import React from 'react';
import Swipe from '../swipe/swipe';
import Header from './Header';
import style from './Home.module.css';
import Navbar from './Navbar';

const Home: React.FC = () => {
  return (
    <div className={style.homePage}>
      <Header />
      <div className={style.swipeCard}>
        <Swipe />
      </div>
      <Navbar />
    </div>
  );
};

export default Home;
