import React ,  {Component} from 'react';
import QueryString from 'query-string';

export default class SignInCallback extends Component {
    render () {
        console.log(this.props.history.location.search);
        const params = QueryString.parse(this.props.history.location.search);
        console.log(params);
        return (
            <div />
        );
    }
}