import React from 'react';
/* let  MyContext = {
    user : {username: '',
    password: '',
    token: ''}
}
export default MyContext;*/
const UserContext = React.createContext({})

export const UserProvider = UserContext.Provider
export const UserConsumer = UserContext.Consumer
export default UserContext
