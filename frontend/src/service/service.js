import axios from "axios";

const dateTypes = {
  today: 0,
  yesterday: 1,
  thisWeek: 2,
  lastWeek: 3,
  thisMonth: 4,
  lastMonth: 5,
  thisYear: 6,
  lastYear: 7,
  allTime: 8
};
const BASEURL = " ";
const getLeaderboardData = async (type, dateType, channel) => {
  let startDate = new Date();
  let endDate = new Date();

  switch (dateType) {
    case dateTypes.today: {
      startDate.setHours(0, 0, 0, 0);
      endDate.setHours(23, 59, 59, 999);
      break;
    }
    case dateTypes.yesterday: {
      startDate.setDate(startDate.getDate() - 1);
      endDate.setDate(endDate.getDate() - 1);
      startDate.setHours(0, 0, 0, 0);
      endDate.setHours(23, 59, 59, 999);
      break;
    }
    case dateTypes.thisWeek: {
      startDate = new Date(
        startDate.setDate(startDate.getDate() - startDate.getDay())
      );
      endDate = new Date(
        startDate.setDate(startDate.getDate() - startDate.getDay() + 6)
      );
      break;
    }
    case dateTypes.lastWeek: {
      break;
    }
    case dateTypes.thisMonth: {
      break;
    }
    case dateTypes.lastMonth: {
      break;
    }
    case dateTypes.thisYear: {
      break;
    }
    case dateTypes.lastYear: {
      break;
    }
    case dateTypes.allTime: {
      break;
    }
    default: {
      break;
    }
  }

  const response = await axios.get('https://api.hey-bevan.com/LeaderBoard');
  console.log('Leaderboard ', response.data);
  return response.data;
};

const getUserRedeemableTotal = userid => {
  /*axios.get(`${BASEURL}/getuserredeemabletotal?userid=${userid}`)
    .then(resource => {
        return resource;
    });*/
  return {
    userName: "Jesse Paclar",
    userImage: "jp.png",
    totalBevans: 100
  };
};

const getChannelActivities = async (channel) => {
  await axios.get(`${BASEURL}/getchannelactivities?channel=${channel}`)
    .then(resource => {
        return resource;
    });

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
    const response = await axios.get('https://api.hey-bevan.com/Channels');
    return response.data;
}

export default {
  getLeaderboardData,
  getAllChannels,
  getUserRedeemableTotal,
  getChannelActivities
};
