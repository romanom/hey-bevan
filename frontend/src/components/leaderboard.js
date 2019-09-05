import React, { Component } from "react";
import getLeaderboardData from "./../service/service";
import "./styles/leaderboard.css";
class Leaderboard extends Component {
  state = {
    leaderboardData: []
  };

  componentDidMount() {
    this.setState({
      leaderboardData: getLeaderboardData(
        this.props.type,
        this.props.dateType,
        this.props.channel
      )
    });
  }

  render() {
    return (
      <div>
        <table id="leaderboard" border="1">
          <tr>
            <th>Rank</th>
            <th>Person</th>
            <th>Total Hey-Bevans</th>
          </tr>
          {this.state.leaderboardData.map(leaderboard => (
            <tr>
              <td>{leaderboard.rank}</td>
              <td>
                {leaderboard.name} {leaderboard.image}
              </td>
              <td>{leaderboard.total}</td>
            </tr>
          ))}
        </table>
      </div>
    );
  }
}

export default Leaderboard;
