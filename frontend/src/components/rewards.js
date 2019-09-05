import React, { Component } from "react";
import "./styles/rewards.css";
import Assistant from "./images/reward-assistant.png";
import Coffee from "./images/reward-coffee.png";
import Dining from "./images/reward-dining.png";
import Pilot from "./images/reward-pilot.png";
import Pug from "./images/reward-pug.png";
class RewardPage extends Component {
  render() {
    return (
      <div className="rewards-container">
        <div className="rewards-card">
          <div className="rewards-img">
            <img src={Assistant}></img>
          </div>
          <div className="rewards-text">
            <div className="rewards-title">Personal Assistant Bevan</div>
            <div className="description">
              Bevan will be your PA for the day. Note that Bevan has no paid
              experience in this role.
            </div>
          </div>
        </div>
        <div className="rewards-card">
          <div className="rewards-img">
            <img src={Coffee}></img>
          </div>
          <div className="rewards-text">
            <div className="rewards-title">Personal Assistant Bevan</div>
            <div className="description">
              Bevan will be your PA for the day. Note that Bevan has no paid
              experience in this role.
            </div>
          </div>
        </div>
        <div className="rewards-card">
          <div className="rewards-img">
            <img src={Dining}></img>
          </div>
          <div className="rewards-text">
            <div className="rewards-title">Personal Assistant Bevan</div>
            <div className="description">
              Bevan will be your PA for the day. Note that Bevan has no paid
              experience in this role.
            </div>
          </div>
        </div>
        <div className="rewards-card">
          <div className="rewards-img">
            <img src={Pilot}></img>
          </div>
          <div className="rewards-text">
            <div className="rewards-title">Personal Assistant Bevan</div>
            <div className="description">
              Bevan will be your PA for the day. Note that Bevan has no paid
              experience in this role.
            </div>
          </div>
        </div>
        <div className="rewards-card">
          <div className="rewards-img">
            <img src={Pug}></img>
          </div>
          <div className="rewards-text">
            <div className="rewards-title">Personal Assistant Bevan</div>
            <div className="description">
              Bevan will be your PA for the day. Note that Bevan has no paid
              experience in this role.
            </div>
          </div>
        </div>
      </div>
    );
  }
}

export default RewardPage;
