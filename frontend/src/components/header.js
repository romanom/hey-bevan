import React, { Component } from "react";
import Menu from "./menu";
import Logo from "./heybevan-removebg.png";
import "./styles/header.css";
class Header extends Component {
  state = {};
  render() {
    return (
      <div id="header-container">
        <img id="logo" alt="logo" src={Logo} />
        <div id="menu-container">
          <Menu />
        </div>
      </div>
    );
  }
}

export default Header;
