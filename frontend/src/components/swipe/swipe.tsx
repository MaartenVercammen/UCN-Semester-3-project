import React, { useMemo, useRef, useState, useEffect, RefObject, lazy } from 'react';
import TinderCard from 'react-tinder-card';
import useKeypress from 'react-use-keypress';
import style from './swipe.module.css';
import RecipeService from '../../service/recipeService';
import { Recipe, SwipedRecipe } from '../../types';
const Card = lazy(() => import('./Card'));

const Swipe: React.FC = () => {
  const [cards, setcards] = useState<Recipe[]>([]);
  const [loading, setLoading] = useState<Boolean>(true);
  let swipeInAction = false;
  const Refs = useMemo<React.RefObject<any>[]>(
    () =>
      Array(4)
        .fill(0)
        .map((i) => React.createRef()),
    []
  );

  useEffect(() => {
    get3Cards();

  }, []);

  const get3Cards = async () => {
    await getCard();
    await getCard();
    await getCard();
    setLoading(false);
  }

  const IsLoading = () => {
    return loading
  }

  const getCard = async () => {
    const res = await RecipeService.getRandomRecipe();
    if(res.status == 401){
      setTimeout(() => {
        getCard();
      }, 1000);
    }
    const recipe: Recipe = res.data;
    cards.push(recipe);
    setcards([...cards]);
  };

  const onSwipe = async (direction: string) => {
    swipeInAction = true;
    await setTimeout(() => {
      cards.shift();
      setcards([...cards]);
    }, 200);

    if (direction === 'left') await onSwipeLeft();
    if (direction === 'right') await onSwipeRight();
    await getCard();
    swipeInAction = false;
  };

  const swipe = async (dir) => {
    if (!swipeInAction){
      await Refs[0].current.swipe(dir);
    }
  };

  useKeypress(['ArrowLeft', 'ArrowRight'], async (event) => {
    if (!swipeInAction) {
      if (event.key === 'ArrowLeft') {
        swipe('left');
      } else {
        await swipe('right');
      }
    }
  });

  const onSwipeRight = async () => {
    const recipe: Recipe = cards[0];
    // TODI: change this to get the current user id
    const authorId: string = '00000000-0000-0000-0000-000000000000';
    if (recipe != undefined) {
      const swipedRecipe: SwipedRecipe = { authorId, recipeId: recipe.recipeId, isLiked: true };
      const res = await RecipeService.swipeRecipe(swipedRecipe);
    }
  };

  const onSwipeLeft = async () => {
    const recipe: Recipe = cards[0];
    // TODI: change this to get the current user id
    const authorId: string = '00000000-0000-0000-0000-000000000000';
    if (recipe != undefined) {
      const swipedRecipe: SwipedRecipe = { authorId, recipeId: recipe.recipeId, isLiked: false };
      const res = await RecipeService.swipeRecipe(swipedRecipe);
    }
  };

  const RecipesLeft = (): boolean => {
    return cards.length > 0
  };

  return (
    <div className={style.container}>
      {IsLoading() ? (<p>loading ...</p>) : (<>
      {RecipesLeft() ? (
        <>
          <div className={style.carddeck}>
            {cards.map((item, index) => (
              <div
                key={item.recipeId}
                style={{
                  position: 'absolute',
                  zIndex: 4 - index,
                  left: 'calc(100vw/2 - 125px)'
                }}
              >
                <TinderCard
                  ref={Refs[index]}
                  className={style.swipe}
                  preventSwipe={['up', 'down']}
                  onSwipe={(dir) => onSwipe(dir)}
                >
                  <Card title={item.name} img={item.pictureURL} time={item.time} />
                </TinderCard>
              </div>
            ))}
          </div>
          <div className={style.buttons}>
            <button onClick={(e) => swipe('left')} className={style.swipeButton}> &#10060;</button>
            <button onClick={(e) => swipe('right')} className={style.swipeButton}>&#128154;</button>
          </div>
        </>
      ) : (
        <div className={style.emptylist}>
          <p>There are no more recipes &#128532;</p>
        </div>
      )}
      </>)}
    </div>
  );
};

export default Swipe;
