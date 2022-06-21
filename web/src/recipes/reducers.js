import {
    CREATE_RECIPE,
    REMOVE_RECIPE,
    UPDATE_RECIPE,
    LOAD_RECIPES_IN_PROGRESS,
    LOAD_RECIPES_SUCCESS,
    LOAD_RECIPES_FAILURE,
    CREATE_INGREDIENT,
    UPDATE_INGREDIENT,
    REMOVE_INGREDIENT,
    LOAD_INGREDIENTS_IN_PROGRESS,
    LOAD_INGREDIENTS_SUCCESS,
    LOAD_INGREDIENTS_FAILURE
} from './actions';

//state
// { 
//   recipes: { isLoading: false, data: [] },
//   ingredients: { isLoading: false, data: [] }
// }


const initialStateRecipes = { isLoading: false, data: [] };

export const recipesReducer = (state = initialStateRecipes, action) => {
    const { type, payload } = action;

    switch (type) {
    case CREATE_RECIPE: {
        const { recipe } = payload;
        return {
            ...state,
            data: state.data.concat(recipe),
        };
    }
    case REMOVE_RECIPE: {
        const { id } = payload;
        return {
            ...state,
            data: state.data.filter(recipe => recipe.id !== id),
        };
    }
    case UPDATE_RECIPE: {
        const { recipe: updatedRecipe } = payload;
        return {
            ...state,
            data: state.data.map(recipe => {
                if (recipe.id === updatedRecipe.id) {
                    return updatedRecipe;
                }
                return recipe;
            }),
        };
    }
    case LOAD_RECIPES_SUCCESS: {
        const { recipes } = payload;
        return {
            ...state,
            isLoading: false,
            data: recipes,
        };
    }
    case LOAD_RECIPES_IN_PROGRESS:
        return {
            ...state,
            isLoading: true,
        }
    case LOAD_RECIPES_FAILURE:
        return {
            ...state,
            isLoading: false,
        }
    default:
        return state;
    }
}

//Move to an ingredientsReducer file
const initialStateIngredients = { isLoading: false, data: [] };
export const ingredientsReducer = (state = initialStateIngredients, action) => {
    const { type, payload } = action;

    switch (type) {
    case CREATE_INGREDIENT: {
        const { ingredient } = payload;
        return {
            ...state,
            data: state.data.concat(ingredient),
        };
    }
    case UPDATE_INGREDIENT: {
        const { ingredient: updatedIngredient } = payload;
        return {
            ...state,
            data: state.data.map(ingredient => {
                if (ingredient.id === updatedIngredient.id) {
                    return updatedIngredient;
                }
                return ingredient;
            }),
        };
    }
    case REMOVE_INGREDIENT: {
        const { ingredient: ingredientToRemove } = payload;
        return state.filter(ingredient => ingredient.id !== ingredientToRemove.id);
    }
    case LOAD_INGREDIENTS_SUCCESS: {
        const { allIngredients } = payload;
        const ingredientsWithPrompt = [ {id: -1, name: "Select an ingredient."}, ...allIngredients ];
        return {
            ...state,
            isLoading: false,
            data: ingredientsWithPrompt,
        };    
    }
    case LOAD_INGREDIENTS_IN_PROGRESS:
        return {
            ...state,
            isLoading: true,
        }
    case LOAD_INGREDIENTS_FAILURE:
        return {
            ...state,
            isLoading: false,
        }
    default:
        return state;
    }
}