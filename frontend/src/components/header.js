import React, { Component } from "react";
import Menu from "./menu";
import Logo from "./images/logo_name.png";
import "./styles/header.css";
import SlackLogin from "./slacklogin";
import { Link } from 'react-router-dom';

class Header extends Component {
  state = {};
  render() {
    return (
      <div class="myob-gradient white-text" id="header-main">
        <div id="header-container">
          <Link to="/"><img id="logo" alt="logo" src={Logo} /></Link>
          <div id="menu-container">
            <Menu/>
          </div>
        </div>
        <SlackLogin />
      </div>
    );
  }
}

export default Header;
