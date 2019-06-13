import React from 'react'
import ReactDOM from 'react-dom'
import { Route, Link, BrowserRouter as Router, Switch } from 'react-router-dom'
import Game from './gameComponents/game.js'
import './index.css'
import Contact from './redirection/contact.js'
import Home from './redirection/home.js'
import Notfound from './redirection/notfound.js'

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
