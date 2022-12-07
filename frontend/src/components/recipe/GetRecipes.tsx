import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faClock } from '@fortawesome/free-solid-svg-icons';
import React, { useEffect, useState } from 'react';
import RecipeService from '../../service/recipeService';
import { Recipe } from '../../types';
import style from './GetRecipes.module.css';
import { useNavigate } from 'react-router-dom';

const GetRecipes: React.FC = () => {
  const [recipes, setRecipes] = useState<any>([]);

  const navigate = useNavigate();

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
      <div className={style.pageContent}>
        <h2 style={{ color: '#A8ACDC' }}>explore <span style={{ color: '#444444' }}>recipes</span></h2>
        {recipes.map((recipe: Recipe) => (
          <div
            className={style.recipe}
            key={recipe.recipeId}
            id={style.recipeChild}
            onClick={() => navigate('/recipes/' + recipe.recipeId)}
          >
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
        ))}
      </div>
    </>
  );
};

export default GetRecipes;
