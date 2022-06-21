import {
    createRecipe,
    updateRecipe,
    removeRecipe,
    loadRecipesInProgress,
    loadRecipesSuccess,
    loadRecipesFailure,
    loadIngredientsInProgress,
    loadIngredientsSuccess,
    loadIngredientsFailure,
    createIngredient
} from './actions';

export const loadRecipes = () => async (dispatch) => {
    try {
        dispatch(loadRecipesInProgress());
        const response = await fetch('http://localhost:5122/api/recipes/GetAll');
        const recipes = await response.json();
    
        dispatch(loadRecipesSuccess(recipes));
    } catch (e) {
        dispatch(loadRecipesFailure());
        dispatch(displayAlert(e));
    }
}

export const loadAllIngredients = () => async (dispatch) => {
    try {
        dispatch(loadIngredientsInProgress());
        const response = await fetch('http://localhost:5122/api/ingredients/GetAll');
        const ingredients = await response.json();
    
        dispatch(loadIngredientsSuccess(ingredients));
    } catch (e) {
        dispatch(loadIngredientsFailure());
        dispatch(displayAlert(e));
    }
}

export const addRecipeRequest = recipe => async dispatch => {
    try {
        const body = JSON.stringify(recipe);
        const response = await fetch('http://localhost:5122/api/recipes/AddRecipe', {
            headers: {
                'Content-Type': 'application/json',
            },
            method: 'post',
            body,
        });
        let createdRecipe = await response.json();
        if(recipe?.imageData) {
            recipe.id = createdRecipe.id;
            createdRecipe = await addImageRequest(recipe);
        }
        dispatch(createRecipe(createdRecipe));
    } catch (e) {
        dispatch(displayAlert(e));
    }
}

const addImageRequest = async (recipe) => {
    let formData = new FormData();
    formData.append('id', recipe.id);
    formData.append('imageData', recipe.imageData);
    const response = await fetch('http://localhost:5122/api/recipes/AddImageToRecipe', {
        method: 'post',
        body: formData,
    });
    const updatedRecipe = await response.json();
    return updatedRecipe;
}

export const updateRecipeRequest = recipe => async dispatch => {
    try {
        const body = JSON.stringify(recipe);      
        const response = await fetch(`http://localhost:5122/api/recipes/UpdateRecipe/${recipe.id}`, {
            headers: {
                'Content-Type': 'application/json',
            },
            method: 'post',
            body,
        });
        let updatedRecipe = await response.json();
        if(recipe?.imageData) {
            updatedRecipe = await addImageRequest(recipe);
        }
        dispatch(updateRecipe(updatedRecipe));
    } catch (e) {
        dispatch(displayAlert(e));
    }
}

export const searchRecipeRequest = text => async dispatch => {
    try {
        dispatch(loadRecipesInProgress());
        let response =  {};
        if(text && text.length > 0) {
            response = await fetch(`http://localhost:5122/api/recipes/SearchRecipes/${text}`);
        }
        else {
            response = await fetch('http://localhost:5122/api/recipes/GetAll');
        }
        const recipes = await response.json();
    
        dispatch(loadRecipesSuccess(recipes));
    } catch (e) {
        dispatch(loadRecipesFailure());
        dispatch(displayAlert(e));
    }
}


export const removeRecipeRequest = id => async dispatch => {
    try {
        const response = await fetch(`http://localhost:5122/api/recipes/DeleteRecipe/${id}`, {
            method: 'delete'
        });
        const succeeded = await response.ok;
        dispatch(removeRecipe(id));
    } catch (e) {
        dispatch(displayAlert(e));
    }
}

export const addIngredientRequest = (ingredient) => async dispatch => {
    try {
        const body = JSON.stringify(ingredient);
        const response = await fetch('http://localhost:5122/api/ingredients/AddIngredient', {
            headers: {
                'Content-Type': 'application/json',
            },
            method: 'post',
            body
        });
        const newIngredient = await response.json();
        dispatch(createIngredient(newIngredient));
    } catch (e) {
        dispatch(displayAlert(e));
    }
}

export const displayAlert = text => () => {
    alert(text);
};