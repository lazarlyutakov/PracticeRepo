// za momenta samo redirektva - ne izvyrshva realna registraciq

import React from 'react'

class Register extends React.Component {

    onSubmit = () => {
        this.props.history.push('/game');
    };

    render() {
        return(
            <form>
                <input placeholder='name' type='name'/>
                <input placeholder='email' type='email'/>
                <button onClick={this.onSubmit}>Go to game</button>                
            </form>
        )
    }

}

export default Register