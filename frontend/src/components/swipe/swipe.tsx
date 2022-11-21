import React, { useMemo, useRef, useState, useEffect } from 'react';
import TinderCard from 'react-tinder-card';
import useKeypress from 'react-use-keypress';
import Card from './Card';
import style from './swipe.module.css';
import RecipeService from '../../service/recipeService';
import { Recipe, SwipedRecipe } from '../../types';

const Swipe: React.FC = () => {
  const [currentindex, setcurrentindex] = useState<number>(0);
  const [cards, setcards] = useState<(Recipe | undefined)[]>([]);
  const [refs, setRefs] = useState<React.RefObject<any>[]>([]);

  useEffect(() => {
    getCard();
    getCard();
    getCard();
  }, []);

  const getCard = async () => {
    const res = await RecipeService.getRandomRecipe();
    const recipe: Recipe = res.data;
    cards.push(recipe);
    const list = cards;
    setcards(list);
    setRefs([...refs, React.createRef()]);
  };

  const updateindex = () => {
    const newval = currentindex + 1;
    setcurrentindex(newval);
  };

  const onSwipe = (direction: string) => {
    setTimeout(() => {
      cards[currentindex] = undefined;
      setcards([...cards]);
    }, 500);

    getCard();
    updateindex();
    if (direction === 'left') onSwipeLeft();
    if (direction === 'right') onSwipeRight();
  };

  const swipe = async (dir) => {
    await refs[currentindex].current.swipe(dir);
  };

  useKeypress(['ArrowLeft', 'ArrowRight'], (event) => {
    if (event.key === 'ArrowLeft') {
      swipe('left');
    } else {
      swipe('right');
    }
  });

  const onSwipeRight = async () => {
    const recipe: Recipe | undefined = cards[currentindex];
    // TODI: change this to get the current user id
    const authorId: string = '00000000-0000-0000-0000-000000000000';
    if (recipe != undefined) {
      const swipedRecipe: SwipedRecipe = { authorId, recipeId: recipe.recipeId, isLiked: true };
      const res = await RecipeService.swipeRecipe(swipedRecipe);
    }
  };

  const onSwipeLeft = async () => {
    const recipe: Recipe | undefined = cards[currentindex];
    // TODI: change this to get the current user id
    const authorId: string = '00000000-0000-0000-0000-000000000000';
    if (recipe != undefined) {
      const swipedRecipe: SwipedRecipe = { authorId, recipeId: recipe.recipeId, isLiked: false };
      const res = await RecipeService.swipeRecipe(swipedRecipe);
    }
  };

  const RecipesLeft = (): boolean => {
    const c = cards.filter((i) => i != undefined);
    return c.length > 0;
  };

  return (
    <div className={style.container}>
      {RecipesLeft() ? (
        <>
          <div className={style.carddeck}>
            {cards.map((item, index) => {
              if (item != undefined) {
                return (
                  <div
                    key={item.recipeId}
                    style={{
                      position: 'absolute',
                      zIndex: 9999 - index,
                      left: 'calc(100vw/2 - 125px)'
                    }}
                  >
                    <TinderCard
                      ref={refs[index]}
                      className={style.swipe}
                      preventSwipe={['up', 'down']}
                      onSwipe={(dir) => onSwipe(dir)}
                    >
                      <Card title={item.name} img={item.pictureURL} time={item.time} />
                    </TinderCard>
                  </div>
                );
              }
            })}
          </div>
          <div className={style.buttons}>
            <button onClick={(e) => swipe('left')}> &#10060;</button>
            <button onClick={(e) => swipe('right')}>&#128154;</button>
          </div>
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
