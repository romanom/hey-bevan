import React, {Component} from "react";
import serviceFunc from "./../service/service";

class Filter extends Component {
    state = {
        channels : []
    };

    componentDidMount() {
        this.setState({channels: serviceFunc.getAllChannels()})
    }

    render() {
        return (
            <div>
                <span>
                    Filter
                </span>
                <select>
                    {this.state.channels.map(channel => 
                    (<options>{channel.name}</options>)
                    )}
                </select>
            </div>
        )
    }
}