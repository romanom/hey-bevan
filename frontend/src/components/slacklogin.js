import React, { Component } from "react";
import configurations from "./../config.json";
//import { WebClient } from '@slack/web-api';

export default class SlackLogin extends Component{
    state = {
        loggedInUser : null
    };

    render() {
        console.log("Slack Login");
        console.log('Logged in user ', this.state.loggedInUser);
        //const token = "xoxb-2315277109-749184996816-mTCSqqL485bJl3SLSmEeVVL5";
        return (
            <div id="slackImageHolder">
                {
                    (this.state.loggedInUser === null) ?
                    <a id="slacklink" href={`https://slack.com/oauth/authorize?scope=identity.basic&client_id=${configurations.slackClientId}&redirect_uri=http://localhost:3000`}><img id="slackimage" src="https://api.slack.com/img/sign_in_with_slack.png" /></a> 
                    : <div><p>user image</p><p>{this.state.loggedInUser}</p> </div>
                }
            </div>
        );
    }
}