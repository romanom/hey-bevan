import React, { Component } from 'react';
import Header from 'header';
import Footer from 'footer';
import Bevan from "./images/logo.png";


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
        <div className="ReasonsWhyPoster"></div>
        <Footer />
      </div>
    );
  }

}

export default HomePage;