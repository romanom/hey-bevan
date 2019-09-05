import React, { Component } from "react";
import Filter from "./filter";
import Leaderboard from "./leaderboard";
import Profile from "./profile";
import "./styles/content.css";

class Content extends Component {
  state = {};
  render() {
    return (
      <div id="content-container">
        <table>
          <tr>
            <td>
              <table>
                <tr id="sub-header">
                  <td className="page-title">{this.props.title}</td>
                  <td>
                    <Filter />
                  </td>
                </tr>
                <tr>
                  <Leaderboard />
                </tr>
              </table>
            </td>
          </tr>
        </table>
        <div>
          <Profile />
        </div>
      </div>
    );
  }
}
// PropTypes = {
//   title: string.isRequired
// };

export default Content;
