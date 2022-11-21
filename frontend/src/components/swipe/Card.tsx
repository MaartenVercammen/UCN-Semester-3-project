import { faClock } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import React from 'react';
import style from './Card.module.css';

type Props = {
  img: string;
  title: string;
  time: number;
};

const Card: React.FC<Props> = ({ img, title, time }: Props) => {
  return (
    <div className={style.container} style={{ backgroundImage: `url(${img})` }}>
      <div className={style.cardcontent}>
        <p>{title}</p>
        <p className={style.smaller}>
          <FontAwesomeIcon icon={faClock} />
          {time} min
        </p>
      </div>
    </div>
  );
};

export default Card;
