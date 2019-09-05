import React, { Component } from 'react';
import SlackLogin from './slacklogin';

export default class LoginToSlack extends Component {
    state = {
        userId : "",
        userName: ""
    }

    onSetLoggedinUser = (id, name) => {
        this.setState({ userId : id , userName:  name});
    }

    render () {
        return (
            <p id="loginpage">
                Please sign in with your Slack account to access your team's HeyBevan! site.
                <SlackLogin onSetLoggedinUser={this.onSetLoggedinUser}/>
                Having problems signing in? Contact us.
            </p>
        );
    }
}




