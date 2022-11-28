import { faLessThan } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import React from 'react';
import { Link } from 'react-router-dom';
import Header from './Header';
import Login from './Login';
import style from './SignUp.module.css';

const SignUp: React.FC = () => {
  const signUp = () => {
    alert('not implemented yet');
  };

  const mainPage = () => {
    window.location.href = '/main';
  }

  return (
    <>
      <Header />
      <div className={style.container}>
      <div onClick={mainPage} id={style.icon}><FontAwesomeIcon icon={faLessThan} /></div>
        <div className={style.signUpPage}>
          <h1 className={style.signUpTitle}>Sign up</h1>
          <form className={style.signUpFormContainer}>
            <div className={style.nameContainer}>
              <input
                type="First Name"
                name="first name"
                id="firstName"
                className={style.signUpInput}
                placeholder="first name"
              />
              <input
                type="Last Name"
                name="last name"
                id="lastName"
                className={style.signUpInput}
                placeholder="last name"
              />
            </div>
            <input
              type="email"
              name="email"
              id="email"
              className={style.signUpInput}
              placeholder="email"
            />
            <input
              type="password"
              name="password"
              id="password"
              className={style.signUpInput}
              placeholder="password"
            />
            <input
              type="address"
              name="address"
              id="address"
              className={style.signUpInput}
              placeholder="address"
            />
          </form>
          <button className={style.signUpButton} onClick={signUp}>
            Sign up
          </button>
          <p className={style.loginRedirect}>
            Already have an account? <Link to={'/login'}><a>Log in</a></Link>
          </p>
        </div>
      </div>
    </>
  );
};

export default SignUp;
