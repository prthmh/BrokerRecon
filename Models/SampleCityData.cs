using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BrokerRecon.Models
{
    public class SampleCityData
    {
        public static readonly List<CityModel> cities = new List<CityModel>
        {
            // Cities in Maharashtra (stateId: 1)
            new CityModel { id = 1, city = "Mumbai", stateId = 1 },
            new CityModel { id = 2, city = "Pune", stateId = 1 },
            new CityModel { id = 3, city = "Nagpur", stateId = 1 },
            new CityModel { id = 4, city = "Nashik", stateId = 1 },
            new CityModel { id = 5, city = "Aurangabad", stateId = 1 },
            new CityModel { id = 6, city = "Thane", stateId = 1 },
            new CityModel { id = 7, city = "Solapur", stateId = 1 },
            new CityModel { id = 8, city = "Kolhapur", stateId = 1 },

            // Cities in Goa (stateId: 2)
            new CityModel { id = 9, city = "Panaji", stateId = 2 },
            new CityModel { id = 10, city = "Margao", stateId = 2 },
            new CityModel { id = 11, city = "Vasco da Gama", stateId = 2 },
            new CityModel { id = 12, city = "Mapusa", stateId = 2 },
            new CityModel { id = 13, city = "Ponda", stateId = 2 },
            new CityModel { id = 14, city = "Bicholim", stateId = 2 },
            new CityModel { id = 15, city = "Curchorem", stateId = 2 },
            new CityModel { id = 16, city = "Canacona", stateId = 2 },

            // Cities in Gujarat (stateId: 3)
            new CityModel { id = 17, city = "Ahmedabad", stateId = 3 },
            new CityModel { id = 18, city = "Surat", stateId = 3 },
            new CityModel { id = 19, city = "Vadodara", stateId = 3 },
            new CityModel { id = 20, city = "Rajkot", stateId = 3 },
            new CityModel { id = 21, city = "Bhavnagar", stateId = 3 },
            new CityModel { id = 22, city = "Jamnagar", stateId = 3 },
            new CityModel { id = 23, city = "Junagadh", stateId = 3 },
            new CityModel { id = 24, city = "Gandhinagar", stateId = 3 },

            // Cities in Sikkim (stateId: 4)
            new CityModel { id = 25, city = "Gangtok", stateId = 4 },
            new CityModel { id = 26, city = "Namchi", stateId = 4 },
            new CityModel { id = 27, city = "Gyalshing", stateId = 4 },
            new CityModel { id = 28, city = "Mangan", stateId = 4 },
            new CityModel { id = 29, city = "Rangpo", stateId = 4 },
            new CityModel { id = 30, city = "Jorethang", stateId = 4 },
            new CityModel { id = 31, city = "Singtam", stateId = 4 },
            new CityModel { id = 32, city = "Ravangla", stateId = 4 },

            // Cities in Assam (stateId: 5)
            new CityModel { id = 33, city = "Guwahati", stateId = 5 },
            new CityModel { id = 34, city = "Dibrugarh", stateId = 5 },
            new CityModel { id = 35, city = "Silchar", stateId = 5 },
            new CityModel { id = 36, city = "Jorhat", stateId = 5 },
            new CityModel { id = 37, city = "Tezpur", stateId = 5 },
            new CityModel { id = 38, city = "Nagaon", stateId = 5 },
            new CityModel { id = 39, city = "Tinsukia", stateId = 5 },
            new CityModel { id = 40, city = "Diphu", stateId = 5 },

            // Cities in Tamil Nadu (stateId: 6)
            new CityModel { id = 41, city = "Chennai", stateId = 6 },
            new CityModel { id = 42, city = "Coimbatore", stateId = 6 },
            new CityModel { id = 43, city = "Madurai", stateId = 6 },
            new CityModel { id = 44, city = "Tiruchirappalli", stateId = 6 },
            new CityModel { id = 45, city = "Salem", stateId = 6 },
            new CityModel { id = 46, city = "Tirunelveli", stateId = 6 },
            new CityModel { id = 47, city = "Vellore", stateId = 6 },
            new CityModel { id = 48, city = "Erode", stateId = 6 },

            // Cities in Kerala (stateId: 7)
            new CityModel { id = 49, city = "Thiruvananthapuram", stateId = 7 },
            new CityModel { id = 50, city = "Kochi", stateId = 7 },
            new CityModel { id = 51, city = "Kozhikode", stateId = 7 },
            new CityModel { id = 52, city = "Thrissur", stateId = 7 },
            new CityModel { id = 53, city = "Alappuzha", stateId = 7 },
            new CityModel { id = 54, city = "Palakkad", stateId = 7 },
            new CityModel { id = 55, city = "Kollam", stateId = 7 },
            new CityModel { id = 56, city = "Kannur", stateId = 7 },

            // Cities in Karnataka (stateId: 8)
            new CityModel { id = 57, city = "Bengaluru", stateId = 8 },
            new CityModel { id = 58, city = "Mysuru", stateId = 8 },
            new CityModel { id = 59, city = "Hubli", stateId = 8 },
            new CityModel { id = 60, city = "Mangaluru", stateId = 8 },
            new CityModel { id = 61, city = "Belagavi", stateId = 8 },
            new CityModel { id = 62, city = "Davangere", stateId = 8 },
            new CityModel { id = 63, city = "Ballari", stateId = 8 },
            new CityModel { id = 64, city = "Shivamogga", stateId = 8 },

            // Cities in Andhra Pradesh (stateId: 9)
            new CityModel { id = 65, city = "Visakhapatnam", stateId = 9 },
            new CityModel { id = 66, city = "Vijayawada", stateId = 9 },
            new CityModel { id = 67, city = "Guntur", stateId = 9 },
            new CityModel { id = 68, city = "Nellore", stateId = 9 },
            new CityModel { id = 69, city = "Kurnool", stateId = 9 },
            new CityModel { id = 70, city = "Tirupati", stateId = 9 },
            new CityModel { id = 71, city = "Rajahmundry", stateId = 9 },
            new CityModel { id = 72, city = "Kakinada", stateId = 9 },

            // Cities in Telangana (stateId: 10)
            new CityModel { id = 73, city = "Hyderabad", stateId = 10 },
            new CityModel { id = 74, city = "Warangal", stateId = 10 },
            new CityModel { id = 75, city = "Nizamabad", stateId = 10 },
            new CityModel { id = 76, city = "Karimnagar", stateId = 10 },
            new CityModel { id = 77, city = "Khammam", stateId = 10 },
            new CityModel { id = 78, city = "Mahbubnagar", stateId = 10 },
            new CityModel { id = 79, city = "Ramagundam", stateId = 10 },
            new CityModel { id = 80, city = "Nalgonda", stateId = 10 },

            // Cities in Rajasthan (stateId: 11)
            new CityModel { id = 81, city = "Jaipur", stateId = 11 },
            new CityModel { id = 82, city = "Jodhpur", stateId = 11 },
            new CityModel { id = 83, city = "Udaipur", stateId = 11 },
            new CityModel { id = 84, city = "Kota", stateId = 11 },
            new CityModel { id = 85, city = "Bikaner", stateId = 11 },
            new CityModel { id = 86, city = "Ajmer", stateId = 11 },
            new CityModel { id = 87, city = "Alwar", stateId = 11 },
            new CityModel { id = 88, city = "Bharatpur", stateId = 11 },

            // Cities in Bihar (stateId: 12)
            new CityModel { id = 89, city = "Patna", stateId = 12 },
            new CityModel { id = 90, city = "Gaya", stateId = 12 },
            new CityModel { id = 91, city = "Bhagalpur", stateId = 12 },
            new CityModel { id = 92, city = "Muzaffarpur", stateId = 12 },
            new CityModel { id = 93, city = "Purnia", stateId = 12 },
            new CityModel { id = 94, city = "Darbhanga", stateId = 12 },
            new CityModel { id = 95, city = "Bihar Sharif", stateId = 12 },
            new CityModel { id = 96, city = "Ara", stateId = 12 },
        };
    }
}