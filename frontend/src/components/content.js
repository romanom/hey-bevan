import React, { Component } from "react";
import Filter from "./filter";
import Leaderboard from "./leaderboard";
import Profile from "./profile";
import "./styles/content.css";

class Content extends Component {
  state = {};
  render() {
    return (
      <div>
        <div id="sub-header">
          <div id="content-title">
            <h3>{this.props.title}</h3>
          </div>
          <div id="filter-container">
            <Filter />
          </div>
        </div>
        <div id="content-container">
          <div>
            <Leaderboard />
          </div>
          <div>
            <Profile userName="jp" total="200" />
          </div>
        </div>
        {/* <table>
          <tr>
            <td id="content-container">
              <table>
                <tr id="sub-header">
                  <td>{this.props.title}</td>
                  <td>
                    <Filter />
                  </td>
                </tr>
                <tr>
                  <Leaderboard />
                </tr>
              </table>
            </td>
            <td>
              <Profile userName="jp" total="200" />
            </td>
          </tr>
        </table> */}
      </div>
    );
  }
}
// PropTypes = {
//   title: string.isRequired
// };

export default Content;
