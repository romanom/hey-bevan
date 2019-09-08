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
        case 0: {
          dateRange.startDate.setHours(0, 0, 0, 0);
          dateRange.endDate.setHours(23, 59, 59, 999);
          break;
        }
        case 1: {
            dateRange.startDate.setDate(dateRange.startDate.getDate() - 1);
            dateRange.endDate.setDate(dateRange.endDate.getDate() - 1);
            dateRange.startDate.setHours(0, 0, 0, 0);
            dateRange.endDate.setHours(23, 59, 59, 999);
          break;
        }
        case 2: {
            dateRange.startDate = new Date(
                dateRange.startDate.setDate(dateRange.startDate.getDate() - dateRange.startDate.getDay())
          );
          dateRange.endDate = new Date(
            dateRange.startDate.setDate(dateRange.startDate.getDate() - dateRange.startDate.getDay() + 6)
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
    
    return dateRange;
}

export const BASEURL = "https://api.hey-bevan.com";
  