import React, { Component } from "react";
import Header from "./components/header";
import "./App.css";
import Content from "./components/content";
import Tags from "./components/tags";
import Footer from "./components/footer";
import { BrowserRouter as Router, Route, Link } from "react-router-dom";
import SignInCallback from "./components/signincallback";
import Home from './components/home';

class App extends Component {
    
  render() {
  return (
      <Router>
        <div className="App">
          <Header />
          <Route exact path= "/" component= { Home } />
          <Route path= "/leaderboard" component= { Content } />
          <Route path="/signincallback" component={ SignInCallback } />
          <Route path="/Tags" component={ Tags } />
          <Footer />
        </div>
      </Router>
    );
  }
}

export default App;
