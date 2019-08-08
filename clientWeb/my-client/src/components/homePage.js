import React from 'react';

class HomePage extends React.Component {
    render(){
        return (
            <div>
                <div>hello home {this.props.token}</div>
            </div>
            
        );
    }
}
export default HomePage;


