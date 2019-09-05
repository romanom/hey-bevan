import React, { Component } from "react";
import Filter from "./filter";
import Activities from './activities';
import "./styles/content.css";

class ActivityContainer extends Component {
  state = {};
  render() {
    return (
      <div id="content-container">
        <table>
          <tr>
            <td>
              <table>
                <tr id="sub-header">
                  <td class="page-title">{this.props.title}</td>
                  <td>
                    <Filter />
                  </td>
                </tr>
                <tr>
                  <Activities />
                </tr>
              </table>
            </td>
          </tr>
        </table>
      </div>
    );
  }
}

export default ActivityContainer;
