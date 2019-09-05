import React, { Component } from "react";
import LeaderboardRow from "./leaderboard-row";
class Leaderboard extends Component {
  render() {
    return (
      <div>
        <table>
          <tr>
            <th>Rank</th>
            <th>Person</th>
            <th>Total Hey-Bevans</th>
          </tr>
          <LeaderboardRow rank="1" name="jp" count="30" />
          <LeaderboardRow rank="2" name="jp2" count="29" />
          <LeaderboardRow rank="3" name="jp3" count="28" />
        </table>
      </div>
    );
  }
}

export default Leaderboard;
