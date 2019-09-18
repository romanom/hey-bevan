import React, { Component } from "react";
import serviceFunc from "./../service/service";
import "./styles/filter.css";
class ActivityFilter extends Component {
  state = {
    channels: [],
    channelSelected : ''
  };
  
  async componentDidMount() {
    this.setState({ channels: await serviceFunc.getAllChannels() });
  }

  onChannelChange = (e) => {
    console.log('seleted channel ', e.target.value);
    this.setState({ channelSelected: e.target.value }, this.props.onFilterChange(e.target.value) );
  }

  render() {
    return (
      <div>
        <span id="filter-heading">Filter</span>
        {this.state.channels ?
          <select onChange={this.onChannelChange} selected={this.state.channelSelected}>
            {this.state.channels.map(channel => (
              <option value={channel.Channel} >{channel.ChannelName}</option>
            ))}
          </select> : ''
        }
      </div>
    );
  }
}
export default ActivityFilter;
