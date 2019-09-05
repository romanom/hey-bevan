import React, { Component } from 'react';
import Header from 'header';
import Footer from 'footer';
import Bevan from "./images/logo.png";
import ReasonsWhyImage from "./images/BevansReasons.png";
import Rewards from "./images/BevansRewards.png";


class HomePage extends Component {

  render() {
    return (
      <div classname="Homepage">
        <div classname="Homepage-header">
          <Header />
        </div>
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
        <Footer />
      </div>
    );
  }

}

export default HomePage;