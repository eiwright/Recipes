import React, { useEffect } from 'react';
import { connect } from 'react-redux';
import SearchBox from './SearchBox';
import RecipeCard from './RecipeCard';
import { loadRecipes, removeRecipeRequest } from './thunks';
import {Row} from 'react-bootstrap';
import {
    getRecipesLoading,
    getRecipes,
} from './selectors';

const RecipeList = ({ recipes = [], onRemovePressed, isLoading, startLoadingRecipes }) => {
    useEffect(() => {
        startLoadingRecipes();
    }, []);
    
    const loadingMessage = <div>Loading recipes...</div>;
    const content = (
        <div className='container'>
            <SearchBox style={{marginTop: '.5rem'}}/>
            <Row xs={1} md={3} className="xl-4">
                {recipes.map( (recipe, index) => 
                    <RecipeCard key={index}
                    recipe={recipe}
                    onRemovePressed={onRemovePressed}/>)}
            </Row>
        </div>
    );
    return isLoading ? loadingMessage : content;
};

const mapStateToProps = state => ({
    isLoading: getRecipesLoading(state),
    recipes: getRecipes(state)
});

const mapDispatchToProps = dispatch => ({
    startLoadingRecipes: () => dispatch(loadRecipes()),
    onRemovePressed: id => dispatch(removeRecipeRequest(id))
});

export default connect(mapStateToProps, mapDispatchToProps)(RecipeList);