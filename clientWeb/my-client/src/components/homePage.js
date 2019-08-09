import React from 'react';

class HomePage extends React.Component {
    
    componentDidMount(){
        const requestOptions = {
            method: 'GET',
            headers : {Authorization : "Bearer " + this.props.token}
            
        }
        fetch('https://localhost:44300/api/Assets',requestOptions)
        .then(res => res.json())
        .then((data) => {
            console.log(this.props.token)
            console.log(data)
        })
        .catch(console.log)
        fetch('https://localhost:44300/api/Companies',requestOptions)
        .then(res => res.json())
        .then((data) => {
            console.log(this.props.token)
            console.log(data)
        })
        .catch(console.log)
        fetch('https://localhost:44300/api/Portfolios',requestOptions)
        .then(res => res.json())
        .then((data) => {
            console.log(this.props.token)
            console.log(data)
        })
        .catch(console.log)
        fetch('https://localhost:44300/api/Currencies',requestOptions)
        .then(res => res.json())
        .then((data) => {
            console.log(this.props.token)
            console.log(data)
        })
        .catch(console.log)
        fetch('https://localhost:44300/api/Investors',requestOptions)
        .then(res => res.json())
        .then((data) => {
            console.log(this.props.token)
            console.log(data)
        })
        .catch(console.log)
    }
    render(){
        return (
            <div>
                <div>hello home {this.props.token}</div>
            </div>
            
        );
    }
}
export default HomePage;


