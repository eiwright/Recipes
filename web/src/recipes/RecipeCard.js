import React, { useState } from 'react';
import { connect } from 'react-redux';
import { removeRecipeRequest } from './thunks';
import styled from 'styled-components';
import {Button, Card, ListGroup, Col} from 'react-bootstrap';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faPlus, faMinus, faTrash, faEdit } from '@fortawesome/free-solid-svg-icons'
import NewRecipeForm from './NewRecipeForm';
import {
    getRecipesLoading,
    getRecipes,
} from './selectors';
import './RecipeCard.css';


const Icon = styled(FontAwesomeIcon)`
    margin: 0rem .5rem;
    color: #9cb051  ;
    cursor: pointer;
`;

const IconButton = styled(FontAwesomeIcon)`
    margin-right: .5rem;
    color: white;
    cursor: pointer;
`;

const Expander = styled.span`
    color: #9cb051;
    cursor: pointer;
    margin-bottom: 1rem;
`;

const EditButton = styled(Button)`
    color: black;
    background-color: #d6dcee;
    border: 2px solid #d6dcee;    
`;
const RemoveButton = styled(Button)`
    color: black;
    background-color: #efd8c8;
    border: 2px solid #efd8c8
`;

const RecipeCard = ({ recipe, onRemovePressed }) => {
    const [showInstructions, setShowInstructions] = useState(false);
    const [showIngredients, setShowIngredients] = useState(false);
    const [showForm, setShowForm] = useState(false);

    return (    
    <Col className='d-flex align-items-stretch pt-4'>
        {showForm && <NewRecipeForm show={showForm} recipe={recipe} editRecipe={true} setShow={setShowForm} />}
    
        <Card className="justify-content-md-center w-auto">
            {recipe?.imageDataUrl?.length > 0 ?
            <Card.Img variant="top" src={recipe.imageDataUrl} alt="recipe image" style={{ maxHeight: '14rem', objectFit:'cover'}}/> :
            <Card.Img variant="top" src={recipe.imageUrl} alt="recipe image" style={{ maxHeight: '14rem', objectFit:'cover'}}/>            
            }
            <Card.Body>
                <Card.Title className="text-center" style={{fontWeight: 'bold'}}>{recipe.name}</Card.Title>
                <Card.Text className='text-center'>{recipe.description}</Card.Text>
            </Card.Body>
            {showIngredients
                    ?
                    <div className='text-start'>
                        <Expander onClick={() => setShowIngredients(false)}>
                            <Icon icon={faMinus} />Hide Ingredients
                        </Expander>                        
                        <ListGroup variant="flush">
                            {recipe.ingredients.map((ingredient, index) => <ListGroup.Item key={index}>{ingredient.name}</ListGroup.Item>)}                
                        </ListGroup>                        
                    </div>
                    : <Expander className='text-start' onClick={() => setShowIngredients(true)}>
                        <Icon icon={faPlus} />
                        Show Ingredients
                        </Expander> }
            {showInstructions
                    ?
                    <div className='text-start'>
                        <Expander onClick={() => setShowInstructions(false)}>
                            <Icon icon={faMinus} />
                            Hide Instructions</Expander>                        
                        <ListGroup variant="flush" style={{textAlign: 'start'}}>
                            {recipe.instructions.map((instruction) => <ListGroup.Item key={instruction.id}>{instruction.order}:&nbsp;{instruction.description}</ListGroup.Item>)}                
                        </ListGroup>
                    </div>
                    : <Expander className='text-start' onClick={() => setShowInstructions(true)}>
                        <Icon icon={faPlus} />Show Instructions</Expander> }        
            <Card.Footer className='text-start'>
                <EditButton className='mx-2 edit-button'
                    onClick={() =>  { setShowForm(true); }}
                    ><IconButton icon={faEdit} style={{color:'#7a84c8'}}></IconButton>Edit</EditButton>
                <RemoveButton className='mx-2 remove-button'
                    onClick={() => onRemovePressed(recipe.id)}
                    ><IconButton icon={faTrash} style={{color:'#eb955e'}}></IconButton>Remove</RemoveButton>
            </Card.Footer>
        </Card>

    </Col>
    );
};

const mapStateToProps = state => ({
    isLoading: getRecipesLoading(state),
    recipes: getRecipes(state)
});

const mapDispatchToProps = dispatch => ({
    onRemovePressed: id => dispatch(removeRecipeRequest(id))
});
export default connect(mapStateToProps, mapDispatchToProps)(RecipeCard);