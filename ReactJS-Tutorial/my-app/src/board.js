import React from 'react';
import {Square} from './index.js';
import './index.css'
import RedirectButton from './redirectButton'
import NewGameButton from './newGameButton'


class Board extends React.Component{
    constructor(props){
        super(props);
        this.state={
           // squares: Array(9).fill(null),
            // xIsNext: true,
            test: {
                name: 'GO TO SULTANBET',
                name1: 'New game',
                name2: 'KKKKKK'
            }
        };
    }    

    handleButtonClick(){
        window.open('https://sultanbet.com', '_blank');
    }

    handleNewGameClick(){
        window.location.reload();
    }

    renderRedirectButton(){
        return(            
            <RedirectButton value={this.state.test.name} onClick = { () => this.handleButtonClick() }/>
        )
    }

    renderNewGameButton(){
        return(
            <NewGameButton value={this.state.test.name1} onClick = { () => this.handleNewGameClick() }/>
        )
    }

    renderSquare(i){
        return( 
        <Square 
               value={this.props.squares[i]} 
               onClick={ () => this.props.onClick(i) } 
         />
        );
    }

    render(){
        return (
            <div>
                <div className="board-row">
                    {this.renderSquare(0)}
                    {this.renderSquare(1)}
                    {this.renderSquare(2)}                    
                </div> 
                <div className="board-row">
                    {this.renderSquare(3)}
                    {this.renderSquare(4)}
                    {this.renderSquare(5)}                    
                </div> 
                <div className="board-row">
                    {this.renderSquare(6)}
                    {this.renderSquare(7)}
                    {this.renderSquare(8)}      
                </div>
                <br></br>
                <div>
                    {this.renderRedirectButton()}
                </div>
                <br></br>
                <div>
                    {this.renderNewGameButton()}
                </div>                      
            </div>                    
        );
    }
}

export default Board;