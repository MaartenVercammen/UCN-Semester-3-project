import React, { lazy } from 'react';
import { Link } from 'react-router-dom';
import style from './Create.module.css';
const Header = lazy(() => import('./Header'));
const Navbar = lazy(() => import('./Navbar'));
const CreateBamboo = lazy(() => import('../bamboo/CreateBamboo'));

const CreateBambooTab: React.FC = () => {
  return (
    <>
      <div className={style.homePage}>
        <Header />
        <div className={style.homeContent}>
            <CreateBamboo />
        </div>
        <Navbar />
      </div>
    </>
  );
};

export default CreateBambooTab;
