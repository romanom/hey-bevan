import React, { Component } from "react";
import Header from "./components/header";
import "./App.css";
import Content from "./components/content";
import Footer from "./components/footer";

class App extends Component {
  render() {
    return (
      <div className="App">
        <div className="App-header">
          <Header />
        </div>
        <Content title="Leaderboard" />
        <Footer />
      </div>
    );
  }
}

export default App;
