// za momenta samo redirektva - ne izvyrshva realna registraciq

import React from 'react'

class Register extends React.Component {

    onSubmit = () => {
        const name = document.getElementById('name').value;
        console.log('ime ' + name);
        this.props.history.push('/game');

        return name;
    };



    render() {
        return(
            <form>
                <input id='name' placeholder='name' type='name'/>
                <input id='password' placeholder='email' type='email'/>
                <button onClick={this.onSubmit}>Go to game</button>                
            </form>
        )
    }

}

export default Register