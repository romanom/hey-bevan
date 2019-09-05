import React, { Component } from "react";
import configurations from "./../config.json";

export default class SlackLogin extends Component{
    state = {
        loggedInUser : null
    };

    render() {
        return (
            <div>
                {
                    (this.state.loggedInUser === null) ?
                    <a id="slacklink" href={`https://slack.com/oauth/authorize?scope=identity.basic&client_id=${configurations.slackClientId}&redirect_uri=localhost:3000`}><img id="slackimage" src="https://api.slack.com/img/sign_in_with_slack.png" /></a> 
                    : <div><img src=""></img><p>{this.state.loggedInUser}</p> </div>
                }
            </div>
        );
    }
}