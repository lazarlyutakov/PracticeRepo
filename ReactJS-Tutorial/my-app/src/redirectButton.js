import React from 'react';
import ReactDOM from 'react-dom';
import './index.js'
import './index.css'

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