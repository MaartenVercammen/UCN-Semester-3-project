import axios from 'axios';
import React, { useEffect, useState } from 'react';
import RecipeService from '../../service/recipeService';
import { Recipe } from '../../types';

const GetRecipe: React.FC = () => {

    const [recipe, setRecipe] = useState<any>([]);

    const getData = async () => {
        const response = await RecipeService.getRecipes();
        const data = response.data;

        data.forEach((recipe: Recipe) => {
            //setRecipes((recipes: any) => [...recipes, recipe]);
        });
    }

    const getSingleData = async () => {
        const response = await RecipeService.getRecipe(window.location.pathname.split('/')[2]);
        const data = response.data;
        setRecipe(data);
    }

    useEffect(() => {
        getSingleData();
    }, []);



    return (
        <div>
            <img src={recipe.pictureURL} alt="" />
            <div>
                <h2>{recipe.name}</h2>
                <h4>{recipe.time}</h4>
                <h4>{recipe.description}</h4>
            </div>
        </div>
    );
};

export default GetRecipe;
