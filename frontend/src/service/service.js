import axios from "axios";
import { getDateRange } from './../global';
import { BASEURL } from './../global';

const getLeaderboardData = async (type, dateType, channel) => {
  const dateRange = getDateRange(dateType);
  console.log(dateRange.startDate, dateRange.endDate);

  const response = await axios.get(`${BASEURL}/LeaderBoard`);
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
    const response = await axios.post(`${BASEURL}/GetByChanneLId`, {
        "ChannelId": channel
    });
    console.log('Channel Activities ', response);
    //return response ;
  return [
    {
      bevanId: "",
      receiverName: "Jesse Paclar",
      giverName: "Sinu Sudhakaran",
      count: 2,
      channel: "cr-hyperion",
      timestamp: "12:05PM",
      message: "You are awesome Jess"
    },
    {
      bevanId: "",
      receiverName: "Ivan Perevernykhata",
      giverName: "Melody Reyes",
      count: 3,
      channel: "cr-hyperion",
      timestamp: "11:05AM",
      message: "You are legend Ivan"
    },
    {
      bevanId: "",
      receiverName: "Ivan Perevernykhata",
      giverName: "Bevan",
      count: 3,
      channel: "cr-hyperion",
      timestamp: "10:28AM",
      message: "Bevan is a legend"
    }
  ];
};

const getAllChannels = async () => {
    const response = await axios.get(`${BASEURL}/Channels`);
    console.log('Channels from api ', response);
    return response.data;
}

export default {
  getLeaderboardData,
  getAllChannels,
  getUserRedeemableTotal,
  getChannelActivities
};
