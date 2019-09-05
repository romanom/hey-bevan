import React, { Component } from "react";
import "./styles/rewards.css";
import Assistant from "./images/reward-assistant.png";
import Coffee from "./images/reward-coffee.png";
import Dining from "./images/reward-dining.png";
import Pilot from "./images/reward-pilot.png";
import Pug from "./images/reward-pug.png";
import Profile from "./profile";
class RewardPage extends Component {
  render() {
    return (
      <div className="rewards-container">
        <div className="rewards-card">
          <div className="rewards-img">
            <img className="rewards-card" src={Assistant}></img>
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
            <img className="rewards-card" src={Coffee}></img>
          </div>
          <div className="rewards-text">
            <div className="rewards-title">Coffee with Bevan himself</div>
            <div className="description">
              Have you ever wanted to pick Bevan's brains? Now's your chance.
              Coffee not included.
            </div>
          </div>
        </div>
        <div className="rewards-card">
          <div className="rewards-img">
            <img className="rewards-card" src={Dining}></img>
          </div>
          <div className="rewards-text">
            <div className="rewards-title">
              Fine Dining Experience for Bevan
            </div>
            <div className="description">
              Want to give back to Bevan? Use your Bevans to give him a culinary
              experience to remember!
            </div>
          </div>
        </div>
        <div className="rewards-card">
          <div className="rewards-img">
            <img className="rewards-card" src={Pilot}></img>
          </div>
          <div className="rewards-text">
            <div className="rewards-title">Pilot Bevan</div>
            <div className="description">
              As a man of many talents, Bevan is able to pivot. He cannot pilot
              though. Good luck.
            </div>
          </div>
        </div>
        <div className="rewards-card">
          <div className="rewards-img">
            <img className="rewards-card" src={Pug}></img>
          </div>
          <div className="rewards-text">
            <div className="rewards-title">Bevansitting</div>
            <div className="description">
              Have a date night planned? Bevan will look after your pets (and
              children). Bevan takes no responsibility for any damaged goods.
            </div>
          </div>
        </div>
      </div>
    );
  }
}

export default RewardPage;
