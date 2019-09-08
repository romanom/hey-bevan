import React, { Component } from "react";
import serviceFunc from "./../service/service";
import "./styles/leaderboard.css";
import logo from "./images/logo.png";
import Configurations from './../config.json';
import spinner from './images/spin.gif';

class Leaderboard extends Component {
  state = {
    leaderboardData: [],
    loading : true
  };

  async componentDidMount() {
    const response = await serviceFunc.getLeaderboardData(
      this.props.type,
      this.props.dateType,
      this.props.channel
    );
    this.setState({
      leaderboardData: response,
      loading : false
    });
  }

  render() {
    if (this.state.leaderboardData) console.log(this.state.leaderboardData);
    return (
      this.state.loading ? <img id="spinimage" src={spinner}/> :
      <div className="scrollable">
        <table id="leaderboard">
          <thead >
            <tr >
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
              <th className="leaderboard-headings">Total {Configurations.projectName}</th>
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
                        alt={Configurations.projectName}
                        src={leaderboard.UserImage}
                      />
                    ) : (
                      ""
                    )}
                    &nbsp;&nbsp;&nbsp;{leaderboard.Name}{" "}
                  </span>
                </td>
                <td id="totalcolumn">
                  {leaderboard.total}
                  <img id="smalllogo" alt={Configurations.projectName} src={logo} />
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
