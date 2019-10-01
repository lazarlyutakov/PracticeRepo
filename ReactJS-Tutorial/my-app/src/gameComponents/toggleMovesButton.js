import React from 'react';

class ToggleeMovesButton extends React.Component {
    render() {
        return(
            <button
                // className = 'toggleMovesButton'
                className = {this.props.class}
                onClick = { () => this.props.onClick() }
            >
                {this.props.value}
            </button>
        )
    }
}

export default ToggleeMovesButton;