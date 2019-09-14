import React from 'react';

class RenderContactInfo extends React.Component {

    render() {
        return(
            <React.Fragment>
                <div>
                    <p>{this.props.title}</p>
                    <p>{this.props.name}</p>
                </div>
                <br></br>    
            </React.Fragment>
        )
    }
}

export default RenderContactInfo;