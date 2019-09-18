import React, { Component } from "react";
// import Filter from "./filter";
import ActivityFilter from "./activitiesFilter";
import Activities from "./activities";
import "./styles/leaderboardcontainer.css";

class ActivityContainer extends Component {
  state = {
    channel : ''
  };

  onFilterChange = (channelSelected) => {
    console.log('channel on filter change in activity container ', channelSelected);
    this.setState({ channel : channelSelected });
  }

  render() {
    return (
      <div id="content-container">
        <table>
          <thead>
            <tr id="sub-header">
              <td className="page-title">Activities</td>
              <td>
                <ActivityFilter onFilterChange={this.onFilterChange} />
              </td>
            </tr>
          </thead>
          <Activities channel={this.state.channel} />
        </table>
      </div>
    );
  }
}

export default ActivityContainer;
