import axios from 'axios';

const dateTypes = {today : 0 , yesterday : 1, thisWeek : 2, lastWeek : 3, thisMonth : 4, lastMonth : 5, thisYear : 6, lastYear : 7, allTime: 8};
const BASEURL = " ";
const getLeaderboardData = ( type, dateType , channel) => {
    let startDate = new Date();
    let endDate = new Date();
    
    switch(dateType){
        case dateTypes.today : {
            startDate.setHours(0,0,0,0);
            endDate.setHours(23,59,59,999);
        }
        case dateTypes.yesterday : {
            startDate.setDate(startDate.getDate() - 1);
            endDate.setDate(endDate.getDate() - 1);
            startDate.setHours(0,0,0,0);
            endDate.setHours(23,59,59,999);
        }
        case dateTypes.thisWeek : {
            startDate = new Date(startDate.setDate(startDate.getDate() - startDate.getDay()));
            endDate = new Date(startDate.setDate(startDate.getDate() - startDate.getDay()+6));
        }
        case dateTypes.lastWeek : {

        }
        case dateTypes.thisMonth : {

        }
        case dateTypes.lastMonth : {

        }
        case dateTypes.thisYear : {

        }
        case dateTypes.lastYear : {

        }
        case dateTypes.allTime : {
        
        }
    }
    
    /*axios.get(`${BASEURL}/getleaderboards?bevanType=${type}&startdate=${startDate}&enddate=${endDate}&channel=${channel}`)
    .then(resource => {
        return resource;
    });*/

    const leaderboard = [
        {
            rank : 1,
            name : "Jp",
            userImage: "",
            totalBevans : 32
        },
        {
            rank : 2,
            name : "Melody",
            userImage : "",
            totalBevans : 20
        },
        {
            rank : 3,
            name : "Ivan",
            userImage: "",
            totalBevans : 15
        },
    ]
    return leaderboard;
}

const getUserRedeemableTotal = (userid) => {

    /*axios.get(`${BASEURL}/getuserredeemabletotal?userid=${userid}`)
    .then(resource => {
        return resource;
    });*/
    return {
        userName : "JP",
        userImage : "./../images/jp.png",
        totalBevans : 100
    };
}

const getChannelActivities = (channel) => {

    /*axios.get(`${BASEURL}/getchannelactivities?channel=${channel}`)
    .then(resource => {
        return resource;
    });*/
    return [
        {
            receiverName : "",
            totalBevans : "",
            giverName : "", 
            channel : "",
            datetime : "",
            message : ""
        }
    ];
}

const getAllChannels = () => {
    /*axios.get(`${BASEURL}/getchannels`)
    .then(resource => {
        return resource;
    });*/
    return [
        {
            name : "hackday-heybevan"
        },
        {
            name : "cr-apollo"
        },
        {
            name : "cr-hyperion"
        },
    ];
}

export default {
    getLeaderboardData,
    getAllChannels,
    getUserRedeemableTotal,
    getChannelActivities
};