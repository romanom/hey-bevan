import React, { Component } from "react";
import Jp from "./images/jp.png";
import "./styles/profile.css";
class Profile extends Component {
  render() {
    return (
      <div>
        <img class="profile-pic" src={Jp}></img>
        <p>{this.props.userName}</p>
        <p>Redeemable HeyBevans</p>
        <p>{this.props.total}</p>
      </div>
    );
  }
}

export default Profile;
