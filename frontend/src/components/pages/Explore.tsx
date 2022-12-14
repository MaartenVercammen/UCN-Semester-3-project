import React, {lazy} from 'react';
import { Link } from 'react-router-dom';
import style from './Create.module.css';
const Header = lazy(() => import('./Header'));
const Navbar = lazy(() => import('./Navbar'));

const Explore: React.FC = () => {
  return (
    <>
      <div className={style.homePage}>
        <Header />
        <div className={style.homeContent}>
          <div className={style.createButtons}>
            <Link to={'/recipes'}><h2 className={style.homeContentChild} >explore recipes</h2></Link>
            <Link to={'/bambooSessions'}><h2 className={style.homeContentChild}>explore bamboo sessions</h2></Link>
          </div>
        </div>
        <Navbar />
      </div>
    </>
  );
};

export default Explore;
