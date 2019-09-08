import React, { Component } from "react";
import serviceFunc from "./../service/service";
import "./styles/profile.css";
import Configurations from './../config.json';

class Profile extends Component {
  state = {
    userName: "",
    userImage: "",
    total: 0
  };
  componentDidMount() {
    const response = serviceFunc.getUserRedeemableTotal(1);
    this.setState({
      userName: response.userName,
      userImage: response.userImage,
      total: response.total
    });
  }
  render() {
    return (
      <div className="profile-container">
        {this.state.userImage ? (
          <img
            alt="user"
            className="profile-img"
            src={require(`./images/${this.state.userImage}`)}
          ></img>
        ) : (
          ""
        )}
        <p>{this.state.userName}</p>
        <p className="redeemable-text">Redeemable {Configurations.projectName} </p>
        <p className="no-padding">{this.state.total}</p>
      </div>
    );
  }
}

export default Profile;
