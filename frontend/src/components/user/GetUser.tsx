import React, { useEffect, useState } from 'react';
import { Link, Navigate, useHref, useNavigate } from 'react-router-dom';
import RecipeService from '../../service/recipeService';
import { Ingredient, Instruction, Recipe, User } from '../../types';
import style from './GetUser.module.css';
import UserService from '../../service/userService';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faCheck, faTimes, faExternalLink } from '@fortawesome/free-solid-svg-icons';
import App from '../../App';
import Liked from '../pages/Liked';

const GetUser: React.FC = () => {
  const [user, setUser] = useState<User>();

  const navigation = useNavigate();

  const getSingleData = async () => {
    const response = await UserService.getUser('00000000-0000-0000-0000-000000000000');
    const data = response.data;
    setUser(data);
    console.log(data);
  };

  useEffect(() => {
    getSingleData();
  }, []);

  const editUser = () => {
    alert('not implemented yet');
  };

  const deleteUser = () => {
    if (user && confirm('Are you sure you want to edit this user?')) {
      // UserService.deleteUser(user.userId.toString());
      alert("don't delete yet pls xd");
    }
  };

  return (
    <>
      {user ? (
        <>
          <div className={style.userContent}>
            <div className={style.userImg}>
              <img
                src="https://media-exp1.licdn.com/dms/image/C5603AQHJsHSXxZvgRw/profile-displayphoto-shrink_400_400/0/1517652728490?e=1674691200&v=beta&t=edyTT84ZBDqPk-JglSjAUeNbZayBVUmLKpnAUCrxt2k"
                alt=""
              />
            </div>
            <div className={style.userDetails}>
              {user.role === 'VERIFIEDUSER' ? (
                <p style={{ color: '#ACDCA8' }}>
                  <FontAwesomeIcon icon={faCheck} /> verified
                </p>
              ) : null}
              {user.role === 'USER' ? (
                <p style={{ color: '#DCBEA8' }}>
                  <FontAwesomeIcon icon={faTimes} /> unverified
                </p>
              ) : null}
              {user.role === 'ADMIN' ? <p style={{ color: '#DCBEA8' }}>administrator</p> : null}
              <h1>
                {user.firstName} {user.lastName}
              </h1>
              <p>{user.email}</p>
            </div>
            <div className={style.usrLink}>
              <Link to={'./liked'}>
                <div className={style.usrLinkChild}>
                  <h4>liked recipes</h4>
                  <FontAwesomeIcon icon={faExternalLink} />
                </div>
              </Link>
              <Link to={'.'}>
                <div className={style.usrLinkChild}>
                  <h4>my recipes</h4>
                  <FontAwesomeIcon icon={faExternalLink} />
                </div>
              </Link>
              <Link to={'.'}>
                <div className={style.usrLinkChild}>
                  <h4>my bamboo sessions</h4>
                  <FontAwesomeIcon icon={faExternalLink} />
                </div>
              </Link>
            </div>
            <div className={style.btnContainer}>
              <button className={style.btn} id={style.editBtn} onClick={editUser}>
                Edit
              </button>
              <button className={style.btn} id={style.deleteBtn} onClick={deleteUser}>
                Delete
              </button>
            </div>
          </div>
        </>
      ) : (
        // change to /login
        <Navigate to="." />
      )}
    </>
  );
};

export default GetUser;
