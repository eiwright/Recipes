import React, { useState, useEffect } from 'react';
import { connect } from 'react-redux';
import { addRecipeRequest, loadAllIngredients, updateRecipeRequest } from './thunks';
import {Modal, Button, Form, ListGroup, Row, Col, Image} from 'react-bootstrap';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faPlus, faXmark } from '@fortawesome/free-solid-svg-icons'
import {
    getAllIngredients,
} from './selectors';
import styled from 'styled-components';

const Icon = styled(FontAwesomeIcon)`
    margin: 0rem .5rem;
    color: blue;
    cursor: pointer;
`;

const Expander = styled.span`
    color: blue;
    cursor: pointer;    
    margin-bottom: 1rem;
`;

const initialImageValues = {
    imageSrc: '/img/placeholder-image.png',
    imageFile: null
};

const NewRecipeForm = ({ show, recipe, editRecipe, setShow, onCreatePressed, onUpdatePressed, startLoadingIngredients, allIngredients }) => {
    const [nameValue, setNameValue] = useState(recipe?.name ?? "");
    const [descriptionValue, setDescriptionValue] = useState(recipe?.description ?? "");
    const [imageValue, setImageValue] = useState(recipe?.image ?? "");
    const [imageDataValues, setImageDataValues] = useState(initialImageValues);
    const [showImageUrl, setShowImageUrl] = useState(true);
    const [ingredients, setIngredients] = useState(recipe?.ingredients ?? []);
    const [instructions, setInstructions] = useState(recipe?.instructions ?? []);
    const [selectedIngredientId, setSelectedIngredientId] = useState();
    const [selectedIngredientQuantity, setSelectedIngredientQuantity] = useState("");
    const [selectedIngredientCalories, setSelectedIngredientCalories] = useState("");

    const [newInstructionText, setNewInstructionText] = useState("");
    const [newInstructionOrder, setNewInstructionOrder] = useState("");

    
    useEffect(() => {
        startLoadingIngredients();
    }, []);

    useEffect(() => {
        if(!editRecipe) {
            setNameValue("");
            setDescriptionValue("");
            setImageValue("");
            setImageDataValues(initialImageValues);
            setIngredients([]);
            setInstructions([]);
        }
    }, [editRecipe]);

    const submit = () =>{
        const recipeToSave = {
            id: recipe?.id,
            name: nameValue,
            description: descriptionValue,
            imageUrl: imageValue ?? null,
            imageData: imageDataValues.imageFile,
            fileName: imageDataValues.imageFile?.name,
            ingredients,
            instructions
        };
        if(recipe?.id && recipe?.id > 0){
            onUpdatePressed(recipeToSave)
        }
        else {
            onCreatePressed(recipeToSave);
        }
    };
    const addIngredient = (ingredientId, quantity, calories) => {
        if(!ingredientId || parseInt(ingredientId) < 1){
            alert("You must select an ingredient.");
            return;     
        }
        if(!quantity){
            alert("You must set a quantity");
            return;
        }
        
        const selectedIngredient = allIngredients.find(i => i.id === parseInt(ingredientId));
        const ingredient = {
            id: parseInt(ingredientId),
            name: selectedIngredient.name, 
            quantity, 
            calories: calories ? parseInt(calories) : 0,
        };
        setIngredients(ingredients.concat(ingredient))
        setSelectedIngredientQuantity("")
        setSelectedIngredientCalories("")
    };

    const removeIngredient = (id) => {
        setIngredients(ingredients.filter(i => i.id !== id));
    };

    const addInstruction = (description, order) => {
        if(!order) {
            alert("You must set a Order");
            return;
        }
        const orderInt = parseInt(order);
        if(instructions.some(x => x.order === orderInt)) {
            alert("Order must be unique.");
            return;
        }
        const orderMax = instructions.length === 0 ? 1 : Math.max(...instructions.map(o => o.order)) + 1
        if(orderMax < orderInt || orderInt < 1) {
            alert("Order is too large or small");
            return;
        }
        const instruction = { description, order:orderInt};
        setInstructions(instructions.concat(instruction));
        setNewInstructionText("")
        setNewInstructionOrder("")
    };
    const removeInstruction = (id) => {
        setInstructions(instructions.filter(i => i.id !== id));
    };
    const showPreview = e => {
        if(e.target.files && e.target.files[0]){
            let imageFile = e.target.files[0];
            const reader = new FileReader();
            reader.onload = x => {
                setImageDataValues({
                    imageFile: imageFile,
                    imageSrc: x.target.result
                });
            }
            reader.readAsDataURL(imageFile);
            setShowImageUrl(false);
        }
        else {
            setImageDataValues(initialImageValues);       
        }
    }
    return (
        <Modal size="lg" show={show} onHide={() => setShow(false)} >
            <Modal.Header closeButton>
                <Modal.Title>{editRecipe ? "Edit Recipe" : "Add a Recipe"}</Modal.Title>
            </Modal.Header>
            <Modal.Body>
                <Form>
                    <Form.Group className="mb-3" controlId="formTitle">
                        <Form.Label>Title</Form.Label>
                        <Form.Control type="input" value={nameValue} onChange={e => setNameValue(e.target.value)} />
                    </Form.Group>
                    <Form.Group className="mb-3" controlId="formDescription">
                        <Form.Label>Description</Form.Label>
                        <Form.Control as="textarea" rows={3} value={descriptionValue} onChange={e => setDescriptionValue(e.target.value)} />
                    </Form.Group>
                    <Form.Group className="mb-3" controlId="formImage">
                        <Form.Label>Image</Form.Label>
                        {showImageUrl &&
                        <Form.Control type="input" value={imageValue} className="mb-2" placeholder='Enter an image url here or choose a file.' onChange={e => setImageValue(e.target.value)} />
                        }
                        <Form.Control type="file" accept='image/*'onChange={showPreview} />
                        <Image src={imageDataValues.imageSrc} alt="recipe image" style={{ margin: '5px', maxHeight: '14rem', objectFit:'cover'}}/>
                    </Form.Group>
                    <Form.Group className="mb-3" controlId="formIngredients">
                        <Form.Label>Ingredients</Form.Label>
                        <Row className="mb-3">
                            <ListGroup variant="flush">
                                {ingredients?.map((ingredient, index) => 
                                <ListGroup.Item key={index}>
                                    {ingredient.name}
                                    &nbsp;:&nbsp;
                                    {ingredient.quantity}&nbsp;
                                    <Expander onClick={() => removeIngredient(ingredient.id)}>
                                        <Icon icon={faXmark} color="red" /></Expander>
                                </ListGroup.Item>)}                
                            </ListGroup>
                        </Row>
                        <Form.Group as={Row} className="mb-3" controlId="formAddIngredients">
                            <Form.Label>Add Ingredients</Form.Label>
                            <Col sm={4}>
                                <Form.Select placeholder='Ingredient' onChange={e => setSelectedIngredientId(e.target.value)}>
                                    {allIngredients?.map((ingredient, index) => <option key={index} value={ingredient.id || 1}>{ingredient.name}</option>)}                
                                </Form.Select>
                            </Col>
                            <Col sm={3}>
                                <Form.Control type="input" placeholder='Quantity' value={selectedIngredientQuantity} onChange={e => setSelectedIngredientQuantity(e.target.value)} />
                            </Col>
                            <Col sm={3}>
                                <Form.Control type="number" placeholder='Calories' value={selectedIngredientCalories} onChange={e => setSelectedIngredientCalories(e.target.value)} />                     
                            </Col>
                            <Col sm={2}>
                                <Expander className='text-start' onClick={() => addIngredient(selectedIngredientId, selectedIngredientQuantity, selectedIngredientCalories)}>
                                <Icon icon={faPlus} />Add</Expander>
                            </Col>
                        </Form.Group>
                    </Form.Group>
                    <Form.Group className="mb-3" controlId="formInstructions">
                        <Form.Label>Instructions</Form.Label>
                        <ListGroup variant="flush" style={{textAlign: 'start'}}>
                            {instructions?.map((instruction, index) => 
                                <ListGroup.Item key={index}>{instruction.order}:&nbsp;{instruction.description}
                                    &nbsp;
                                    <Expander onClick={() => removeInstruction(instruction.id)}>
                                    <Icon icon={faXmark} color="red" /></Expander>
                                </ListGroup.Item>)}                
                        </ListGroup>
                        <Form.Label>Add Instruction</Form.Label>
                        <Form.Group as={Row} className="mb-3" controlId="formText">
                            <Col sm={8}>
                                <Form.Control as="textarea" placeholder='Instructions Text'  rows={3} value={newInstructionText} onChange={e => setNewInstructionText(e.target.value)} />
                            </Col>
                            <Col sm={2}>
                                <Form.Control type="number" placeholder='Order' value={newInstructionOrder} onChange={e => setNewInstructionOrder(e.target.value)} />                     
                            </Col>
                            <Col sm={2}>
                                <Expander className='text-start'  onClick={() => addInstruction(newInstructionText, newInstructionOrder)}>
                                <Icon icon={faPlus} />Add</Expander>
                            </Col>
                        </Form.Group>
                    </Form.Group>                        
                </Form>
            </Modal.Body>
            <Modal.Footer>
                <Button variant="secondary"  className='mx-2' onClick={() => setShow(false)}>Cancel</Button>
                <Button variant="success" onClick={() =>  { submit(); setShow(false);}}>
                    {editRecipe ? "Save Recipe" : "Add recipe"}
                </Button>
            </Modal.Footer>
        </Modal>
    );
};

const mapStateToProps = state => ({
    allIngredients: getAllIngredients(state),
});

const mapDispatchToProps = dispatch => ({
    startLoadingIngredients: () => dispatch(loadAllIngredients()),
    onCreatePressed: recipe => dispatch(addRecipeRequest(recipe)),
    onUpdatePressed: recipe => dispatch(updateRecipeRequest(recipe)),
});

export default connect(mapStateToProps, mapDispatchToProps)(NewRecipeForm);