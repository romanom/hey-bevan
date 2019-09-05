import React, { Component } from "react";
import Filter from "./filter";
import Leaderboard from "./leaderboard";
import Profile from "./profile";

class Content extends Component {
  state = {};
  render() {
    return (
      <div>
        <table>
          <tr>
            <td>
              <table>
                <tr>
                  {this.props.title}
                  <Filter />
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
        </table>
      </div>
    );
  }
}
// PropTypes = {
//   title: string.isRequired
// };

export default Content;
