export const dateTypes = [
    "Today" ,
    "Yesterday",
    "This Week",
    "Last Week",
    "This Month",
    "Last Month",
    "This Year",
    "Last Year",
    "All Time"
];

export const getDateRange = (dateType) => {
    let dateRange = {
        startDate : new Date(),
        endDate : new Date()
    };
    switch (dateType) {
        case "1": { //yesterday
            dateRange.startDate.setDate(dateRange.startDate.getDate() - 1);
            dateRange.endDate.setDate(dateRange.endDate.getDate() - 1);
          break;
        }
        case "2": { //this week
          dateRange.startDate.setDate(dateRange.startDate.getDate() - dateRange.startDate.getDay());
          dateRange.endDate.setDate(dateRange.startDate.getDate() + 6);
          break;
        }
        case "3": { //last week
          dateRange.endDate.setDate(dateRange.startDate.getDate() - dateRange.startDate.getDay() - 1);
          dateRange.startDate.setDate(dateRange.endDate.getDate() - 6);
        break;
        }
        case "4": { //this month
          dateRange.startDate = new Date(dateRange.startDate.getFullYear(), dateRange.startDate.getMonth(), 1);
          dateRange.endDate = new Date(dateRange.startDate.getFullYear(), dateRange.startDate.getMonth() + 1, 0);
          break;
        }
        case "5": { //last month
          var currentMonth = dateRange.startDate.getMonth();
          var year = dateRange.startDate.getFullYear();
          var month = currentMonth - 1;
          if (currentMonth === 1)
          {
            year = dateRange.startDate.getFullYear() - 1;
            month = 12;
          }
          dateRange.startDate = new Date(year, month, 1);
          dateRange.endDate = new Date(year, currentMonth, 0);
          break;
        }
        case "6": { //this year
          dateRange.startDate = new Date(dateRange.startDate.getFullYear(), 0, 1);
          dateRange.endDate = new Date(dateRange.startDate.getFullYear(), 11, 31);
          break;
        }
        case "7": { //last year
          dateRange.startDate = new Date(dateRange.startDate.getFullYear()-1, 0, 1);
          dateRange.endDate = new Date(dateRange.startDate.getFullYear()-1, 11, 31);
          break;
        }
        case "8": {
          dateRange.startDate = new Date(dateRange.startDate.getFullYear()-2, 0, 1);
          break;
        }
        default: {
          break;
        }
      }
      dateRange.startDate.setHours(0, 0, 0, 0);
      dateRange.endDate.setHours(23, 59, 59, 999);
      return dateRange;
}

export const BASEURL = "https://api.hey-bevan.com";
  