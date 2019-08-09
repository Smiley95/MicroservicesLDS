import React, { Component } from 'react';
import { Redirect, Route, BrowserRouter as Router } from 'react-router-dom';
import Login from './views/Login';
import DashboardPage from './views/Dashboard/Dashboard';

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
              <Redirect to='/dashboard'/>
              <Route exact path="/dashboard" component={DashboardPage} />
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
