import React, {lazy} from 'react';
import { Link, useNavigate } from 'react-router-dom';
import style from './Start.module.css';
const Header = lazy(() => import('./Header'));

const Start: React.FC = () => {
  const navigate = useNavigate();
  return (
    <>
      <div className={style.container}>
      <div className={style.btnStyling}>
        <div className={style.header}><Header /></div>
        <div className={style.btnContainer}>
          <button className={style.logInBtn} onClick={() => navigate('/login')}>Log in</button>
          <button className={style.signUpBtn} onClick={() => navigate('/signup')}>Sign up</button>
        </div>
        </div>
      </div>
    </>
  );
};

export default Start;
