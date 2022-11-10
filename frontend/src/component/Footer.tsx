import React from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faGithub } from '@fortawesome/free-brands-svg-icons';
import style from './Footer.module.css';

const Footer:React.FC = () => {
  return (
    <footer>
      <div className={style.footer_container}>
        <FontAwesomeIcon icon={faGithub} className={style.fa} />
        <a
          href="https://github.com/MaartenVercammen/UCN-Semester-3-project"
          target="_blank"
          rel="noreferrer"
        >
          GitHub
        </a>
      </div>
    </footer>
  );
}

export default Footer;
