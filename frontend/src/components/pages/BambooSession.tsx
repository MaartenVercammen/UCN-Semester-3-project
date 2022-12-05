import React, {lazy} from 'react';
import style from './Home.module.css';
const Header = lazy(() => import('./Header'));
const Navbar = lazy(() => import('./Navbar'));
const GetBamboo = lazy(() => import('../bamboo/GetBamboo'));

const BambooSession: React.FC = () => {
  return (
    <>
      <div className={style.homePage}>
        <Header />
        <div className={style.homeContent}>
          <GetBamboo />
        </div>
        <Navbar />
      </div>
    </>
  );
};

export default BambooSession;
