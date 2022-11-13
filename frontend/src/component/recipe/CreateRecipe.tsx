import React, { useState } from 'react'
import RecipeService from '../../service/recipeService'
import { Ingredient, Instruction, Recipe } from '../../types'

const CreateRecipe: React.FC = () => {
  const [IngredientsList, setIngredientsList] = useState<Ingredient[]>([])
  const [InstructionsList, setInstructionsList] = useState<Instruction[]>([])
  const [name, setname] = useState<string>("")
  const [description, setdescription] = useState<string>("")
  const [picture, setpicture] = useState<string>("")
  const [time, settime] = useState<number>(0)
  const [portion, setportion] = useState<number>(0)
  const [message, setmessage] = useState<string>()

  const submitForm = async (e) => {
    const recipe: Recipe = {recipeId: "00000000-0000-0000-0000-000000000000",name: name, description: description, pictureURL: picture, time: time, portionNum: portion, author: "00000000-0000-0000-0000-000000000000", ingredients: IngredientsList, instructions: InstructionsList}
    const res = await RecipeService.createRecipe(recipe);
    console.log(res.data)
  }

  return (
    <div >
      <p>{message}</p>
      <label htmlFor='name'>Name</label>
      <input type="text" id='name' name='name' placeholder='Bananabread' onChange={e => setname(e.target.value)}></input>
      <label htmlFor='description'>Description</label>
      <input type="text" id='description' name='description' placeholder='Bananabread is cool' onChange={e => setdescription(e.target.value)}></input>
      <label htmlFor='pictureURL'>Picture</label>
      <input type="text" id='pictureURL' name='pictureURL' placeholder='http://google.cum' onChange={e => setpicture(e.target.value)}></input>
      <label htmlFor='time'>Time</label>
      <input type="number" id='time' name='time' placeholder='60' onChange={e => settime(Number.parseInt(e.target.value))}></input>
      <label htmlFor='portionNum'>PortionSize</label>
      <input type="number" id='portionNum' name='portionNum' placeholder='5' onChange={e => setportion(Number.parseInt(e.target.value))}></input>
      <label>Ingredients</label>
      <ul>
        {IngredientsList && IngredientsList.map((ingredient, index) => (
          <li>
            <label>Name</label>
            <input type="text" value={ingredient.name} onChange={e => {
              const newIngredient: Ingredient = {...ingredient, name: e.target.value}
              const newIngredientList = IngredientsList;
              newIngredientList[index] = newIngredient;
              setIngredientsList([...newIngredientList]);
            }}></input>
            <label>amount</label>
            <input type="text" value={ingredient.amount} onChange={e => {
              const newIngredient: Ingredient = {...ingredient, amount: Number.parseInt(e.target.value)}
              const newIngredientList = IngredientsList;
              newIngredientList[index] = newIngredient;
              setIngredientsList([...newIngredientList]);
            }}></input>
            <label>unit</label>
            <input type="text" value={ingredient.unit} onChange={e => {
              const newIngredient: Ingredient = {...ingredient, unit: e.target.value}
              const newIngredientList = IngredientsList;
              newIngredientList[index] = newIngredient;
              setIngredientsList([...newIngredientList]);
            }}></input>
            <button onClick={e => {
              const newIngredientList = IngredientsList.filter((_, i) => i != index);
              setIngredientsList([...newIngredientList]);
            }}>X</button>
          </li>
        ))}
        <button onClick={() => setIngredientsList([...IngredientsList, {name: "", amount: 0, unit: ""}])}>Add</button>
      </ul>
      <label>Instructions</label>
      <ol>
        {InstructionsList && InstructionsList.map((instruction, index) => (
          <li>
            <input type="text" value={instruction.description} onChange={e => {
              const newInstruction: Instruction = {step: index, description: e.target.value}
              const newInstructionsList = InstructionsList;
              newInstructionsList[index] = newInstruction;
              setInstructionsList([...newInstructionsList]);
            }}></input>
            <button onClick={e => {
              const newInstructionsList = InstructionsList.filter((_, i) => i != index).map((item, i) => ({
                ...item, step: i
              }));

              setInstructionsList([...newInstructionsList]);
            }}>X</button>
          </li>
        ))}
        <button onClick={() => setInstructionsList([...InstructionsList, {description: "", step: 0}])}>Add</button>
        </ol>
      <button onClick={submitForm}>Create</button>
    </div>
  )
}

export default CreateRecipe
