import React from 'react';
import style from './Footer.module.css';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faGithub } from '@fortawesome/free-brands-svg-icons';

function Footer() {
  return (
    <footer>
      <div className="footer-container">
        <FontAwesomeIcon icon={faGithub} className="fa" />
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
