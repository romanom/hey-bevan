import React, { Component } from "react";
import Header from "./components/header";
import "./App.css";
import Content from "./components/content";
import ActivityContainer from "./components/activitycontainer";
import Tags from "./components/tags";
import Footer from "./components/footer";

class App extends Component {
  state = {
    showLeaderboard : true,
    showActivities : false,
    showTags: false
  }

  onClickActivities = () => {
    this.setState({ showActivities : true, showLeaderboard: false, showTags: false});
  }

  onClickLeaderboard = () => {
    this.setState({ showLeaderboard : true, showActivities: false, showTags: false});
  }

  onClickTags = () => {
    this.setState({ showTags : true, showActivities: false, showLeaderboard: false});
  }

  render() {
    return (
      <div className="App">
        <div className="App-header">
          <Header 
            onClickActivities={this.onClickActivities} 
            onClickLeaderboard= {this.onClickLeaderboard}
            onClickTags={this.onClickTags}  
          />
        </div>
        { this.state.showLeaderboard && <Content title="Leaderboard" />}
        { this.state.showActivities && <ActivityContainer title="Activities" />}
        { this.state.showTags && <Tags title="Tags" />}
        <Footer />
      </div>
    );
  }
}

export default App;
