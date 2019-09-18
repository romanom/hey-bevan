import React ,  {Component} from 'react';
import  { Redirect } from 'react-router-dom';

export default class SignInCallback extends Component {
    render () {
        const params = new URLSearchParams(this.props.history.location.search);
        const token = params.get('token');
        
        console.log('url params and token ', params.toString(), token);
        // this.props.history.redirect('slack.com/api/users.identity?token='+ token);
        return (
            <div>
            { token !== '' && token !== undefined ?
                <Redirect to='/Leaderboard' /> : ''
            }
            </div>
        );
    }
}