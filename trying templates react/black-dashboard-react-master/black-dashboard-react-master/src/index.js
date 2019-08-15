/*!

=========================================================
* Black Dashboard React v1.0.0
=========================================================

* Product Page: https://www.creative-tim.com/product/black-dashboard-react
* Copyright 2019 Creative Tim (https://www.creative-tim.com)
* Licensed under MIT (https://github.com/creativetimofficial/black-dashboard-react/blob/master/LICENSE.md)

* Coded by Creative Tim

=========================================================

* The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

*/
import React, { Component } from "react";
import ReactDOM from "react-dom";
import { createBrowserHistory } from "history";
import { Router, Route } from "react-router-dom";

import AdminLayout from "layouts/Admin/Admin.jsx";
import Login from "views/Login.jsx";

import "assets/scss/black-dashboard-react.scss";
import "assets/demo/demo.css";
import "assets/css/nucleo-icons.css";


class App extends Component{
  constructor(props){
    super(props);
    this.state = {
      loggedIn : false,
    }
  }
  
  
  render(){
    const hist = createBrowserHistory();
    if (this.state.loggedIn){
      return (
        
            <Router history={hist}>
              <div>
              
              <Route exact path="/admin/dashboard" component={AdminLayout} />
              </div>
            </Router>
        
      );
    }
    else {
      return (
        
          <Router history={hist}>
              <div>
               
              <Route exact path="/login" component={Login}/>           
              </div>
            </Router>
        
            
      );
    }  
  }
}

ReactDOM.render(<App />, document.getElementById('root'));
/*ReactDOM.render(
  <Router history={hist}>
    <Switch>
      <Route path="/login" render={props => <Login {...props} />} />
      <Route path="/admin" render={props => <AdminLayout {...props} />} />
      <Redirect from="/" to="/admin/dashboard" />
    </Switch>
  </Router>,
  document.getElementById("root")
);*/
