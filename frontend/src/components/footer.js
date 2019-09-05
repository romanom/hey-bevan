import React, { Component } from "react";
import "../App.css";
import MYOB from "./images/myoblogo.png";
import "./styles/footer.css";
class Footer extends Component {
  render() {
    return (
      <div id="footer">
        <img class="myob-logo" src={MYOB}></img>
        <ul class="footer-menu">
          <li>How it works</li>
          <li>FAQ</li>
          <li>Support</li>
        </ul>
      </div>
    );
  }
}

export default Footer;
