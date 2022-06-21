import React from 'react';
import logo from './logo.svg';
import './App.css';
import RecipeList from './recipes/RecipeList';
import Header from './Header';
function App() {
  return (
    <div className="App">
        <Header />        
        <RecipeList />
    </div>
  );
}

export default App;
