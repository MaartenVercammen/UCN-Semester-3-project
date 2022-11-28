import React from 'react';
import { Link } from 'react-router-dom';
import Header from './Header';
import style from './Login.module.css';
import { faLessThan } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { mainModule } from 'process';

const Login: React.FC = () => {
  const login = () => {
    alert('not implemented yet');
  };

  const mainPage = () => {
    window.location.href = '/main';
  };

  return (
    <>
      <Header />
      <div className={style.container}>
        <div onClick={mainPage} id={style.icon}>
          <FontAwesomeIcon icon={faLessThan} />
        </div>
        <div className={style.loginPage}>
          <h1 className={style.loginTitle}>Log in</h1>
          <form className={style.loginFormContainer}>
            <input
              type="email"
              name="email"
              id="email"
              className={style.loginInput}
              placeholder="email"
            />
            <input
              type="password"
              name="password"
              id="password"
              className={style.loginInput}
              placeholder="password"
            />
          </form>
          <button className={style.loginButton} onClick={login}>
            Log in
          </button>
          <p className={style.signUpRedirect}>
            Don't have an account?{' '}
            <Link to={'/signup'}>
              <a>Sign Up</a>
            </Link>
          </p>
        </div>
      </div>
    </>
  );
};

export default Login;
