import React from 'react';
import './index.js'
import './index.css'

class NewGameButton extends React.Component{
    render(){
        return(
            <button
                className="newGameButton"
                onClick={ () => this.props.onClick() }
            >
                {this.props.value}
            </button>
            
        )
    }
}

export default NewGameButton;