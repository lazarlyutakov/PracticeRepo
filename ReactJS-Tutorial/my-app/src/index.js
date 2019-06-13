import React from 'react'
import ReactDOM from 'react-dom'
import { Route, Link, BrowserRouter as Router, Switch } from 'react-router-dom'
import Game from './game.js'
import './index.css'
import Contact from './contact.js'
import Home from './home.js'
import Notfound from './notfound.js'


function Square(props) {    
        return(
            <button className="square" onClick={props.onClick}>
                {props.value}                  
            </button>
        )
}

const routing = (
    <Router>
        <div>
            <ul>
                <li>
                    <Link to='/'>Home</Link>
                </li>
            </ul>
        </div>
        <Switch>
            <Route exact path ='/' component={Home} />            
            <Route path ='/contact' component={Contact} />
            <Route path ='/game' component={Game} />
            <Route component={Notfound} />                                    
        </Switch>
    </Router>    
)

ReactDOM.render(
    routing,    
    document.getElementById('root')
);



export {Square} ;
