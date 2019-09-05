import React, { Component } from "react";
import "./styles/menu.css";
import { Link} from 'react-router-dom';

class Menu extends Component {
  render() {
    return (
      <ul id="menu">
        <li><Link to="/leaderboard">Leaderboard</Link></li>
        <li><Link to="/activity">Activity</Link></li>
        <li><Link to="/tags">Tags</Link></li>
      </ul>
    );
  }
}

export default Menu;
