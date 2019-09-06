import React, { Component } from "react";
import "./styles/filter.css";
import serviceFunc from "./../service/service";
class Filter extends Component {
  state = {
    channels: []
  };

  async componentDidMount() {
    const channelResponse = await serviceFunc.getAllChannels();
    this.setState({ channels: channelResponse });
  }

  render() {
    const hide = this.props.only;
    return (
      <div>
        <span id="filter-heading">Filter</span>
        <select>
          <option>HeyBevan received</option>
          <option>HeyBevan sent</option>
        </select>
        <select>
          <option>Today</option>
          <option>Yesterday</option>
          <option>This week</option>
          <option>Last week</option>
          <option>This month</option>
          <option>Last month</option>
          <option>This year</option>
          <option>Last year</option>
        </select>
        <select>
          {this.state.channels && this.state.channels.map(channel => (
            <option>{channel.ChannelName}</option>
          ))}
        </select>
      </div>
    );
  }
}

export default Filter;
