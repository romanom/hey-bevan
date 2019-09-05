import React, { Component } from "react";
import "./styles/menu.css";
import { NavLink } from "react-router-dom";

class Menu extends Component {
  render() {
    return (
      <ul id="menu">
        <li>
          <NavLink
            to="/leaderboard"
            className="link-text"
            activeStyle={{ color: "gray" }}
          >
            Leaderboard
          </NavLink>
        </li>
        <li>
          <NavLink
            to="/activity"
            className="link-text"
            activeStyle={{ color: "gray" }}
          >
            Activity
          </NavLink>
        </li>
        <li>
          <NavLink
            to="/tags"
            className="link-text"
            activeStyle={{ color: "gray" }}
          >
            Tags
          </NavLink>
        </li>
        <li>
          <NavLink
            to="/rewards"
            className="link-text"
            activeClassName="selected"
          >
            Rewards
          </NavLink>
        </li>
      </ul>
    );
  }
}

export default Menu;
