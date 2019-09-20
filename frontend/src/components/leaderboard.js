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

  async componentDidUpdate() {
    this.loadLeaderboardData();
  }
  
  loadLeaderboardData = async () => {
    if (this.props.leaderDataType >= 0 && this.props.dateType >= 0 && this.props.channel !== "")
    {
      console.log('Load data for ', this.props.leaderDataType, "  ", this.props.dateType, "  ", this.props.channel);
      const response = await serviceFunc.getLeaderboardData(
        this.props.leaderDataType,
        this.props.dateType,
        this.props.channel
      );
      this.setState({
        leaderboardData: response,
        loading : false
      });
    }
  }

  render() {
    if (this.state.leaderboardData) console.log(this.state.leaderboardData);
    return (
      this.state.loading ? <img id="spinimage" src={spinner}/> :
      <div className="scrollable">
        <table id="leaderboard">
            <tr>
              <th>Rank</th>
              <th>Person</th>
              <th>Total {Configurations.projectName}</th>
            </tr>
            {this.state.leaderboardData && this.state.leaderboardData.map((leaderboard, index) => (
              <tr>
                <td style={{ textAlign: "left" }}>{index+1}</td>
                <td>
                    {leaderboard.UserImage ? (
                      <img
                        id="mediumlogo"
                        alt={Configurations.projectName}
                        src={leaderboard.UserImage}
                      />
                    ) : (
                      ""
                    )}
                  {leaderboard.Name}{" "}
                </td>
                <td id="totalcolumn">
                  {leaderboard.total}
                  <img id="smalllogo" alt={Configurations.projectName} src={logo} />
                </td>
              </tr>
            ))}
        </table>
      </div>
    );
  }
}

export default Leaderboard;
