import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import RecipeService from '../../service/recipeService';
import { Ingredient, Instruction, Recipe, User } from '../../types';
import style from './UpdateRecipe.module.css';
import UserService from '../../service/userService';

const UpdateRecipe: React.FC = () => {
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
    if ((name.length, description.length, picture.length, time, portion === 0)) {
      error.push('All fields are required');
      ok = false;
    }
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

  const editRecipe = async (e) => {
    e.preventDefault();
    const token = sessionStorage.getItem('user');
    const activeUser: User = JSON.parse(token || '{}');
    const id = activeUser.userId;
    const user: User = (await UserService.getUser(id)).data;
    const recipe: Recipe = (await RecipeService.getRecipe(id)).data;
    if (recipe.author === user.userId && validateForm()) {
      recipe.name = name;
      recipe.description = description;
      recipe.pictureURL = picture;
      recipe.time = time;
      recipe.portionNum = portion;
      recipe.ingredients = IngredientsList;
      recipe.instructions = InstructionsList;

      // update
      const res = await RecipeService.updateRecipe(recipe);
      if (res.status == 200) {
        alert('Information updated');
        navigate('/recipes/' + recipe.recipeId);
      } else {
        alert('Something went wrong. Try again');
      }
    }
  };

  return (
    <>
      <div className={style.container}>
        <div className={style.editPage}>
          <h1>
            <span style={{ color: '#A8ACDC' }}>Edit</span> recipe{' '}
          </h1>
          <form className={style.editRecipeForm}>
            <input
              className={style.updateRecipeInput}
              type="text"
              id="name"
              name="name"
              placeholder="recipe name"
              onChange={(e) => setname(e.target.value)}
              required
              min="5"
              max="50"
            />
            <input
              className={style.updateRecipeInput}
              type="text"
              id="description"
              name="description"
              placeholder="description"
              onChange={(e) => setdescription(e.target.value)}
              required
              min="5"
              max="50"
            />
            <input
              className={style.updateRecipeInput}
              type="text"
              id="pictureURL"
              name="pictureURL"
              placeholder="picture URL"
              onChange={(e) => setpicture(e.target.value)}
              required
              pattern="http(s*)://(.*)[.png, .webp, .jpeg]"
            />
            <input
              className={style.updateRecipeInput}
              type="number"
              id="time"
              name="time"
              placeholder="time to prepare (min)"
              onChange={(e) => settime(Number.parseInt(e.target.value))}
              required
              min="1"
            />
            <input
              className={style.updateRecipeInput}
              type="number"
              id="portionNum"
              name="portionNum"
              placeholder="number of portions"
              onChange={(e) => setportion(Number.parseInt(e.target.value))}
              required
              min="1"
            />

            <h4>ingredients</h4>

            <ul className={style.ingredientList}>
              {IngredientsList &&
                IngredientsList.map((ingredient, index) => (
                  <div className={style.ingredientListContainer}>
                    <li>
                      <p>&#x2022;</p>
                      <input
                        className={style.ingredientInput}
                        type="text"
                        value={ingredient.name}
                        placeholder="ingredient name"
                        onChange={(e) => {
                          const newIngredient: Ingredient = { ...ingredient, name: e.target.value };
                          const newIngredientList = IngredientsList;
                          newIngredientList[index] = newIngredient;
                          setIngredientsList([...newIngredientList]);
                        }}
                        min="1"
                        required
                      />
                      <input
                        className={style.ingredientInput}
                        type="number"
                        value={ingredient.amount}
                        placeholder="amount"
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
                      />
                      <input
                        className={style.ingredientInput}
                        type="text"
                        value={ingredient.unit}
                        placeholder="unit"
                        onChange={(e) => {
                          const newIngredient: Ingredient = { ...ingredient, unit: e.target.value };
                          const newIngredientList = IngredientsList;
                          newIngredientList[index] = newIngredient;
                          setIngredientsList([...newIngredientList]);
                        }}
                      />
                      <button
                        id={style.btnRemove}
                        onClick={(e) => {
                          const newIngredientList = IngredientsList.filter((_, i) => i != index);
                          setIngredientsList([...newIngredientList]);
                        }}
                      >
                        remove
                      </button>
                    </li>
                  </div>
                ))}
              <div className={style.btnAddContainer}>
                <button
                  id={style.btnAdd}
                  className={style.btnAdd}
                  onClick={() =>
                    setIngredientsList([...IngredientsList, { name: '', amount: 0, unit: '' }])
                  }
                >
                  Add ingredient
                </button>
              </div>
            </ul>

            <h4>instructions</h4>

            <ul className={style.instructionLI}>
              {InstructionsList &&
                InstructionsList.map((instruction, index) => (
                  <div className={style.instructionListContainer}>
                    <li>
                      <p>{index + 1}</p>
                      <textarea
                        value={instruction.description}
                        id={style.descriptionTA}
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
                      />
                      <button
                        id={style.btnRemove}
                        onClick={(e) => {
                          const newInstructionsList = InstructionsList.filter(
                            (_, i) => i != index
                          ).map((item, i) => ({
                            ...item,
                            step: i
                          }));

                          setInstructionsList([...newInstructionsList]);
                        }}
                      >
                        remove
                      </button>
                    </li>
                  </div>
                ))}
              <div className={style.btnAddContainer}>
                <button
                  className={style.btnAdd}
                  id={style.btnAdd}
                  onClick={() =>
                    setInstructionsList([...InstructionsList, { description: '', step: 0 }])
                  }
                >
                  Add instruction
                </button>
              </div>
            </ul>
          </form>

          <button form="createRecipeForm" className={style.editRecipeBtn} onClick={editRecipe}>
            Edit recipe
          </button>
        </div>
      </div>
    </>
  );
};

export default UpdateRecipe;
