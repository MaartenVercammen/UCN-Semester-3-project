import React from 'react';
import { Link, useNavigate } from 'react-router-dom';
import Header from './Header';
import style from './Start.module.css';

const Start: React.FC = () => {

  const navigate = useNavigate();

  const login = () => {
    navigate('/login');
  }
  
  const signUp = () => {
    navigate('/signup');
  }

  return (
    <>
      <div className={style.container}>
      <div className={style.btnStyling}>
        <div className={style.header}><Header /></div>
        <div className={style.btnContainer}>
            <button className={style.logInBtn} onClick={login}>Log in</button>
            <button className={style.signUpBtn} onClick={signUp}>Sign up</button>
        </div>
        </div>
      </div>
    </>
  );
};

export default Start;
