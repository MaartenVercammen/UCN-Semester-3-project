import React from 'react';
import panda from '../../assets/panda.png';
import heart from '../../assets/heart.png';
import style from '../Navbar.module.css';


const Header: React.FC = () => {
  return (
    <header>
        <nav>
          <div className={style.logo}>
            <img src={panda} alt='panda' className={style.panda} />
            <h1 className={style.pandaLogo}>FoodPanda<span>.dev</span></h1>
            <img src={heart} alt="heart" className={style.heart} />
          </div>
        </nav>
    </header>
  );
};

export default Header;
