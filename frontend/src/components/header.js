import React, { Component } from "react";
import Menu from "./menu";
import Logo from "./images/logo_name.png";
import "./styles/header.css";
import SlackLogin from "./slacklogin";

class Header extends Component {
  state = {};
  render() {
    return (
      <div class="myob-gradient white-text" id="header-main">
        <div id="header-container">
          <img id="logo" alt="logo" src={Logo} />
          <div id="menu-container">
            <Menu
              onClickActivities={this.props.onClickActivities}
              onClickLeaderboard={this.props.onClickLeaderboard}
              onClickTags={this.props.onClickTags}
            />
          </div>
        </div>
        <SlackLogin />
      </div>
    );
  }
}

export default Header;
