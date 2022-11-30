import { faLessThan } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import React, { useState, lazy } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import { User, Role } from '../../types';
import UserService from '../../service/userService';
import style from './EditUser.module.css';
const Header = lazy(() => import('../pages/Header'));

const EditUser: React.FC = () => {
  const [firstName, setFirstName] = useState<string>('');
  const [lastName, setLastName] = useState<string>('');
  const [email, setEmail] = useState<string>('');
  const [password, setPassword] = useState<string>('');
  const [checkPassword, setCheckPassword] = useState<string>('');
  const [address, setAddress] = useState<string>('');
  const [role, setRole] = useState<Role>();
  const [id, setID] = useState<string>();

  const navigate = useNavigate();

  const validateForm = () => {
    let ok = true;
    if (firstName.length, lastName.length, email.length, password.length, checkPassword.length, address.length === 0) {
      ok = false;
    }
    return ok;
  };

  const editUser = async (e) => {
    e.preventDefault();
    //todo: update id
    const token = sessionStorage.getItem('user');
    const activeUser: User = JSON.parse(token || '{}');
    const id = activeUser.userId;
    const user: User = (await UserService.getUser(id)).data;
    if (password === checkPassword && validateForm()) {
      user.firstName = firstName;
      user.lastName = lastName;
      user.email = email;
      user.password = password;
      user.address = address;
      user.role = user.role;
      const res = await UserService.updateUser(user);    
      alert('Information updated');
      navigate('/user/6409edb9-16d2-4dde-bdec-def45658aa5a');
    } else if (password !== checkPassword) {
      alert('Passwords do not match');
    } else {
      alert('Please fill out all fields');
    }
    };


  const startPage = () => {
    navigate('/user/6409edb9-16d2-4dde-bdec-def45658aa5a');
  };

  return (
    <>
      <Header />
      <div className={style.container}>
        <div className={style.userImg}>
          <img
            src="https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Fyt3.ggpht.com%2Fa%2FAATXAJy4xq-6KE3NuFJ66mRZz7zmGDCsswMnrgwv1w%3Ds900-c-k-c0xffffffff-no-rj-mo&f=1&nofb=1&ipt=a27325248187d8a9d7f5bd32a673d12185ff26b1d524818319c441619de75895&ipo=images"
            alt=""
          />
        </div>
        <div onClick={startPage} id={style.icon}>
          <FontAwesomeIcon icon={faLessThan} />
        </div>
        <div className={style.signUpPage}>
          <h1 className={style.signUpTitle}>edit your account</h1>
          <form className={style.signUpFormContainer} onSubmit={editUser} id="editForm">
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
            />
            <input
              type="password"
              name="password"
              id="password"
              onChange={(e) => setPassword(e.target.value)}
              className={style.signUpInput}
              placeholder="password"
              required
              min="5"
            />
            <input
              type="password"
              name="confirmPassword"
              id="confirmPassword"
              onChange={(e) => setCheckPassword(e.target.value)}
              className={style.signUpInput}
              placeholder="confirm password"
              required
              min="5"
            />
            <input
              type="address"
              name="address"
              id="address"
              onChange={(e) => setAddress(e.target.value)}
              className={style.signUpInput}
              placeholder="address"
              required
              min="5"
            />
          </form>
          <button className={style.signUpButton} onClick={editUser} form="editForm">
            Update profile
          </button>
        </div>
      </div>
    </>
  );
};

export default EditUser;

