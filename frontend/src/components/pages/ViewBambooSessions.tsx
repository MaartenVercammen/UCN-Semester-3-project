import React, {lazy} from 'react';
import style from './Create.module.css';
const Header = lazy(() => import('./Header'));
const Navbar = lazy(() => import('./Navbar'));
const GetBamboos = lazy(() => import('../bamboo/GetBamboos'));

const ViewBambooSessions: React.FC = () => {
  return (
    <>
      <div className={style.homePage}>
        <Header />
        <div className={style.homeContent}>
            <GetBamboos />
        </div>
        <Navbar />
      </div>
    </>
  );
};

export default ViewBambooSessions;
