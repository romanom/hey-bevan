import React, { Component } from "react";
import serviceFunc from "./../service/service";
import "./styles/profile.css";

class Profile extends Component {
  state = {
    userName: "",
    userImage: null,
    totalBevans: 0
  };
  componentDidMount() {
    const response = serviceFunc.getUserRedeemableTotal(1);
    this.setState({
      userName: response.userName,
      userImage: response.userImage,
      totalBevans: response.totalBevans
    });
  }
  render() {
    return (
      <div class="profile-container">
        <img src={"this.state.userImage"}></img>
        <p>{this.state.userName}</p>
        <p class="redeemable-text">Redeemable HeyBevans</p>
        <p class="no-padding">{this.state.totalBevans}</p>
      </div>
    );
  }
}

export default Profile;
