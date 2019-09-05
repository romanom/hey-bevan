import React, { Component } from "react";
import serviceFunc from "./../service/service";
import "./styles/leaderboard.css";

class Leaderboard extends Component {
  state = {
    leaderboardData: []
  };

  componentDidMount() {
    this.setState({
      leaderboardData: serviceFunc.getLeaderboardData(
        this.props.type,
        this.props.dateType,
        this.props.channel
      )
    });
  }

  render() {
    return (
      <div>
        <table id="leaderboard">
          <col width="10%" />
          <col width="80%" />
          <col width="10%" />
          <tr>
            <th class="leaderboard-headings" style={{ textAlign: "left" }}>
              Rank
            </th>
            <th class="leaderboard-headings" style={{ textAlign: "left" }}>
              Person
            </th>
            <th class="leaderboard-headings">Total HeyBevans</th>
          </tr>
          {this.state.leaderboardData.map(leaderboard => (
            <tr>
              <td style={{ textAlign: "left" }}>{leaderboard.rank}</td>
              <td style={{ textAlign: "left" }}>
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
