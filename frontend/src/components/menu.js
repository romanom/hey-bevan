import React, { Component } from "react";
import "./styles/menu.css";
import { Link } from "react-router-dom";

class Menu extends Component {
  render() {
    return (
      <ul id="menu">
        <li>
          <Link to="/leaderboard" className="link-text">
            Leaderboard
          </Link>
        </li>
        <li>
          <Link to="/activity" className="link-text">
            Activity
          </Link>
        </li>
        <li>
          <Link to="/tags" className="link-text">
            Tags
          </Link>
        </li>
      </ul>
    );
  }
}

export default Menu;
