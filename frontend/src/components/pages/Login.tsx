import React from 'react';
import Header from './Header';
import style from './Login.module.css';

const Login: React.FC = () => {

  const login = () => {
    alert('not implemented yet');
  }

  const signUp = () => {
    alert('not implemented yet');
  }

  return (
    <>
    <Header />
    <div className={style.container}>
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
        <button className={style.loginButton} onClick={login}>Log in</button>
        <p className={style.signUpRedirect}>Don't have an account? <a onClick={signUp}>Sign Up</a></p>
      </div>
    </div>
    </>
  );
};

export default Login;
