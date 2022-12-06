import React, { useEffect, useState } from 'react';
import RecipeService from '../../service/recipeService';
import UserService from '../../service/userService';
import { BambooSession, Recipe, User } from '../../types';
import style from './CreateBamboo.module.css';
import '@mobiscroll/react/dist/css/mobiscroll.min.css';
import { Datepicker, localeEn, setOptions } from '@mobiscroll/react';
import { useNavigate } from 'react-router-dom';
import BambooService from '../../service/bambooService';
import moment from 'moment';

setOptions({
  theme: 'ios',
  themeVariant: 'light'
});

const CreateBamboo: React.FC = () => {
  const [user, setUser] = useState<User>();
  const [recipes, setRecipes] = useState<Recipe[]>([]);
  const [compIsShown, setCompIsShown] = useState(false);
  const [value, setValue] = useState<string>('');
  
  const [recipe, setRecipe] = useState<string>("");
  const [address, setAddress] = useState<string>('');
  const [description, setDescription] = useState<string>('');
  const [slots, setSlots] = useState<number>(0);
  const [date, setDate] = useState<any>();
  const [errorMessages, setErrorMessages] = useState<string[]>([]);

  const navigate = useNavigate();

  const formatDate = (date: string) => {
    var formatDate  = moment(date).format();
    formatDate = formatDate.slice(0, formatDate.length - 6);
    setDate(formatDate);
  };

  const validateForm = () => {
    let ok = true;
    let error: string[] = [];
    if (address.length, description.length, slots  === 0) {
      error.push('fill out all fields');
      ok = false;
    }
    else if (slots <= 0) {
      error.push('slots must be greater than 0');
      ok = false;
    }
    else if (date === null) {
      error.push('pick a date and time');
      ok = false;
    }
    else if (recipe === undefined || recipe === null) {
      error.push('pick a recipe');
      ok = false;
    }
    setErrorMessages(error);
    return ok;
  };

  const submitForm = async (e) => {
    e.preventDefault();
    var id = crypto.randomUUID();
    if (validateForm() && user && recipe) {
      const bamboo: BambooSession = {
        sessionId: id.toString(),
        host: user.userId,
        address: address,
        recipe: recipe,
        description: description,
        dateTime: date,
        slotsNumber: slots,
        attendees: []
      };
      const res = await BambooService.createBambooSession(bamboo);
      if (res.status === 200) {
        navigate('/bamboosessions');
      }
    }
  };


  const handleSelectChange = async(e) => {
    const recipeId: string = e.target.value;
    const recipe: Recipe = (await RecipeService.getRecipe(recipeId)).data;
    setRecipe(recipe.recipeId);
  };

  const getUserFromToken = async () => {
    const token = sessionStorage.getItem('user');
    const activeUser: User = JSON.parse(token || '{}');
    const id = activeUser.userId;
    const user: User = (await UserService.getUser(id)).data;
    setUser(user);
    return user;
  };

  const getRecipes = async () => {
    const recipes: Recipe[] = (await RecipeService.getRecipes()).data;
    setRecipes(recipes);
  };

  useEffect(() => {
    getUserFromToken();
    getRecipes();
  }, []);

  return (
    <>
      <div className={style.container}>
        <h1 className={style.createBambooTitle}> Create bamboo session</h1>

       <div className={style.createBambooPage}>

        <form className={style.createBambooFormContainer} onSubmit={submitForm} id="createBamboo">

          <div className={style.nameContainer} id={style.formChild}>
            <label className={style.nameLabel}>Hosted by: {user?.firstName + " " + user?.lastName}</label>
          </div>

          <div className={style.pickRecipe} id={style.formChild}>
            <label>Pick a recipe</label>
            <select className={style.recipeSelect} onChange={handleSelectChange}>
                  {recipes.map((recipe: Recipe) => (
                    <option key={recipe.recipeId} value={recipe.recipeId}>
                      {recipe.name}
                    </option>
                  ))}
              </select>
          </div>
          
          <div className={style.inputFields}>
          <input
              id={style.formChild}
                type="text"
                name="address"
                className={style.createBambooInput}
                placeholder="address"
                onChange={(e) => setAddress(e.target.value)}
                required
                min="5"
              />
            <textarea
            id={style.formChild}
              name="description"
              className={style.createBambooInput}
              placeholder="description"
              required
              onChange={(e) => setDescription(e.target.value)}
            />
            <input
            id={style.formChild}
              type="number"
              name="slots"
              className={style.createBambooInput}
              placeholder="slots"
              required
              min="1"
              onChange={(e) => setSlots(parseInt(e.target.value))}
            />  
          </div>

          <div className={style.dateContainer} id={style.formChild}>
            <div className={style.chooseDate}>
              <label>Pick date and time :</label>
              <button onClick={() => setCompIsShown(!compIsShown)} id={style.dateAndTimeBtn}>click me</button>
            </div>
      {compIsShown && 
      <div><Datepicker
                onChange={(event) => formatDate(event.value)}
                locale={localeEn}
                controls={['calendar', 'time']}
                display="inline"
                headerText="You selected {value}"
                required
              /></div>}
          </div>

        </form>

        </div> 

        <div className={style.btnContainer}>
          <button className={style.createBambooBtn} form="createBamboo" type='submit' onClick={submitForm}>
              Create Bamboo Session
          </button>
        </div>
        
      </div>
    </>
  );
};

export default CreateBamboo;

