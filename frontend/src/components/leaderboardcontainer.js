import React, { Component } from "react";
import Filter from "./filter";
import Leaderboard from "./leaderboard";
import Profile from "./profile";
import "./styles/leaderboardcontainer.css";

class LeaderboardContainer extends Component {
  state = {
    typeSelected : 0,
    dateSelected : 4,
    channelSelected : 'C6XKWUHAN'
  };

  onFilterChanged = (type, date, channel) =>{
    console.log('on filter changed ', type, date, channel);
    this.setState({typeSelected: type, dateSelected : date, channelSelected : channel});
  }

  render() {
    return (
      <div id="content-container">
        <table>
          <tbody>
            <tr>
              <td>
                <table>
                  <thead>
                    <tr id="sub-header">
                      <td className="page-title">{this.props.title}</td>
                      <td>
                        <Filter onChanged={this.onFilterChanged} />
                      </td>
                    </tr>
                  </thead>
                  <tbody>
                    <tr>
                      <td><Leaderboard 
                        leaderDataType={this.state.typeSelected} 
                        dateType={this.state.dateSelected} 
                        channel={this.state.channelSelected} />
                      </td>
                      <td><Profile /></td>
                    </tr>
                  </tbody>
                </table>
              </td>
            </tr>
          </tbody>
        </table>
      </div>
    );
  }
}

export default LeaderboardContainer;
