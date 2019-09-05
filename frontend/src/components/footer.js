import React, { Component } from "react";
import "../App.css";
import MYOB from "./images/myoblogo.png";
import "./styles/footer.css";
class Footer extends Component {
  render() {
    return (
      <div class="myob-gradient" id="footer">
        <img alt="myoblogo" class="myob-logo" src={MYOB}></img>
        <ul className="footer-menu">
          <li>
            <a>How it works</a>
          </li>
          <li>
            <a>FAQ</a>
          </li>
          <li>
            <a>Support</a>
          </li>
        </ul>
      </div>
    );
  }
}

export default Footer;
