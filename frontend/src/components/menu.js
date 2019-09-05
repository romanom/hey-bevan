import React, { Component } from "react";
import "./styles/menu.css";
class Menu extends Component {
  render() {
    return (
      <ul id="menu">
        <li>Leaderboard</li>
        <li>Activity</li>
        <li>Tags</li>
      </ul>
    );
  }
}

export default Menu;
