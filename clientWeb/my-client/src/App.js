import React, { Component } from 'react';
import { Redirect, Route, BrowserRouter as Router } from 'react-router-dom';
import Login from './components/login';
import HomePage from './components/homePage';

class App extends Component{
  constructor(props){
    super(props);
    this.state = {
      loggedIn : false,
    }
  }
  
  render(){

    if (this.state.loggedIn){
      return (
        
            <Router>
              <Redirect to='/home'/>
              <Route exact path="/home" component={HomePage} />
            </Router>
        
      );
    }
    else {
      return (
        
          <Router>
              <Redirect to='/login'/> 
              <Route exact path="/login" component={Login}/>           
            </Router>
        
            
      );
    }  
  }
}
export default App;
