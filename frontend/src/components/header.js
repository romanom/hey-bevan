import React, { Component } from "react";
import Menu from "./menu";
import Logo from "./heybevan-removebg.png";
class Header extends Component {
  state = {};
  render() {
    return (
      <div>
        <img id="logo" alt="logo" src={Logo} />
        <Menu />
      </div>
    );
  }
}

export default Header;
