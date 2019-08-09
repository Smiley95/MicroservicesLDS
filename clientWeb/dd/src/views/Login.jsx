import React from 'react';
import DashboardPage from "./Dashboard/Dashboard.jsx";
import "mdbreact/dist/css/mdb.css";
import { Redirect, Route, BrowserRouter as Router } from 'react-router-dom';
import {
  MDBContainer,
  MDBRow,
  MDBCol,
  MDBCard,
  MDBCardBody,
  MDBModalFooter,
  MDBIcon,
  MDBCardHeader,
  MDBBtn,
  MDBInput
} from "mdbreact";

class Login extends React.Component {
    constructor(props){
        super(props);
        this.state={
          username:'',
          password:'',
          submitted: false,
          token: ''
      }
      this.handleChange = this.handleChange.bind(this);
      this.handleSubmit = this.handleSubmit.bind(this);
    }
    
    handleChange= (event)=> {
      const {name, value }= event.target;
      this.setState({[name]: value,        
      });
    } 
    
    handleSubmit(e) {
      e.preventDefault();

      this.setState({ submitted: true });
      const { username, password } = this.state;
      if (!(username && password)) {
        return;
      }
      var details = { username: username, password: password, grant_type: 'password' }
      var formBody = [];
      for (var property in details) {
        var encodedKey = encodeURIComponent(property);
        var encodedValue = encodeURIComponent(details[property]);
        formBody.push(encodedKey + "=" + encodedValue);
      }
      formBody = formBody.join("&");
      const requestOptions = {
        method: 'POST',
        headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
        body: formBody
      };
      fetch('https://localhost:44300/token',requestOptions)
      .then(res => res.json())
      .then((data) => {
        if(data['error']){
          console.log(data['error_description'])
          this.setState({ username: '', password: '' })
        }
        else{
          this.setState({ token: data['access_token'] })
          console.log(this.state.token);
        }
        
        
      })
      .catch(console.log)
      
  }
    render() {
      if(this.state.token){
        return (
        
          <Router>
          <Redirect to='/dashboard'/>
          <Route exact path="/dashboard" render={(props) => <DashboardPage {...props} token={this.state.token} username={this.state.username} password={this.state.password} />}/>
        </Router>
        );
      }
      else{
        return (
          
            <MDBContainer className="d-flex justify-content-center">
      <MDBRow >
        <MDBCol md="12">
          <MDBCard>
            <MDBCardBody>
              <MDBCardHeader className="form-header deep-blue-gradient rounded">
                <h3 className="my-3">
                  <MDBIcon icon="lock" /> Login:
                </h3>
              </MDBCardHeader>
              <form name="form" onSubmit={this.handleSubmit}>
                <div className={ 'grey-text form-group' + (this.state.submitted && !this.state.username ? ' has-error' : '')}>
                  <MDBInput
                    label="Type your username"
                    name="username"
                    value={this.state.username}
                    onChange={this.handleChange}
                    icon="user-tie"
                    group
                    type="text"
                    validate
                    error="wrong"
                    success="right"
                  />
                </div>
                <div className={ 'grey-text form-group' + (this.state.submitted && !this.state.password ? ' has-error' : '')}>
                <MDBInput
                    label="Type your password"
                    name="password"
                    value={this.state.password}
                    onChange={this.handleChange}
                    icon="unlock-alt"
                    group
                    type="password"
                    validate
                  />
                </div>
              <div className="text-center mt-4">
                {this.state.username && this.state.password && 
                  <MDBBtn
                  color="light-blue"
                  className="mb-3"
                  type="submit"
                >
                  Login
                </MDBBtn>
                }
                {!(this.state.username && this.state.password) && 
                  <MDBBtn disabled
                  color="light-blue"
                  className="mb-3"
                  type="submit"
                >
                  Login
                </MDBBtn>
                }
                </div>
              </form>
              <MDBModalFooter>
                <div className="font-weight-light">
                  <p>Forgot Password?</p>
                </div>
              </MDBModalFooter>
            </MDBCardBody>
          </MDBCard>
        </MDBCol>
      </MDBRow>
    </MDBContainer>
      );    
              }        
      }
}
export default Login;