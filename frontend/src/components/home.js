import React, { Component } from 'react';
import Bevan from "./images/logo.png";
import Achievement from "./images/reason-achievement.png";
import Easy from "./images/reason-easy.png"
import Happy from "./images/reason-happiness.png"
import Rewards from "./images/reason-rewards.png"
import Slack from "./images/reason-slack.png"
import ReasonsWhyImage from "./images/BevansReasons.png";
import RewardsPoster from "./images/BevanRewards.png";
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
                <div className="flex-container">
                    <div className="reason-jumbotron">
                        <h1>5 REASONS TO GIVE BEVANS</h1>
                    </div>
                    <div className="reason-card-2">
                        <img src={Achievement} alt="Achievment Icon" />
                        <div class="container">
                            <h4><b>Celebrate An Achievement</b></h4>
                            <p>Boost morale by recognising your team's achievements and effort</p>
                        </div>
                    </div>
                    <div className="reason-card-2">
                        <img src={Easy} alt="Easy Icon" />
                        <div class="container">
                            <h4><b>Easy to Use</b></h4>
                            <p>It only takes a few seconds to tag your team and give a Bevan</p>
                        </div>
                    </div>
                    <div className="reason-card-2">
                        <img src={Happy} alt="Happiness Icon" />
                        <div class="container">
                            <h4><b>Make Someone's Day</b></h4>
                            <p>Spread extra happiness at MYOB</p>
                        </div>
                    </div>
                    <div className="reason-card-2">
                        <img src={Rewards} alt="Rewards Icon" />
                        <div class="container">
                            <h4><b>Trade in for Rewards</b></h4>
                            <p>Redeem your Bevans for awesome prizes</p>
                        </div>
                    </div>
                    <div className="reason-card-1">
                        <img src={Slack} alt="Slack Icon" />
                        <div class="container">
                            <h4><b>Integrated with Slack</b></h4>
                            <p>Use Bevans through your existing Slack channels for free! Made with love using AWS Lambda, Amazon DynamoDB, .NET Core, React, and Bevan's beautiful face</p>
                        </div>
                    </div>
                    <div className="reason-jumbotron">
                        <h1>For more information, talk to your local Bevan expert</h1>
                        <p>Learn more at ui.hey-bevan.com</p>
                    </div>
                </div>
            </div>
        );
    }
}