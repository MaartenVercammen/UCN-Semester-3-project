import React from 'react';
import GetUser from '../user/GetUser';
import Header from './Header';
import style from './Home.module.css';
import Navbar from './Navbar';

const UserTab: React.FC = () => {
  return (
    <>
      <div className={style.homePage}>
        <Header />
        <div className={style.homeContent}>
          <GetUser />
        </div>
        <Navbar />
      </div>
    </>
  );
};

export default UserTab;
