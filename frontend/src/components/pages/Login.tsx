import React, { useState, lazy } from 'react';
import style from './Login.module.css';
import userService from '../../service/userService';
import { Link, useNavigate } from 'react-router-dom';
import { faLessThan } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
const Header = lazy(() => import('./Header'));

const Login: React.FC = () => {
  const [password, setPassword] = useState<string>('');
  const [email, setEmail] = useState<string>('');

  const navigate = useNavigate();

  const login = async () => {
    let res;
    try {
      res = await userService.login(email, password);
      const data = res.data; //user
      const JWT = res.headers.token; //token
      sessionStorage.setItem('user', JSON.stringify(data));
      sessionStorage.setItem('token', JWT);
      setTimeout(() => {sessionStorage.removeItem('token')}, 1000*60*60);
      navigate('/app');

    } catch (e) {
      alert('Entered email or password is incorrect');
    }
  };

  const startPage = () => {
    navigate('/start');
  };

  const onFormSubmit = (e) => {
    e.preventDefault();
  };

  return (
    <>
      <Header />
      <div className={style.container}>
        <div onClick={startPage} id={style.icon}>
          <FontAwesomeIcon icon={faLessThan} />
        </div>
        <div className={style.loginPage}>
          <h1 className={style.loginTitle}>Log in</h1>
          <form className={style.loginFormContainer} onSubmit={onFormSubmit}>
            <input
              type="email"
              name="email"
              id="email"
              className={style.loginInput}
              placeholder="email"
              value={email}
              onChange={(e) => setEmail(e.target.value)}
            />
            <input
              type="password"
              name="password"
              id="password"
              className={style.loginInput}
              placeholder="password"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
            />
            <button className={style.loginButton} onClick={login} type="submit">
              Log in
            </button>
            <p className={style.signUpRedirect}>
              Don't have an account? <span onClick={() => navigate('/signup')}>Sign Up</span>
            </p>
          </form>
        </div>
      </div>
    </>
  );
};

export default Login;
