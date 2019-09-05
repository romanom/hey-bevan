import React, { Component } from "react";
import Filter from "./filter";
import "./styles/content.css";

class Activities extends Component {
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
                </tr>
                <tr>
                  
                </tr>
              </table>
            </td>
          </tr>
        </table>
      </div>
    );
  }
}

export default Activities;
