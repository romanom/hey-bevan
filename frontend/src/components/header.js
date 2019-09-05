import React, { Component } from "react";
import Menu from "./menu";
import Logo from "./images/logo_name.png";
import "./styles/header.css";
import SlackLogin from './slacklogin';

class Header extends Component {
  state = {};
  render() {
    return (
      <div id="header-container">
        <img id="logo" alt="logo" src={Logo} />
        <div id="menu-container">
          <Menu />
        </div>
        <SlackLogin />
    </div>
    );
  }
}

export default Header;
