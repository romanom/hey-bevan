import React, { Component } from 'react';
import Bevan from "./images/logo.png";
import ReasonsWhyImage from "./images/BevansReasons.png";
import Rewards from "./images/BevanRewards.png";

export default class Home extends Component {
    render() {
        return (
            <div classname="Homepage">
                <div className="BevansHead">
                    <img
                        id="largeBevansHead"
                        alt="Bevans head"
                        src={Bevan}></img>
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