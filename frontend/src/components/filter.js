import React, { Component } from "react";
import "./styles/filter.css";
import serviceFunc from "./../service/service";
class Filter extends Component {
  state = {
    channels: []
  };

  componentDidMount() {
    this.setState({ channels: serviceFunc.getAllChannels() });
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
          {this.state.channels.map(channel => (
            <option>{channel.name}</option>
          ))}
        </select>
      </div>
    );
  }
}

export default Filter;
