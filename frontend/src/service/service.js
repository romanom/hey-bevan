import axios from "axios";
import { getDateRange } from './../global';
import { BASEURL } from './../global';

const getAllChannels = async () => {
  const response = await axios.get(`${BASEURL}/Channels`);
  console.log('Channels from api ', response);
  return response.data;
}

const getLeaderboardData = async (type, dateType, channel) => {
  console.log('Date option selected ', dateType);
  const dateRange = getDateRange(dateType);
  console.log(dateRange.startDate.toISOString() , dateRange.endDate.toISOString() );
  var postData = {
    Type : type,
    StartDate : dateRange.startDate.toISOString() ,
    EndDate : dateRange.endDate.toISOString() ,
    Channel : channel
  };
  const response = await axios.post(`${BASEURL}/LeaderBoard`, postData);
  //const response = await axios.get(`${BASEURL}/LeaderBoard`);
  const modifiedResponse = [];
  response.data.map(record => (modifiedResponse.push({ ...record, total: record.TotalBevans})))
  console.log('Leaderboard ', modifiedResponse);
  return modifiedResponse;
};

const getUserRedeemableTotal = async userid => {
  const response = await axios.post(`${BASEURL}/Redeemable`, {
    "ReceiverId": userid
    });
    console.log('Redeemable total ', response.data);
  const modifiedResponse = [];
  response.data.map(record => (modifiedResponse.push({ ...record, total: record.TotalBevans})))
  return modifiedResponse;
};

const getChannelActivities = async (channel) => {
    const response = await axios.post(`${BASEURL}/GetActivitiesByChannelId`, {
        "ChannelId": channel
    });
    console.log('Channel Activities ', response);
    return response.data ;
};

export default {
  getLeaderboardData,
  getAllChannels,
  getUserRedeemableTotal,
  getChannelActivities
};
