import React from 'react';
import { Link } from 'react-router-dom';
import Header from './Header';
import style from './Main.module.css';

const Main: React.FC = () => {
  return (
    <>
      <div className={style.container}>
      <div className={style.btnStyling}>
        <div className={style.header}><Header /></div>
        <div className={style.btnContainer}>
            <Link to={'/login'}><button className={style.logInBtn}>Log in</button></Link>
            <Link to={'/signup'}><button className={style.signUpBtn}>Sign up</button></Link>
        </div>
        </div>
      </div>
    </>
  );
};

export default Main;
