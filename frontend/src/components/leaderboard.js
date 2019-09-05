import React, { Component } from "react";
import serviceFunc from "./../service/service";
import "./styles/leaderboard.css";
import bevans from "./images/logo.png";

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
      <div className="scrollable">
        <table id="leaderboard">
          <thead>
            <tr>
              <th
                className="leaderboard-headings"
                style={{ textAlign: "left" }}
              >
                Rank
              </th>
              <th
                className="leaderboard-headings"
                style={{ textAlign: "left" }}
              >
                Person
              </th>
              <th className="leaderboard-headings">Total HeyBevans</th>
            </tr>
          </thead>
          {this.state.leaderboardData.map(leaderboard => (
            <tr>
              <td style={{ textAlign: "left" }}>{leaderboard.rank}</td>
              <td style={{ textAlign: "left" }}>
                <span id="leaderboard-person">
                  {leaderboard.image ? (
                    <img
                      id="mediumlogo"
                      alt="heybevans"
                      src={require(`./images/${leaderboard.image}`)}
                    />
                  ) : (
                    ""
                  )}
                  &nbsp;&nbsp;&nbsp;{leaderboard.name}{" "}
                </span>
              </td>
              <td>
                {leaderboard.totalBevans}
                <img id="smalllogo" alt="heybevans" src={bevans} />
              </td>
            </tr>
          ))}
        </table>
      </div>
    );
  }
}

export default Leaderboard;
