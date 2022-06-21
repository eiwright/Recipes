import { Recipe } from '../types/Recipe'
import { Ingredient } from '../types/Ingredient'

export const CREATE_RECIPE = 'CREATE_RECIPE';
export const createRecipe = (recipe : Recipe) => ({
    type: CREATE_RECIPE,
    payload: { recipe },
});

export const REMOVE_RECIPE = 'REMOVE_RECIPE';
export const removeRecipe = (id : number) => ({
    type: REMOVE_RECIPE,
    payload: { id },
});

export const UPDATE_RECIPE  = 'UPDATE_RECIPE ';
export const updateRecipe = (recipe : Recipe) => ({
    type: UPDATE_RECIPE,
    payload: { recipe },
});

export const LOAD_RECIPES_IN_PROGRESS = 'LOAD_RECIPES_IN_PROGRESS';
export const loadRecipesInProgress = () => ({
    type: LOAD_RECIPES_IN_PROGRESS,
});

export const LOAD_RECIPES_SUCCESS = 'LOAD_RECIPES_SUCCESS';
export const loadRecipesSuccess = (recipes : Recipe[]) => ({
    type: LOAD_RECIPES_SUCCESS,
    payload: { recipes },
});

export const LOAD_RECIPES_FAILURE = 'LOAD_RECIPES_FAILURE';
export const loadRecipesFailure = () => ({
    type: LOAD_RECIPES_FAILURE,
});

export const CREATE_INGREDIENT = 'CREATE_INGREDIENT';
export const createIngredient = (ingredient : Ingredient) => ({
    type: CREATE_INGREDIENT,
    payload: { ingredient },
});

export const UPDATE_INGREDIENT = 'UPDATE_INGREDIENT';
export const updateIngredient = (ingredient : Ingredient) => ({
    type: UPDATE_INGREDIENT,
    payload: { ingredient },
});

export const REMOVE_INGREDIENT = 'REMOVE_INGREDIENT';
export const removeIngredient = (ingredient : Ingredient) => ({
    type: REMOVE_INGREDIENT,
    payload: { ingredient },
});

export const LOAD_INGREDIENTS_IN_PROGRESS = 'LOAD_INGREDIENTS_IN_PROGRESS';
export const loadIngredientsInProgress = () => ({
    type: LOAD_INGREDIENTS_IN_PROGRESS,
});

export const LOAD_INGREDIENTS_SUCCESS = 'LOAD_INGREDIENTS_SUCCESS';
export const loadIngredientsSuccess = (allIngredients : Ingredient[]) => ({
    type: LOAD_INGREDIENTS_SUCCESS,
    payload: { allIngredients },
});

export const LOAD_INGREDIENTS_FAILURE = 'LOAD_INGREDIENTS_FAILURE';
export const loadIngredientsFailure = () => ({
    type: LOAD_INGREDIENTS_FAILURE,
});