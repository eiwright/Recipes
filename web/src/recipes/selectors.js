import { createSelector } from 'reselect';

export const getRecipes = state => state.recipes.data;
export const getRecipesLoading = state => state.recipes.isLoading;
export const getAllIngredients = state => state.ingredients.data;

// export const getIncompleteTodos = createSelector(
//     getRecipes,
//     getAllIngredients,
//     (recipes, ingredients) => todos.filter(todo => !todo.isCompleted),
// );
