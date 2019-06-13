import React from 'react'
import RedirectButton from './redirectButton'
import { Route, Link } from 'react-router-dom'

const Contact = ({match}) => <p>{match.params.id}</p>

class Contacts extends React.Component {
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
      //const {url} = this.props.match
    return (
        <div>
            <h1>Contact</h1>
            <strong>Select a contact</strong>
            <ul>
                <li>
                    <Link to='/contact/1'>Contact 1</Link>
                </li>
                <li>
                    <Link to='/contact/2'>Contact 2</Link>
                </li>
                <li>
                    <Link to='/contact/3'>Contact 3</Link>
                </li>
            </ul>
            <Route path='/contact/:id' component={Contact} />            
            {this.renderRedirectButton()}
        </div>
    ) 
        
  }
}
export default Contacts