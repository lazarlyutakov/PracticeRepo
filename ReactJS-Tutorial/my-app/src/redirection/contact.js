import React from 'react'
import RedirectButton from './redirectButton'

class Contact extends React.Component {
    constructor(props){
        super(props);
        this.state={
            test: {
                name: 'GO TO SULTANBET',                
            }
        };
    }
    
    handleButtonClick(){
        window.open('https://sultanbet.com', '_blank');
    }

    renderRedirectButton(){
        return(            
            <RedirectButton value={this.state.test.name} onClick = { () => this.handleButtonClick() }/>
        )
    }

  render() {
      const { params } = this.props.match
    return (
        <div>
            <h1>Contact</h1>
            <p>{params.id}</p>                        
            {this.renderRedirectButton()}
        </div>
    ) 
        
  }
}
export default Contact