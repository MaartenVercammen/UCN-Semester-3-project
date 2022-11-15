import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import React, { useEffect, useState } from 'react';
import RecipeService from '../../service/recipeService';
import { Recipe } from '../../types';
import style from './GetRecipes.module.css';
import { Link } from 'react-router-dom';

const GetRecipes: React.FC = () => {
  const [recipes, setRecipes] = useState<any>([]);

  const getData = async () => {
    const response = await RecipeService.getRecipes();
    const data = response.data;
    setRecipes(data);
  };

  useEffect(() => {
    getData();
  }, []);

  return (
    <>
      <h1>all recipes</h1>
      <div className={style.pageContent}>
        {recipes.map((recipe: Recipe) => (
          <Link to={'/recipes/' + recipe.recipeId}>
            <div className={style.recipe} key={recipe.recipeId} id={style.recipeChild}>
              <div className={style.imgWrapper}>
                <img src={recipe.pictureURL} alt="" />
              </div>
              <div className={style.recipeContent} id={style.recipeChild}>
                <h2>{recipe.name}</h2>
                <h4>{recipe.time}</h4>
                <p>{recipe.description}</p>
              </div>
            </div>
          </Link>
        ))}
      </div>
    </>
  );
};

export default GetRecipes;
