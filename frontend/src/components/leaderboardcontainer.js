import React, { Component } from "react";
import Filter from "./filter";
import Leaderboard from "./leaderboard";
import Profile from "./profile";
import "./styles/leaderboardcontainer.css";

class LeaderboardContainer extends Component {
  state = {};
  render() {
    return (
      <div id="content-container">
        <table>
          <tbody>
            <tr>
              <td>

                <table>
                  <thead>
                    <tr id="sub-header">
                      <td className="page-title">{this.props.title}</td>
                      <td>
                        <Filter />
                      </td>
                    </tr>
                  </thead>
                  <tbody>
                    <tr>
                      <td><Leaderboard /></td>
                      <td><Profile /></td>
                    </tr>
                  </tbody>
                </table>

              </td>
            </tr>
          </tbody>
        </table>
      </div>
    );
  }
}

export default LeaderboardContainer;
