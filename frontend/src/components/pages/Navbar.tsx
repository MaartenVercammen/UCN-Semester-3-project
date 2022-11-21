import {
  faHouse,
  faMagnifyingGlass,
  faPlus,
  faUser,
  faBars
} from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import React from 'react';
import style from './Navbar.module.css';

const Navbar: React.FC = () => {
  return (
    <nav className={style.navbar}>
      <ul className={style.navbarParent}>
        <li className={style.navbarChild}>
          <a href="/app">
            <FontAwesomeIcon icon={faHouse} className={style.icon} />
          </a>
        </li>
        <li className={style.navbarChild}>
          <a href="/app">
            <FontAwesomeIcon icon={faMagnifyingGlass} className={style.icon} />
          </a>
        </li>
        <li className={style.navbarChild}>
          <a href="/createrecipe">
            <FontAwesomeIcon icon={faPlus} className={style.icon} />
          </a>
        </li>
        <li className={style.navbarChild}>
          <a href="/app">
            <FontAwesomeIcon icon={faUser} className={style.icon} />
          </a>
        </li>
        <li className={style.navbarChild}>
          <a href="/recipes">
            <FontAwesomeIcon icon={faBars} className={style.icon} />
          </a>
        </li>
      </ul>
    </nav>
  );
};

export default Navbar;
