import React, { Component } from "react";
class Profile extends Component {
  render() {
    return (
      <div>
        <p>Image</p>
        <p>{this.props.userName}</p>
        <p>Redeemable Hey-Bevans</p>
        <p>{this.props.total}</p>
      </div>
    );
  }
}

export default Profile;
