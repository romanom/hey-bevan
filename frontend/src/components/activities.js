import React, { Component } from "react";
import serviceFunc from "./../service/service";
import "./styles/leaderboardcontainer.css";
import Logo from "./images/logo.png";

class Activities extends Component {
  state = {
    activities: []
  };

  async componentDidMount() {
    this.setState({
      activities: await serviceFunc.getChannelActivities("cr-hyperion")
    });
  }

  render() {
    return (
      <div id="content-container">
        <table>
          <tr>
            <td>
              <table>
                {this.state.activities.map(activity => (
                  <tr>
                    {" "}
                    <span className="activity-name">
                      {activity.receiverName}{" "}
                    </span>
                    received {activity.count} HeyBevans{" "}
                    <img id="smalllogo" src={Logo} />
                    from {activity.giverName}
                    in {activity.channel}{" "}
                    <span className="timestamp">{activity.timestamp}</span>
                    <p>{activity.message} </p>
                  </tr>
                ))}
              </table>
            </td>
          </tr>
        </table>
      </div>
    );
  }
}

export default Activities;
