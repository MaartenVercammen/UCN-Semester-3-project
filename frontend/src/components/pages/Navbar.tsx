import {
  faHouse,
  faMagnifyingGlass,
  faPlus,
  faUser,
  faHeart
} from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import React, { useEffect, useState } from 'react';
import { Role, User } from '../../types';
import style from './Navbar.module.css';

const Navbar: React.FC = () => {
  const [userId, setUserId] = useState<string>('');
  const [user, setUser] = useState<User>();

  useEffect(() => {
    const tokenstring = sessionStorage.getItem('user');
    if(tokenstring != undefined){
      const user : User = JSON.parse(tokenstring);
      setUserId(user.userId);
      setUser(user);
    }
  }, []);

  const Isallowed = (IsAllowed: Role[]) => {
    const tokenstring = sessionStorage.getItem('user');
    if(tokenstring !== null){
      const loggedInUser: User = JSON.parse(tokenstring);
      if(IsAllowed.includes(loggedInUser.role)){
        return true;
      }
    }

    return false;
  }

  return (
    <>
      <nav className={style.navbar}>
        <ul className={style.navbarParent}>
          <li className={style.navbarChild}>
            <a href="/app">
              <FontAwesomeIcon icon={faHouse} className={style.icon} />
            </a>
          </li>
          <li className={style.navbarChild}>
            <a href="/explore">
              <FontAwesomeIcon icon={faMagnifyingGlass} className={style.icon} />
            </a>
          </li>
          { Isallowed([Role.ADMIN, Role.VERIFIEDUSER]) &&
          <li className={style.navbarChild}>
            <a href="/create">
              <FontAwesomeIcon icon={faPlus} className={style.icon} />
            </a>
          </li>
          }
          <li className={style.navbarChild}>
            <a href={"/user/" + userId}>
              <FontAwesomeIcon icon={faUser} className={style.icon} />
            </a>
          </li>
          <li className={style.navbarChild}>
            <a href={"/user/"+ userId + "/liked"}>
              <FontAwesomeIcon icon={faHeart} className={style.icon} />
            </a>
          </li>
        </ul>
      </nav>
    </>
  );
};

export default Navbar;
