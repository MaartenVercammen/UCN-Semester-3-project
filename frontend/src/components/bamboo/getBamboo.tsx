import { join } from 'path';
import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import BambooService from '../../service/bambooService';
import RecipeService from '../../service/recipeService';
import UserService from '../../service/userService';
import { BambooSession, Recipe, User } from '../../types';
import style from '../recipe/GetRecipe.module.css';

const GetBamboo: React.FC = () => {
  const [recipe, setRecipe] = useState<Recipe>();
  const [author, setAuthor] = useState<User>();
  const [bamboo, setBamboo] = useState<BambooSession>();

  const navigation = useNavigate();

  const getData = async () => {
    const response = await BambooService.getBambooSession(window.location.pathname.split('/')[2]);
    const data = response.data;
    setBamboo(data);
    getRecipe(data.recipe);
  };

  const getRecipe = async (id: string) => {
    const response = await RecipeService.getRecipe(id);
    const data = response.data;
    setRecipe(data);
    getAuthor(data.author);
  };

  const getAuthor = async (id: string) => {
    const response = await UserService.getUser(id);
    const data = response.data;
    setAuthor(data);
  };

  const deleteBamboo = async () => {
    if (confirm('Are you sure you want to delete this recipe?')) {
      //await BambooService.deleteBambooSession(bamboo?.sessionId);
      navigation('/app');
    }
  };

  const join = async () => {
    alert('you would have joined the bamboo session, if this feature was implemented')
  };

  useEffect(() => {
    getData();
  }, []);

  return (
    <div className={style.recipeContent}>
      <div className={style.recipeImg}>
        <img src={recipe?.pictureURL} alt="" />
      </div>
      <h6>recipe author: {author?.firstName + " " + author?.lastName}</h6> 
      <h1>Bamboo Session: {recipe?.name}</h1>
      <div className={style.container}>
        <h4>organized by: {author?.firstName + " " + author?.lastName}</h4>
        <h5>description: </h5>
        <p>{bamboo?.description}</p>
        <h5>Where?</h5>
        <p>{bamboo?.address}</p>
        <h5>total slots: {bamboo?.slotsNumber}</h5>
        <h5>available slots: yes</h5> {/* TODO: implement this*/}
      </div>
      <div className={style.buttonContainer}>
        <button onClick={(e) => join()} className={style.joinBtn}>join</button>
        <button onClick={(e) => deleteBamboo()} className={style.deleteBtn}>delete</button>
      </div>
    </div>
  );
};

export default GetBamboo;
