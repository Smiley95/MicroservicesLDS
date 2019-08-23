import React from 'react';
import { Redirect, Route, BrowserRouter as Router } from 'react-router-dom';
import Dashboard from './dashboard';

class Login extends React.Component{
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
          <Route exact path="/dashboard" render={(props) => <Dashboard {...props} token={this.state.token} username={this.state.username} password={this.state.password} />}/>
        </Router>
        );
      }
      else{
        return (
          <div className="login-wrap">
          <div className="login-html">
            <form className="login-form" name="form" onSubmit={this.handleSubmit}>
              <div className="sign-in-htm">
                <div className="group">
                  <label htmlFor="user" className="label">Username</label>
                  <input id="user" type="text" className="input" value={this.state.username} onChange={this.handleChange}
                    />
                </div>
                <div className="group">
                  <label htmlFor="pass" className="label">Password</label>
                  <input id="pass" type="password" className="input" data-type="password" value={this.state.password}
                    onChange={this.handleChange}
                    />
                </div>
                <div className="group">
                  <input id="check" type="checkbox" className="check" checked/>
                  <label htmlFor="check"><span className="icon"></span> Keep me Signed in</label>
                </div>
                <div className="group" >
                  <button type="button" className="button" disabled={!(this.state.username && this.state.password)}>Sign In</button>
                </div>
                <div className="hr"></div>
                <div className="foot-lnk">
                  <a href="#forgot">Forgot Password?</a>
                </div>
              </div> 
            </form>
          </div>
          </div>
        );
      }        
    }
}
export default Login;