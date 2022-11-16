import React from 'react';
import panda from '../../assets/panda.png';
import style from './Header.module.css';

const Header: React.FC = () => {
  return (
    <header>
      <div className={style.logo}>
        <img src={panda} alt="panda" className={style.panda}></img>
        <h1 className={style.pandaLogo}>FoodPanda</h1>
      </div>
    </header>
  );
};

export default Header;
