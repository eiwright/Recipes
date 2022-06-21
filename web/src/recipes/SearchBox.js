import React, { useState } from 'react';
import { connect } from 'react-redux';
import { searchRecipeRequest } from './thunks';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faMagnifyingGlass } from '@fortawesome/free-solid-svg-icons'
import styled from 'styled-components';
import NewRecipeForm from './NewRecipeForm';
import {Row, Col, Form, Container, Button} from 'react-bootstrap';
import './SearchBox.css';

const SearchDiv = styled.div`
    border-radius: 8px;
    padding: 16px;
    text-align: center;
    box-shadow: 0 4px 8px grey;
    margin-top: .5rem;
`;

const SearchButton = styled(Button)`
    color: white;
    background-color: #95b251;
    border: #95b251
`;

const SearchBox = ({ onSearchPressed }) => {
    const [searchValue, setSearchValue] = useState('');
    const [showForm, setShowForm] = useState(false);

    return (
        <div>
        <SearchDiv>
            <Container fluid className='text-start' >
                <Row className='justify-content-between'>
                    <Col>
                        <SearchButton className="search-button"
                            onClick={() => setShowForm(true)}>
                            Create Recipe
                        </SearchButton>
                    </Col>
                    <Col>
                        <Row className='justify-content-end'>
                            <Col xs={10}>
                                <Form.Control placeholder="Search for recipes" value={searchValue}
                                    onChange={e => setSearchValue(e.target.value)}
                                    onKeyPress={e => (e.key === 'Enter' ? onSearchPressed(e.target.value) : null) } />
                            </Col>
                            <Col xs={2}>
                                <SearchButton className="search-button" onClick={() => onSearchPressed(searchValue)} >
                                    <FontAwesomeIcon icon={faMagnifyingGlass} className="align-middle icon fa-icon" />
                                </SearchButton>
                            </Col>
                        </Row>
                    </Col>                
                </Row>
            </Container>
        </SearchDiv>
        <NewRecipeForm show={showForm} recipe={null} editRecipe={false} setShow={setShowForm} />
        </div>
    );
};

const mapStateToProps = state => ({
});

const mapDispatchToProps = dispatch => ({
    onSearchPressed: text => dispatch(searchRecipeRequest(text)),
});

export default connect(mapStateToProps, mapDispatchToProps)(SearchBox);