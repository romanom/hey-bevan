import React, { Component } from 'react';
import Bevan from "./images/logo.png";
import ReasonsWhyImage from "./images/BevansReasons.png";
import Rewards from "./images/BevanRewards.png";
import "./styles/home.css";

export default class Home extends Component {
    render() {
        return (
            <div id="homepage">
                <div>
                    <img
                        id="large-bevans-head"
                        alt="Bevans head"
                        src={Bevan}></img>
                    <div>
                        <h1 id="bevan-tagline">
                            Too often we get caught up in our work and forget to recognize each other, have a little fun, and celebrate. HeyBevan! is the answer.
                        </h1>
                    </div>
                </div>
                <div className="ReasonsWhyPoster">
                    <img id="reasonsWhyImage"
                        alt="Reasons Why you should give a Bevan!"
                        src={ReasonsWhyImage}>
                    </img>
                </div>
                <div className="RewardsPoster">
                    <img id="rewardsPoster"
                        alt="Rewards you can get from redeeming your Bevans"
                        src={Rewards}>
                    </img>
                </div>
            </div>
        );
    }
}