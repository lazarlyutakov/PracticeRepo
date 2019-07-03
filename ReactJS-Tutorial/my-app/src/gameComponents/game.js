import React from 'react';
import Board from './board.js';
import Helper from '../helper';

class Game extends React.Component{
    constructor(props) {
        super(props);
        this.state = {
          history: [{
            squares: Array(9).fill(null),
          }],
          stepNumber: 0,
          xIsNext: true,
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

        let position = 'a';

        switch(i) {
            case 0:
                position = 'row 1, col 1';
              break;
            case 1:
                position = 'row 1, col 2';
              break;
            case 2:
                position = 'row 1, col 3';
              break;
            case 3:
                position = 'row 2, col 1';
              break;
            case 4:
                position = 'row 2, col 2';
              break;
            case 5:
                position = 'row 2, col 3';
              break;
            case 6:
                position = 'row 3, col 1';
              break;
            case 7:
                position = 'row 3, col 2';
              break;
            case 8:
                position = 'row 3, col 3';
              break;              
            default:
          }

        let root = document.getElementById('root');
        let positionLine = document.createElement('div');
        positionLine.innerHTML = position;
        root.appendChild(positionLine);
    }

    jumpTo(step){
        this.setState({
            stepNumber: step,
            xIsNext: (step % 2 ) === 0
        });
    }

    render(){
        const history = this.state.history;
        const current = history[this.state.stepNumber];
        const winner = Helper.calculateWinner(current.squares);

        const moves = history.map((step, move) => {
            const desc = move ? 'Go to move #' + move : 'Go to game start';
            return(
                <li key={move}>
                    <button onClick = { () => this.jumpTo(move)}>{desc}</button>
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
            </div>
        );
    }
}

export default Game;