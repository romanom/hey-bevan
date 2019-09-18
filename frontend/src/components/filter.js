import React, { Component } from "react";
import "./styles/filter.css";
import serviceFunc from "./../service/service";
import Configurations from './../config.json';
import { dateTypes } from './../global';

class Filter extends Component {
  state = {
    channels: [],
    channelSelected : 'C6XKWUHAN',
    typeSelected : 0,
    dateTypeSelected : 4
  };

  onTypeChange = (e) => {
    this.setState({ ...this.state, typeSelected: e.target.value },
      this.props.onChanged(e.target.value, this.state.dateTypeSelected, this.state.channelSelected));
  }

  onDateChange = (e) => {
    this.setState({ ...this.state, dateTypeSelected: e.target.value },
      this.props.onChanged(this.state.typeSelected, e.target.value, this.state.channelSelected))
  }

  onChannelChange(channel){
    this.setState({ ...this.state, channelSelected: channel },
      this.props.onChanged(this.state.typeSelected, this.state.dateTypeSelected, channel));
  }

  onChannelChangeEvent = (e) => {
    this.onChannelChange(e.target.value);
  }

  async componentDidMount() {
    const channelResponse = await serviceFunc.getAllChannels();
    console.log('Channel Response ', channelResponse);
    this.setState({ channels: channelResponse, channelSelected: channelResponse[1].Channel }, this.onChannelChange(channelResponse[1].Channel));
  }

  render() {
    return (
      <div id="filter-container">
        <span id="filter-heading">Filter</span>
        <select id="type" onChange={this.onTypeChange} defaultValue={this.state.typeSelected}>
          <option value="0">{Configurations.projectName} received</option>
          <option value="1">{Configurations.projectName} sent</option>
        </select>
        <select id="dateType" onChange={this.onDateChange} defaultValue={this.state.dateTypeSelected} >
          {dateTypes.map((type, index) => (
            <option value={index}> { type } </option>
          )) }
        </select>
        <select id="channel" onChange={this.onChannelChangeEvent} defaultValue={this.state.channelSelected}>
          {this.state.channels && this.state.channels.map(channel => (
            <option value={channel.Channel} >{channel.ChannelName}</option>
          ))}
        </select>
      </div>
    );
  }
}

export default Filter;
