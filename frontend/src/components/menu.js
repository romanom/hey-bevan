import React, { Component } from "react";
import "./styles/menu.css";
class Menu extends Component {
  render() {
    return (
      <ul id="menu">
        <li>
          <a onClick={this.props.onClickLeaderboard}>Leaderboard</a>
        </li>
        <li>
          <a onClick={this.props.onClickActivities}>Activity</a>
        </li>
        <li>
          <a onClick={this.props.onClickTags}>Tags</a>
        </li>
      </ul>
    );
  }
}

export default Menu;
