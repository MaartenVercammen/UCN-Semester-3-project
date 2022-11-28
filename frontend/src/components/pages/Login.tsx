import React, { useState } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import Header from './Header';
import style from './Login.module.css';
import { faLessThan } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';

const Login: React.FC = () => {

  const [email, setEmail] = useState<string>('');
  const [password, setPassword] = useState<string>('');

  const navigate = useNavigate();

  const login = async (e) => {
    e.preventDefault();
    alert('not implemented yet');
  };

  const mainPage = () => {
    navigate('/main');
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
          <form className={style.loginFormContainer} id='loginForm'>
            <input
              type="email"
              name="email"
              id="email"
              onChange={(e) => setEmail(e.target.value)}
              className={style.loginInput}
              placeholder="email"
            />
            <input
              type="password"
              name="password"
              id="password"
              onChange={(e) => setPassword(e.target.value)}
              className={style.loginInput}
              placeholder="password"
            />
          </form>
          <button className={style.loginButton} onClick={login} form="loginForm">
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
