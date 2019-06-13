import React from 'react';
import {Square} from './square.js';
import NewGameButton from '../gameComponents/newGameButton'


class Board extends React.Component{
    constructor(props){
        super(props);
        this.state={
           // squares: Array(9).fill(null),
            // xIsNext: true,
            test: {
                name1: 'New game',
                name2: 'KKKKKK'
            }
        };
    }    

    handleNewGameClick(){
        window.location.reload();
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
                    {this.renderNewGameButton()}
                </div>
                <br></br>              
            </div>
                                
        );
    }
}

export default Board;