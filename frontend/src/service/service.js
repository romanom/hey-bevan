import axios from 'axios';

const getLeaderboardData = ( type, datetype , channel) => {
    /*axios.get("")
    .then(resource => {

    })*/
    const leaderboard = [
        {
            rank : 1,
            name : "Jp",
            total : 32
        },
        {
            rank : 2,
            name : "Melody",
            total : 20
        },
        {
            rank : 3,
            name : "Ivan",
            total : 15
        },
    ]
    return leaderboard;
}

export default getLeaderboardData;