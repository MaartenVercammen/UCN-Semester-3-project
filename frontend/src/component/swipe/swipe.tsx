import React, { useMemo, useRef, useState } from 'react';
import TinderCard from 'react-tinder-card';
import useKeypress from 'react-use-keypress';
import Card from './Card';
import style from './swipe.module.css';

const db = [
  {
    img: 'https://cdn.loveandlemons.com/wp-content/uploads/2020/03/pantry-recipes-2-853x1024.jpg',
    title: 'test',
    time: 5
  },
  {
    img: 'https://cdn.loveandlemons.com/wp-content/uploads/2020/03/pantry-recipes-1-900x1024.jpg',
    title: 'test1',
    time: 5
  },
  {
    img: 'https://img.jamieoliver.com/jamieoliver/recipe-database/oldImages/large/1571_2_1437661403.jpg?tr=w-800,h-1066',
    title: 'test2',
    time: 5
  },
  {
    img: 'https://images.everydayhealth.com/images/diet-nutrition/34da4c4e-82c3-47d7-953d-121945eada1e00-giveitup-unhealthyfood.jpg?w=1110',
    title: 'test3',
    time: 5
  }
];

const Swipe: React.FC = () => {
  const [currentindex, setcurrentindex] = useState<number>(db.length - 1);

  const currentIndexRef = useRef(currentindex);

  const childRefs: React.RefObject<any>[] = useMemo(
    () =>
      Array(db.length)
        .fill(0)
        .map((i) => React.createRef()),
    []
  );

  const updateindex = (val) => {
    const newval = val - 1;
    setcurrentindex(newval);
    currentIndexRef.current = newval;
  };

  const onSwipe = (direction: string, index: number) => {
    updateindex(index);
    if (direction === 'left') onSwipeLeft();
    if (direction === 'right') onSwipeRight();
  };

  const swipe = async (dir) => {
    await childRefs[currentindex].current.swipe(dir);
  };

  useKeypress(['ArrowLeft', 'ArrowRight'], (event) => {
    if (event.key === 'ArrowLeft') {
      swipe('left');
    } else {
      swipe('right');
    }
  });

  const onSwipeRight = () => {
    //TODO on swipe right
  };

  const onSwipeLeft = () => {
    //TODO on swipe left
  };

  return (
    <div className={style.container}>
      {currentindex >= 0 ? (
        <>
          <div className={style.carddeck}>
            {db.map((item, index) => (
              <TinderCard
                ref={childRefs[index]}
                key={item.title}
                className={style.swipe}
                preventSwipe={['up', 'down']}
                onSwipe={(dir) => onSwipe(dir, index)}
              >
                <Card title={item.title} img={item.img} time={item.time} />
              </TinderCard>
            ))}
          </div>
          <div className={style.buttons}>
            <button onClick={(e) => swipe('left')}> &#10060;</button>
            <button onClick={(e) => swipe('right')}>&#128154;</button>
          </div>{' '}
        </>
      ) : (
        <div className={style.emptylist}>
          <p>There are no more recipes &#128532;</p>
        </div>
      )}
    </div>
  );
};

export default Swipe;
