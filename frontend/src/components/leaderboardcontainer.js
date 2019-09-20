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
        <table id="header-table">
          <thead>
            <th className="page-title">{this.props.title}</th>
            <th className="filter"><Filter onChanged={this.onFilterChanged} /></th>
          </thead>
        </table>
        
        <table id="body-table">
          <tr>
              <td className="leader-col">
                <Leaderboard 
                    leaderDataType={this.state.typeSelected} 
                    dateType={this.state.dateSelected} 
                    channel={this.state.channelSelected} />
              </td>
              <td className="profile-col"><Profile /></td>
          </tr>
        </table>
      </div>
    );
  }
}

export default LeaderboardContainer;
