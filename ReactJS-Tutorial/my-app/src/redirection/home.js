import React from 'react'

class Home extends React.Component {  
  render() {
    return (
      <div>
        <h1>Кур за 10BetChina</h1>
            <ul>
                <li>
                    <a href='/game'>Game</a>
                </li>
                <li>
                    <a href='/register'>Register</a>                 
                </li>
                <li>
                    <a href='/contact'>Contact</a>                 
                </li>
                <li>
                    <a href='/prazno'>Prazno - 404 - test</a>                 
                </li>                                
            </ul>
      </div>
    )
  }
}

export default Home