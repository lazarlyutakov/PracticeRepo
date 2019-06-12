import React from 'react';
import ReactDOM from 'react-dom';
import RedirectButton from './redirectButton'
import NewGameButton from './newGameButton'
import Game from './game.js'
import './index.css'

function Square(props) {    
        return(
            <button className="square" onClick={props.onClick}>
                {props.value}                  
            </button>
        )
}

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

ReactDOM.render(
    <Game />,
    document.getElementById('root')
);

function calculateWinner(squares){
    const lines = [
        [0, 1, 2],
        [3, 4, 5],
        [6, 7, 8],
        [0, 3, 6],
        [1, 4, 7],
        [2, 5, 8],
        [0, 4, 8],
        [2, 4, 6],        
    ]

    for (let i = 0; i < lines.length; i++) {
        const [a, b, c] = lines[i];

        if(squares[a] && squares[a] === squares[b] && squares[a] === squares[c]){
            setTimeout( () => {
            window.alert('Game over ! Winner is: ' + squares[a]);
                
            }, 50);
            return squares[a];
        }
    }
    return null;

}

export {calculateWinner, Board} ;
