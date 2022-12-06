import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import RecipeService from '../../service/recipeService';
import UserService from '../../service/userService';
import { Ingredient, Instruction, Recipe, User } from '../../types';
import style from './CreateRecipe.module.css';

const CreateRecipe: React.FC = () => {
  const [IngredientsList, setIngredientsList] = useState<Ingredient[]>([]);
  const [InstructionsList, setInstructionsList] = useState<Instruction[]>([]);
  const [name, setname] = useState<string>('');
  const [description, setdescription] = useState<string>('');
  const [picture, setpicture] = useState<string>('');
  const [time, settime] = useState<number>(0);
  const [portion, setportion] = useState<number>(0);
  const [errorMessages, seterrorMessages] = useState<string[]>([]);

  const navigate = useNavigate();

  const validateForm = () => {
    let ok = true;
    let error: string[] = [];
    if (InstructionsList.length <= 0) {
      error.push('Add at least 1 instruction');
      ok = false;
    }
    if (IngredientsList.length <= 0) {
      error.push('Add at least 1 ingredient');
      ok = false;
    }
    seterrorMessages(error);
    return ok;
  };

  const submitForm = async (e) => {
    const token = sessionStorage.getItem('user');
    const activeUser: User = JSON.parse(token || '{}');
    const id = activeUser.userId;
    const user: User = (await UserService.getUser(id)).data;
    e.preventDefault();
    if (validateForm()) {
      const recipe: Recipe = {
        recipeId: crypto.randomUUID.toString(),
        name: name,
        description: description,
        pictureURL: picture,
        time: time,
        portionNum: portion,
        author: user.userId,
        ingredients: IngredientsList,
        instructions: InstructionsList
      };
      const res = await RecipeService.createRecipe(recipe);
      //TODO change path to recipe list
      navigate('/');
    }
  };

  return (
    <div className={style.container}>
      <h2>create recipe</h2>
      <p>{errorMessages && errorMessages.map((error) => <p>{error}</p>)}</p>
      <form className={style.formContainer} onSubmit={submitForm} id="createRecipeForm">
        <div className={style.formChild}>
          <label htmlFor="name">
            recipe name
            <input
              className={style.createRecipeInput}
              type="text"
              id="name"
              name="name"
              onChange={(e) => setname(e.target.value)}
              required
              min="5"
              max="50"
            ></input>
          </label>
        </div>
        <div className={style.formChild}>
          <label htmlFor="description">
            description
            <input
              className={style.createRecipeInput}
              type="text"
              id="description"
              name="description"
              onChange={(e) => setdescription(e.target.value)}
              required
              min="5"
            ></input>
          </label>
        </div>
        <div className={style.formChild}>
          <label htmlFor="pictureURL">
            picture url
            <input
              className={style.createRecipeInput}
              type="text"
              id="pictureURL"
              name="pictureURL"
              onChange={(e) => setpicture(e.target.value)}
              required
              pattern="http(s*)://(.*)[.png, .webp, .jpeg]"
            ></input>
          </label>
        </div>
        <div className={style.formChild}>
          <label htmlFor="time">
            time (min)
            <input
              className={style.createRecipeInput}
              type="number"
              id="time"
              name="time"
              onChange={(e) => settime(Number.parseInt(e.target.value))}
              required
              min="1"
            ></input>
          </label>
        </div>
        <div className={style.formChild}>
          <label htmlFor="portionNum">
            portion size
            <input
              className={style.createRecipeInput}
              type="number"
              id="portionNum"
              name="portionNum"
              onChange={(e) => setportion(Number.parseInt(e.target.value))}
              required
              min="1"
            ></input>
          </label>
        </div>
        <h4>ingredients</h4>
        <ul>
          {IngredientsList &&
            IngredientsList.map((ingredient, index) => (
              <li>
                <p>&#x2022;</p>
                <label>
                  Name
                  <input
                    className={style.createRecipeInput}
                    type="text"
                    value={ingredient.name}
                    onChange={(e) => {
                      const newIngredient: Ingredient = { ...ingredient, name: e.target.value };
                      const newIngredientList = IngredientsList;
                      newIngredientList[index] = newIngredient;
                      setIngredientsList([...newIngredientList]);
                    }}
                    min="1"
                    required
                  ></input>
                </label>
                <label>
                  amount
                  <input
                    className={style.createRecipeInput}
                    type="number"
                    value={ingredient.amount}
                    onChange={(e) => {
                      const newIngredient: Ingredient = {
                        ...ingredient,
                        amount: Number.parseInt(e.target.value)
                      };
                      const newIngredientList = IngredientsList;
                      newIngredientList[index] = newIngredient;
                      setIngredientsList([...newIngredientList]);
                    }}
                    min="1"
                    required
                  ></input>
                </label>
                <label>
                  unit
                  <input
                    className={style.createRecipeInput}
                    type="text"
                    value={ingredient.unit}
                    onChange={(e) => {
                      const newIngredient: Ingredient = { ...ingredient, unit: e.target.value };
                      const newIngredientList = IngredientsList;
                      newIngredientList[index] = newIngredient;
                      setIngredientsList([...newIngredientList]);
                    }}
                  ></input>
                </label>
                <div className={style.btnAddContainer}>
                  <button
                    className={style.btnAdd}
                    onClick={(e) => {
                      const newIngredientList = IngredientsList.filter((_, i) => i != index);
                      setIngredientsList([...newIngredientList]);
                    }}
                  >
                    X
                  </button>
                </div>
              </li>
            ))}
          <div className={style.btnAddContainer}>
            <button
              className={style.btnAdd}
              onClick={() =>
                setIngredientsList([...IngredientsList, { name: '', amount: 0, unit: '' }])
              }
            >
              Add
            </button>
          </div>
        </ul>
        <h4>instructions</h4>
        <ol>
          {InstructionsList &&
            InstructionsList.map((instruction, index) => (
              <li>
                <p>{index + 1}</p>
                <textarea
                  value={instruction.description}
                  onChange={(e) => {
                    const newInstruction: Instruction = {
                      step: index,
                      description: e.target.value
                    };
                    const newInstructionsList = InstructionsList;
                    newInstructionsList[index] = newInstruction;
                    setInstructionsList([...newInstructionsList]);
                  }}
                  minLength={5}
                  required
                ></textarea>
                <button
                  onClick={(e) => {
                    const newInstructionsList = InstructionsList.filter((_, i) => i != index).map(
                      (item, i) => ({
                        ...item,
                        step: i
                      })
                    );

                    setInstructionsList([...newInstructionsList]);
                  }}
                >
                  X
                </button>
              </li>
            ))}
          <div className={style.btnAddContainer}>
            <button
              className={style.btnAdd}
              onClick={() =>
                setInstructionsList([...InstructionsList, { description: '', step: 0 }])
              }
            >
              Add
            </button>
          </div>
        </ol>
        <button type="submit" form="createRecipeForm">
          Create
        </button>
      </form>
    </div>
  );
};

export default CreateRecipe;
