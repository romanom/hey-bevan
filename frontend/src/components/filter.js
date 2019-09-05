import React, { Component } from "react";
import "./styles/filter.css";
class Filter extends Component {
  state = {};
  render() {
    return (
      <div>
        <span id="filter-heading">Filter</span>
        <select>
          <option>Hey-Bevan Received</option>
          <option> Hey-Bevan Sent</option>
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
          <option>All channel</option>
          <option>cr-apollo</option>
          <option>cr-hyperion</option>
        </select>
      </div>
    );
  }
}

export default Filter;
