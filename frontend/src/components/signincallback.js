import React ,  {Component} from 'react';
import Configurations from './../config.json';
//import { WebClient } from '@slack/web-api';

export default class SignInCallback extends Component {
    render () {
        console.log(this.props.history.location.search);
        console.log(Configurations.slackClientId);
        //const client = new WebClient(Configurations.slackClientId);
        //client.api.

        const params = new URLSearchParams(this.props.history.location.search);
        const token = params.token;
        console.log(params, token);
        return (
            <div />
        );
    }
}