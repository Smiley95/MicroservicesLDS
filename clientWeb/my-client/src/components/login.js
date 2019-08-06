import React from 'react';

class Login extends React.Component {
    constructor(props){
        super(props);
        this.state={
          username:'',
          password:'',
          submitted: false,
          token: '',
          loading: false,
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
      fetch('https://localhost:44330/token',requestOptions)
      .then(res => res.json())
      .then((data) => {
        if(data['error']){
          console.log(data['error_description'])
          this.setState({ username: '', password: '' })
        }
        else{
          this.setState({ token: data['access_token'] })
          console.log(this.state.token)
          this.setState({ loading: true, })
        }
        
        
      })
      .catch(console.log)
      
  }
    render() {
        return (
        <div className="col-md-6 col-md-offset-3">
        <div>hello there {this.state.token}</div>
        <h2>Login</h2>
        <form name="form" onSubmit={this.handleSubmit}>
            <div className={'form-group' + (this.state.submitted && !this.state.username ? ' has-error' : '')}>
                <label htmlFor="username">Username</label>
                <input type="text" className="form-control" name="username" value={this.state.username} onChange={this.handleChange} />
                {this.state.submitted && !this.state.username &&
                    <div className="help-block">Username is required</div>
                }
            </div>
            <div className={'form-group' + (this.state.submitted && !this.state.password ? ' has-error' : '')}>
                <label htmlFor="password">Password</label>
                <input type="password" className="form-control" name="password" value={this.state.password} onChange={this.handleChange} />
                {this.state.submitted && !this.state.password &&
                    <div className="help-block">Password is required</div>
                }
            </div>
            <div className="form-group">
                <button className="btn btn-primary" disabled={this.state.loading} >Login</button>
                {this.state.loading &&
                  <img src="data:image/gif;base64,R0lGODlhEAAQAPIAAP///wAAAMLCwkJCQgAAAGJiYoKCgpKSkiH/C05FVFNDQVBFMi4wAwEAAAAh/hpDcmVhdGVkIHdpdGggYWpheGxvYWQuaW5mbwAh+QQJCgAAACwAAAAAEAAQAAADMwi63P4wyklrE2MIOggZnAdOmGYJRbExwroUmcG2LmDEwnHQLVsYOd2mBzkYDAdKa+dIAAAh+QQJCgAAACwAAAAAEAAQAAADNAi63P5OjCEgG4QMu7DmikRxQlFUYDEZIGBMRVsaqHwctXXf7WEYB4Ag1xjihkMZsiUkKhIAIfkECQoAAAAsAAAAABAAEAAAAzYIujIjK8pByJDMlFYvBoVjHA70GU7xSUJhmKtwHPAKzLO9HMaoKwJZ7Rf8AYPDDzKpZBqfvwQAIfkECQoAAAAsAAAAABAAEAAAAzMIumIlK8oyhpHsnFZfhYumCYUhDAQxRIdhHBGqRoKw0R8DYlJd8z0fMDgsGo/IpHI5TAAAIfkECQoAAAAsAAAAABAAEAAAAzIIunInK0rnZBTwGPNMgQwmdsNgXGJUlIWEuR5oWUIpz8pAEAMe6TwfwyYsGo/IpFKSAAAh+QQJCgAAACwAAAAAEAAQAAADMwi6IMKQORfjdOe82p4wGccc4CEuQradylesojEMBgsUc2G7sDX3lQGBMLAJibufbSlKAAAh+QQJCgAAACwAAAAAEAAQAAADMgi63P7wCRHZnFVdmgHu2nFwlWCI3WGc3TSWhUFGxTAUkGCbtgENBMJAEJsxgMLWzpEAACH5BAkKAAAALAAAAAAQABAAAAMyCLrc/jDKSatlQtScKdceCAjDII7HcQ4EMTCpyrCuUBjCYRgHVtqlAiB1YhiCnlsRkAAAOwAAAAAAAAAAAA==" />
                }
                
            </div>
            
        </form>
    </div>)
      }
}
export default Login;