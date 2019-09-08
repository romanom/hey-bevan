import React, { Component } from "react";
import Header from "./components/header";
import "./App.css";
import LeaderboardContainer from "./components/leaderboardcontainer";
import Tags from "./components/tags";
import Footer from "./components/footer";
import { BrowserRouter as Router, Route } from "react-router-dom";
import SignInCallback from "./components/signincallback";
import Home from "./components/home";
import ActivityContainer from "./components/activitycontainer";
import RewardPage from "./components/rewards";

class App extends Component {
  render() {
    return (
      <Router>
        <div className="App">
          <Header />
          <Route exact path="/" component={Home} />
          <Route
            path="/leaderboard"
            render={props => <LeaderboardContainer {...props} title="Leaderboard" />}
          />
          <Route path="/tokenExchangeRedirection" component={SignInCallback} />
          <Route path="/Activity" component={ActivityContainer} />
          <Route path="/Tags" component={Tags} />
          <Route path="/Rewards" component={RewardPage} />
          <Footer />
        </div>
      </Router>
    );
  }
}

export default App;
