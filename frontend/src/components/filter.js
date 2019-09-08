import React, { Component } from "react";
import "./styles/filter.css";
import serviceFunc from "./../service/service";
import Configurations from './../config.json';
import { dateTypes } from './../global';

class Filter extends Component {
  state = {
    channels: [],
    channelSelected : '',
    typeSelected : 0,
    dateTypeSelected : 0
  };

  onTypeChange = (e) => {
    this.setState({ ...this.state, typeSelected: e.target.value })
    console.log(e.target.value);
    this.props.onChanged(this.state.typeSelected, this.state.dateTypeSelected, this.state.channelSelected);
  }

  onDateChanged = (e) => {
    this.setState({ ...this.state, dateTypeSelected: e.target.value })
    console.log(e.target.value);
    this.props.onChanged(this.state.typeSelected, this.state.dateTypeSelected, this.state.channelSelected);
  }

  onChannelChange = (e) => {
    this.setState({ ...this.state, channelSelected: e.target.value })
    console.log(e.target.value);
    this.props.onChanged(this.state.typeSelected, this.state.dateTypeSelected, this.state.channelSelected);
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
        <select id="dateType" onChange={this.onDateChanged} selected={this.state.dateTypeSelected} >
          {dateTypes.map((type, index) => (
            <option value={index}> { type } </option>
          )) }
        </select>
        <select id="channel" onChange={this.onChannelChange} selected={this.state.channelSelected}>
          {this.state.channels && this.state.channels.map(channel => (
            <option value={channel.Channel} >{channel.ChannelName}</option>
          ))}
        </select>
      </div>
    );
  }
}

export default Filter;
