import React, {lazy} from 'react';
import style from './Home.module.css';
const Header = lazy(() => import('./Header'));
const Navbar = lazy(() => import('./Navbar'));
const GetUser = lazy(() => import('../user/GetUser'));

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
