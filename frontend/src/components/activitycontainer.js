import React, { Component } from "react";
// import Filter from "./filter";
import ActivityFilter from "./activitiesFilter";
import Activities from "./activities";
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
                <thead>
                  <tr id="sub-header">
                    <td className="page-title">Activities</td>
                    <td>
                      <ActivityFilter />
                    </td>
                  </tr>
                </thead>
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
