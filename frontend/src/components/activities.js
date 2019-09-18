import React, { Component } from "react";
import serviceFunc from "./../service/service";
import "./styles/leaderboardcontainer.css";
import Logo from "./images/logo.png";

class Activities extends Component {
  state = {
    activities: [],
    channel : ''
  };

  async componentDidUpdate() {
    console.log(' props channel in activities ', this.props.channel);
    if (this.props.channel !== this.state.channel){
      var channelActivities = await serviceFunc.getChannelActivities(this.props.channel)
      this.setState({
        activities:  channelActivities, channel : this.props.channel
      });  
    }
  }

  getListOfImages = (count) => {
    let images = [];
    for (let i = 0; i< count ; i++){
      images.push(<img id="smalllogo" src={Logo} />);
    }
  return images;
  }

  render() {
    return (
      <tbody>
        {this.state.activities.map(activity => (
          <tr>
            <td>
            {" "}
            <span className="activity-name">
              {activity.ReceiverId}{" "}
            </span>
            received {activity.Count} HeyBevans{" "}
            { this.getListOfImages(activity.Count) }
            from <span className="activity-name">{activity.GiverId} </span>
            <span className="timestamp">{activity.timestamp}</span>
            <p>{activity.Message} </p>
            </td>
          </tr>
        ))}
      </tbody>
    );
  }
}

export default Activities;
