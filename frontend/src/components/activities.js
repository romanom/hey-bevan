import React, { Component } from "react";
import serviceFunc from "./../service/service";
import "./styles/content.css";

class Activities extends Component {
  state = {
    activities : []
  };

  componentDidMount() {
    this.setState({ activities : serviceFunc.getChannelActivities('cr-hyperion')});
  }

  render() {
    return (
      <div id="content-container">
        <table>
          <tr>
            <td>
              <table>
                <tr id="sub-header">
                  <td className="page-title">{this.props.title}</td>
                </tr>
                <tr>Activities</tr>
                {this.state.activities.map(activity => (
                  <tr> {activity.receiverName} received {activity.count} hey-bevans 
                  from {activity.giverName}
                  in {activity.channel} {activity.timestamp}
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
