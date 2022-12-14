import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import RecipeService from '../../service/recipeService';
import UserService from '../../service/userService';
import { User, Ingredient, Instruction, Recipe, SwipedRecipe } from '../../types';
import style from './GetRecipe.module.css';

const GetRecipe: React.FC = () => {
  const [recipe, setRecipe] = useState<any>([]);
  const [author, setAuthor] = useState<any>([]);
  const [ingredients, setIngredients] = useState<any>([]);
  const [instructions, setInstructions] = useState<any>([]);
  const [user, setUser] = useState<User>();
  const [liked, setLiked] = useState<SwipedRecipe[]>([]);

  const navigate = useNavigate();

  const getData = async () => {
    const response = await RecipeService.getRecipe(window.location.pathname.split('/')[2]);
    const authorResponse = await UserService.getUser(response.data.author);
    const data = response.data;
    setRecipe(data);
    setIngredients(data.ingredients);
    setInstructions(data.instructions);
    setAuthor(authorResponse.data);
    await getUser();
  };

  const getLiked = async (userId) => {
    const response = await RecipeService.getSwiped(userId);
    const data = response.data;
    setLiked(data);
  };

  const deleteRecipe = async () => {
    if (confirm('Are you sure you want to delete this recipe?')) {
      await RecipeService.deleteRecipe(recipe.recipeId);
      navigate('/recipes');
    }
  };

  const isLiked = () => {
    let isLiked = false;
    liked.forEach((element) => {
      if (element.recipeId === recipe.recipeId) {
        isLiked = true;
      }
    });
    return isLiked;
  };

  const editRecipe = async () => {
    navigate('/recipes/' + recipe.recipeId + '/edit');
  };

  const getUser = async () => {
    const token = sessionStorage.getItem('user');
    const activeUser: User = JSON.parse(token || '{}');
    const id = activeUser.userId;
    const user: User = (await UserService.getUser(id)).data;
    setUser(user);
    getLiked(user.userId);
  };

  const likeRecipe = async () => {
    if (user) {
      const swipe: SwipedRecipe = {
        authorId: user.userId,
        recipeId: recipe.recipeId,
        isLiked: true
      };
      await RecipeService.swipeRecipe(swipe);
      alert('Recipe liked!');
      window.location.reload();
    }
  };

  const dislikeRecipe = async () => {
    if (user) {
      const swipe: SwipedRecipe = {
        authorId: user.userId,
        recipeId: recipe.recipeId,
        isLiked: false
      };
      await RecipeService.swipeRecipe(swipe);
      alert('Recipe disliked!');
      window.location.reload();
    }
  };

  useEffect(() => {
    getData();
  }, []);

  return (
    <>
    <div className={style.recipeContent}>
      <div className={style.recipeImg}>
        <img src={recipe.pictureURL} alt="" />
      </div>
      <h1>{recipe.name}</h1>
      <div className={style.container}>
        <h4>Preparation time: {recipe.time}min</h4>
        <p>{recipe.description}</p>
        <h4>author: {author.firstName + " " + author.lastName}</h4>
        <h4>Number of portions: {recipe.portionNum}</h4>
        <h3>Ingredients:</h3>
        {ingredients.map((ingredient: Ingredient) => (
          <div key={ingredient.name}>
            <div className={style.ingredientContainer}>
              <p className={style.ingredientChild}>{ingredient.name}:</p>
              <p className={style.ingredientChild}>
                {ingredient.amount}
                {ingredient.unit}
              </p>
            </div>
          </div>
        ))}
        {instructions.map((instruction: Instruction) => (
          <div className={style.instruction} key={instruction.step}>
            <h4>Step {instruction.step}:</h4>
            <p>{instruction.description}</p>
          </div>
        ))}
      </div>
      <p>{isLiked()}</p>
      {recipe.author === user?.userId ? (
      <div className={style.buttonContainer}>
        <button onClick={() => editRecipe()} className={style.editBtn}>edit</button>
        <button onClick={() => deleteRecipe()} className={style.deleteBtn}>delete</button>
      </div>)
      : (
      <div className={style.buttonContainer}>
        {isLiked() ?
        <button onClick={() => dislikeRecipe()} className={style.editBtn}>dislike</button> :
        <button onClick={() => likeRecipe()} className={style.editBtn}>like</button>
      } </div> )}
    </div>
    </>
  );
};

export default GetRecipe;
