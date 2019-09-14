import React from 'react'
import RedirectButton from './redirectButton'
import RenderContactInfo from './renderContactInfo'
import { Route, Link } from 'react-router-dom'

const Contact = ({match}) => <div>{checkId(match)} </div>

const checkId = (match) => {
    if(match.params.id === '1'){
        return( 
            <RenderContactInfo name='Pesho Geq' title='Master Gay'/>
        )            
    }
    else if(match.params.id === '2'){
            return(
            <RenderContactInfo name='Paco Tigara' title='Vratarche'/>                
            )         
    }
    else{
        return(
            <RenderContactInfo name='Koki Kosmonavta' title='Dyrvo'/>            
        ) 
    }
}

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