import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import BambooService from '../../service/bambooService';
import RecipeService from '../../service/recipeService';
import { BambooSession, Recipe } from '../../types';
import style from './GetBamboos.module.css';

const GetBamboos: React.FC = () => {
  const [bamboos, setBamboos] = useState<BambooSession[]>([]);
  const [recipe, setRecipe] = useState<Recipe>();

  const navigate = useNavigate();

  const getData = async () => {
    const response = await BambooService.getBambooSessions();
    const data = response.data;
    setBamboos(data);
    data.forEach(element => {
      getRecipe(element.recipe);
    });
  };

  const getRecipe = async (id: string) => {
    const response = await RecipeService.getRecipe(id);
    const data = response.data;
    setRecipe(data);
  };

  useEffect(() => {
    getData();
  }, []);

  return (
    <>
    {console.log(bamboos)}  
      <div className={style.pageContent}>
        <h2 style={{ color: '#DCBEA8' }}>explore</h2>
        {bamboos.map((bamboo: BambooSession) => (
          <div
            className={style.recipe}
            key={bamboo.sessionId + bamboo.host}
            id={style.recipeChild}
            onClick={() => navigate('/bamboosession/' + bamboo.sessionId)}
          >
            <div className={style.imgWrapper}>
              <img src={recipe?.pictureURL} alt="" />
            </div>
            <div className={style.recipeContent} id={style.recipeChild}>
              <h2>{recipe?.name}</h2>
              <div className={style.prepTime}>
                <p>created by: </p>
                <p>{bamboo.description}</p>
              </div>
            </div>
          </div>
        ))}
      </div>
    </>
  );
};

export default GetBamboos;
