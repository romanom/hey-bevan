import React, { Component } from "react";
import serviceFunc from "./../service/service";

class Profile extends Component {
  state = {
    userName : "",
    totalBevans : 0
  }
  componentDidMount(){
    const response = serviceFunc.getUserRedeemableTotal(1);

    this.setState({ userName : response.userName , totalBevans : response.totalBevans})
  }
  render() {
    return (
      <div>
        <p>{this.props.userImage}</p>
        <p>{this.props.userName}</p>
        <p>Redeemable Hey-Bevans</p>
        <p>{this.props.totalBenavs}</p>
      </div>
    );
  }
}

export default Profile;
