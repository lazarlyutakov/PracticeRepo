import React from 'react';
import Board from './board.js';
import Helper from '../helper';
import ToggleMovesButton from './toggleMovesButton';

var toggleButtonValueStates = {
    descending: 'Sort descending',
    ascending: 'Sort ascending'
  }

class Game extends React.Component{
    constructor(props) {
        super(props);
        this.state = {
          history: [{
            squares: Array(9).fill(null),
          }],
          stepNumber: 0,
          xIsNext: true,
          toggleMovesButtonState: 'ascending',
          toggleButtonValue: toggleButtonValueStates.descending
        };
      }
      
      handleClick(i){
          const history = this.state.history.slice(0, this.state.stepNumber + 1);
          const current = history[history.length - 1];
          const squares = current.squares.slice();
        
        if(Helper.calculateWinner(squares) || squares[i]){
            return;
        }
        squares[i] = this.state.xIsNext ? 'X' : 'O'

        this.setState({
            history: history.concat([{
                squares: squares,
            }]),
            stepNumber: history.length,
            xIsNext: !this.state.xIsNext,
        });

        Helper.displayPositionLine(i);
    }

    jumpTo(step){
        this.setState({
            stepNumber: step,
            xIsNext: (step % 2 ) === 0
        });
    }

    handleToggleMovesButtonClick(){

        let arr =[];
        const history = this.state.history;
        
        Helper.pushToMovesArray(history, arr);

        if(this.state.toggleMovesButtonState === 'ascending' && this.state.toggleButtonValue === toggleButtonValueStates.descending ) {
            this.setState({
                toggleMovesButtonState: 'descending',
                toggleButtonValue: 'Sort ascending'
            });

            arr.reverse();
            console.log(arr);
        }
        else if (this.state.toggleMovesButtonState === 'descending' && this.state.toggleButtonValue === toggleButtonValueStates.ascending) {
            this.setState({
                toggleMovesButtonState: 'ascending',
                toggleButtonValue: 'Sort descending'
            });

            console.log(arr);
        }        
    }

    renderToggleMovesButton() {
        return(
            <ToggleMovesButton class = {this.state.toggleMovesButtonState} value = {this.state.toggleButtonValue} onClick = { () => this.handleToggleMovesButtonClick()} />
        )
    }

    render(){
        const history = this.state.history;
        const current = history[this.state.stepNumber];
        const winner = Helper.calculateWinner(current.squares);

        const moves = history.map((step, move) => {
            const desc = move ? 'Go to move #' + move : 'Go to game start';
            return(
                <li key={move}>
                    <button className = 'moveButton' onClick = { () => this.jumpTo(move)}>{desc}</button>
                </li>
            );
        });

        let status;
        if(winner){
            status = 'Winner: ' + winner;
        } else{
            status = 'Next player: ' + (this.state.xIsNext ? 'X' : 'O');
        }

        return(
            <div className="game">
                <div className="game-board">
                    <Board 
                        squares = {current.squares}
                        onClick = {(i) => this.handleClick(i)}
                    />
                </div>
                <div className="game-info">
                    <div>{status}</div>                                        
                    <ol>{moves}</ol>
                </div>
                <br></br>
                <div>{this.renderToggleMovesButton()}</div>
            </div>
        );
    }
}

export default Game;