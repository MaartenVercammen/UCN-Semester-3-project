import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import BambooService from '../../service/bambooService';
import RecipeService from '../../service/recipeService';
import { BambooSession, Recipe } from '../../types';
import style from './GetBamboos.module.css';

const GetBamboos: React.FC = () => {
  const [bambooRecipe, setBambooRecipe] = useState<{bambooSession: BambooSession, recipe: Recipe}[]>([]);

  const navigate = useNavigate();

  const getData = async () => {
    const responseBamboo = await BambooService.getBambooSessions();
    const dataBamboo = responseBamboo.data;
    const list = await dataBamboo.map(async(bamboo: BambooSession) => {
      const recipe: Recipe = await getRecipe(bamboo.recipe);
      return {bambooSession: bamboo, recipe: recipe};
    });
    setBambooRecipe(list? await Promise.all(list) : []);
  };

  const getRecipe = async (id: string) => {
    const response = await RecipeService.getRecipe(id);
    const data = response.data;
    return data;
  };

  useEffect(() => {
    getData();
  }, []);

  return (
    <>
      <div className={style.pageContent}>
        <h2 style={{ color: '#A8ACDC' }}>explore <span style={{ color: '#444444' }}>bamboo sessions</span></h2>
        {bambooRecipe.map(({bambooSession, recipe} ) => (
          <div
            className={style.recipe}
            key={bambooSession.sessionId + bambooSession.host}
            id={style.recipeChild}
            onClick={() => navigate('/bamboosessions/' + bambooSession.sessionId)}
          >
            <div className={style.imgWrapper}>
              <img src={recipe.pictureURL} alt="" />
            </div>
            <div className={style.recipeContent} id={style.recipeChild}>
              <h2>{recipe.name}</h2>
              <div className={style.prepTime}>
                <p>{bambooSession.description}</p>
              </div>
            </div>
          </div>
        ))}
      </div>
    </>
  );
};

export default GetBamboos;