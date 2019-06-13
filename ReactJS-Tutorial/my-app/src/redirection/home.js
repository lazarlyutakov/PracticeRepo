import React from 'react'

class Home extends React.Component {
  render() {
    return (
      <div>
        <h1>Кур за 10betChina</h1>
            <ul>
                <li>
                    <a href='/game'>Game</a>
                </li>
                <li>
                    <a href='/contact'>Contact</a>                 
                </li>                                
            </ul>
      </div>
    )
  }
}
export default Home