import React, { Component } from "react";
class LeaderboardRow extends Component {
  render() {
    return (
      <div>
        <tr>
          <td>{this.props.rank}</td>
          <td>
            <span>Image Placeholder</span>
            <span>{this.props.name}</span>
          </td>
          <td>
            <span>{this.props.count}</span>
            <span>img-placeholder</span>
          </td>
        </tr>
      </div>
    );
  }
}

export default LeaderboardRow;
