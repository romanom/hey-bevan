import React, { Component } from "react";
import serviceFunc from "./../service/service";
import "./styles/profile.css";

class Profile extends Component {
  state = {
    userName : "",
    userImage: null,
    totalBevans : 0
  }
  componentDidMount(){
    const response = serviceFunc.getUserRedeemableTotal(1);
    this.setState({ userName : response.userName , userImage: response.userImage , totalBevans : response.totalBevans})
  }
  render() {
    return (
      <div>
        <p><img src={"this.state.userImage"}></img></p>
        <p>{this.state.userName}</p>
        <p>Redeemable HeyBevans</p>
        <p>{this.state.totalBevans}</p>
      </div>
    );
  }
}

export default Profile;
