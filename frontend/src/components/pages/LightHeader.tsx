import React from 'react';
import panda from '../../assets/panda.png';
import style from './LightHeader.module.css';

const LightHeader: React.FC = () => {
  return (
    <header>
        <nav>
          <div className={style.logo}>
            <img src={panda} alt='panda' className={style.panda}></img>
            <h1 className={style.pandaLogo}>FoodPanda<span>.dev</span></h1>
          </div>
        </nav>
    </header>
  );
};

export default LightHeader;
