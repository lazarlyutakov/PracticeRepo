import React from 'react';

class RedirectButton extends React.Component{
    render(){
        return(
            <button
                className="button"
                onClick={ () => this.props.onClick() }
            >
                {this.props.value}               
            </button>            
        )
    }
}

export default RedirectButton;