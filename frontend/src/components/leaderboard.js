import React, { Component } from "react";
import serviceFunc from "./../service/service";
import "./styles/leaderboard.css";
import bevans from "./images/logo.png";

class Leaderboard extends Component {
  state = {
    leaderboardData: []
  };

  async componentDidMount() {
    const response = await serviceFunc.getLeaderboardData(
      this.props.type,
      this.props.dateType,
      this.props.channel
    );
    this.setState({
      leaderboardData: response
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
          <tbody>
            {this.state.leaderboardData && this.state.leaderboardData.map((leaderboard, index) => (
              <tr>
                <td style={{ textAlign: "left" }}>{index+1}</td>
                <td style={{ textAlign: "left" }}>
                  <span id="leaderboard-person">
                    {leaderboard.UserImage ? (
                      <img
                        id="mediumlogo"
                        alt="heybevans"
                        src={leaderboard.UserImage}
                      />
                    ) : (
                      ""
                    )}
                    &nbsp;&nbsp;&nbsp;{leaderboard.Name}{" "}
                  </span>
                </td>
                <td>
                  {leaderboard.TotalBevans}
                  <img id="smalllogo" alt="heybevans" src={bevans} />
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    );
  }
}

export default Leaderboard;
