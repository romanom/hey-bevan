import React, { Component } from "react";
import serviceFunc from "./../service/service";
import "./styles/filter.css";
class ActivityFilter extends Component {
  state = {
    channels: []
  };
  componentDidMount() {
    this.setState({ channels: serviceFunc.getAllChannels() });
  }
  render() {
    return (
      <div>
        <span id="filter-heading">Filter</span>
        <select>
          {this.state.channels.map(channel => (
            <option>{channel.name}</option>
          ))}
        </select>
      </div>
    );
  }
}
export default ActivityFilter;
