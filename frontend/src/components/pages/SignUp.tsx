import { faLessThan } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import React, { useState, lazy } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import style from './SignUp.module.css';
import {User, Role} from '../../types';
import UserService from '../../service/userService';
const Header = lazy(() => import('./Header'));


const SignUp: React.FC = () => {
  const [firstName, setFirstName] = useState<string>('');
  const [lastName, setLastName] = useState<string>('');
  const [email, setEmail] = useState<string>('');
  const [password, setPassword] = useState<string>('');
  const [address, setAddress] = useState<string>('');
  const [role, setRole] = useState<Role>();
  const [id, setID] = useState<string>();
  
const navigate = useNavigate();

  const signUp = async (e) => {
    e.preventDefault();
    var id = crypto.randomUUID();
    const user: User = {
      firstName: firstName,
      lastName: lastName,
      email: email,
      password: password,
      address: address,
      role: (Role.USER),
      userId: id.toString(),
    };
    const res = await UserService.createUser(user);
    navigate('/app');
  };

  const startPage = () => {
    navigate('/start');
  }

  return (
    <>
      <Header />
      <div className={style.container}>
      <div onClick={startPage} id={style.icon}><FontAwesomeIcon icon={faLessThan} /></div>
        <div className={style.signUpPage}>
          <h1 className={style.signUpTitle}>Sign up</h1>
          <form className={style.signUpFormContainer} onSubmit={signUp} id="signUpForm">
            <div className={style.nameContainer}>
              <input
                type="First Name"
                name="first name"
                onChange={(e) => setFirstName(e.target.value)}
                id="firstName"
                className={style.signUpInput}
                placeholder="first name"
                required
                min="5"
                max="20"
              />
              <input
                type="Last Name"
                name="last name"
                id="lastName"
                onChange={(e) => setLastName(e.target.value)}
                className={style.signUpInput}
                placeholder="last name"
                required
                min="5"
                max="20"
              />
            </div>
            <input
              type="email"
              name="email"
              id="email"
              onChange={(e) => setEmail(e.target.value)}
              className={style.signUpInput}
              placeholder="email"
              required
              pattern="(.*)@(.*).(.*)"
              min="5"
              max="20"
            />
            <input
              type="password"
              name="password"
              id="password"
              onChange={(e) => setPassword(e.target.value)}
              className={style.signUpInput}
              placeholder="password"
              min="5"
              max="20"
            />
            <input
              type="address"
              name="address"
              id="address"
              onChange={(e) => setAddress(e.target.value)}
              className={style.signUpInput}
              placeholder="address"
              min="5"
              max="20"
            />
          </form>
          <button className={style.signUpButton} onClick={signUp} form="signUpForm">
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
