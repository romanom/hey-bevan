import React, { Component } from "react";
import Assistant from "./images/reward-assistant.png";
import Coffee from "./images/reward-assistant.png";
import Dining from "./images/reward-assistant.png";
import Pilot from "./images/reward-assistant.png";
import Pug from "./images/reward-assistant.png";
class RewardPage extends Component {
  render() {
    return (
      <div className="reward-container">
        <div className="card">
          <div className="reward-img">
            <img src={Assistant}></img>
          </div>
          <div className="description"></div>
        </div>
      </div>
    );
  }
}

export default RewardPage;
