import React, { Component } from "react";
import "./styles/filter.css";
import serviceFunc from "./../service/service";
import Configurations from './../config.json';
import { dateTypes } from './../global';

class Filter extends Component {
  state = {
    channels: [],
    channelSelected : '',
    typeSelected : '',
    dateTypeSelected : 0
  };

  onTypeChange = (e) => {
    this.setState({ ...this.state, typeSelected: e.target.value })
    console.log(e.target.value);
  }

  onChannelChange = (e) => {
    this.setState({ ...this.state, channelSelected: e.target.value })
    console.log(e.target.value);
  }

  async componentDidMount() {
    const channelResponse = await serviceFunc.getAllChannels();
    this.setState({ channels: channelResponse });
  }

  render() {
    return (
      <div id="filter-container">
        <span id="filter-heading">Filter</span>
        <select id="type" onChane={this.onTypeChange}>
          <option>{Configurations.projectName} received</option>
          <option>{Configurations.projectName} sent</option>
        </select>
        <select id="dateType">
          {dateTypes.map((type, index) => (
            <option value={index}> { type } </option>
          )) }
        </select>
        <select id="channel" onChange={this.onChannelChange} selected={this.state.channelSelected}>
          {this.state.channels && this.state.channels.map(channel => (
            <option value={channel.ChannelId} >{channel.ChannelName}</option>
          ))}
        </select>
      </div>
    );
  }
}

export default Filter;
