import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faClock } from '@fortawesome/free-solid-svg-icons';
import React, { useEffect, useState } from 'react';
import RecipeService from '../../service/recipeService';
import { Recipe, User } from '../../types';
import style from './GetRecipes.module.css';
import { Link } from 'react-router-dom';

const GetLikedRecipes: React.FC = () => {
  const [recipes, setRecipes] = useState<(any)>([]);
  const [user, setUser] = useState<any>([]);
  
  const getData = async () => {
    const response = await RecipeService.getLiked(user.userId);
    const data = response.data;
    setRecipes(data);
  };
  
  useEffect(() => {
    // TODO: get signed in user
    user.userId = '00000000-0000-0000-0000-000000000000';
    setUser(user);
    getData();
  }, []);

  return (
    <>
      <div className={style.pageContent}>
        <h2>liked recipes</h2>
        {recipes.map((recipe: Recipe) => (
          <Link to={'/recipes/' + recipe.recipeId}>
            <div className={style.recipe} key={recipe.recipeId} id={style.recipeChild}>
              <div className={style.imgWrapper}>
                <img src={recipe.pictureURL} alt="" />
              </div>
              <div className={style.recipeContent} id={style.recipeChild}>
                <h2>{recipe.name}</h2>
                <div className={style.prepTime}>
                  <FontAwesomeIcon icon={faClock} />
                  <p>{recipe.time}</p>
                </div>
              </div>
            </div>
          </Link>
        ))}
      </div>
    </>
  );
};

export default GetLikedRecipes;
