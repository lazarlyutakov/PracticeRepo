import React from 'react';

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