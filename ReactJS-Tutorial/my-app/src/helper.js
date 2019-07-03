import React from 'react';


var Helper = ({
    calculateWinner(squares){
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
    },
    
    displayPositionLine(i){
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
})

export default Helper;