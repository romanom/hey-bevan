import React, { Component } from 'react';
import Header from 'header';
import Footer from 'footer';


class HomePage extends Component {

  render() {
    return (
      <div classname="Homepage">
        <div classname="Homepage-header">
          <Header />
        </div>
        <Footer />
      </div>
    );
  }

}

export default HomePage;