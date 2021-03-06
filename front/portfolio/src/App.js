import React from 'react';
import logo from './logo.svg';
import { Redirect, Route, BrowserRouter as Router } from 'react-router-dom';
import  Dashboard  from "./views/dashboard";
import  Login  from "./views/login";
import './App.css';

class App extends React.Component{
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
              <Redirect to='/dashboard'/>
              <Route exact path="/dashboard" component={Dashboard} />
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

