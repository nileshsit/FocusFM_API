using System.ComponentModel;
using System.Net.Mail;
using System.Reflection;

namespace FocusFM.Common.Helpers
{
    public class TimzoneModel
    {
        public string Offset { get; set; }
        public string TimezoneId { get; set; }
    }
    public static class Utility
    {

        /// <summary>
        /// Check if valid email address or not
        /// </summary>
        /// <param name="emailaddress"></param>
        /// <returns></returns>
        public static bool IsValidEmail(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Check if valid mobile number or not (As per Norway standards)
        /// </summary>
        /// <param name="mobileNumber">Mobile number</param>
        /// <returns></returns>
        public static bool IsValidMobilNo(string mobileNumber)
        {
            bool isValid = false;

            if (mobileNumber.Length > 0 && mobileNumber.All(char.IsNumber))
            {
                isValid = true;
            }

            return isValid;
        }

        /// <summary>
        /// Generate random password
        /// </summary>
        /// <returns></returns>
        public static string GeneratePassword()
        {
            // Password should be 8 caharacters - first 6 lower case then 2 didgits
            var stringLower = "abcdefghijklmnopqrstuvwxyz";
            var numeric = "0123456789";
            var password = "";
            var character = "";
            var characters = 0;
            var numerics = 0;
            Random random = new Random();
            while (characters < 6) // 6 characters lower case
            {
                var entity1 = random.Next(0, stringLower.Length - 1);
                characters++;
                character += stringLower.ToCharArray()[entity1];
            }

            while (numerics < 2) // 2 digits
            {
                var entity3 = random.Next(0, numeric.Length - 1);
                numerics++;

                character += numeric.ToCharArray()[entity3];
            }
            password = character;

            return password;
        }

        public static DateTime Convert_FromUTC(DateTime utcDateTime, string timeZone)
        {
            try
            {
                TimeZoneInfo nzTimeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZone);
                DateTime dateTimeAsTimeZone = TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, nzTimeZone);
                return dateTimeAsTimeZone;
            }
            catch
            {
                throw;
            }
        }

        public static DateTime ConvertToUTC(DateTime dateTime, string timeZone)
        {
            try
            {
                TimeZoneInfo nzTimeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZone);
                DateTime utcDateTime = TimeZoneInfo.ConvertTimeToUtc(dateTime, nzTimeZone);
                return utcDateTime;
            }
            catch
            {
                throw;
            }
        }

        public static int[] ConvertEnumToIntArray(Type enums)
        {
            return System.Enum.GetValues(enums)
           .Cast<int>()
           .Select(x => x)
           .ToArray();
        }

        public static string ApplicationPath
        {
            get
            {
                return AppDomain.CurrentDomain.BaseDirectory;
            }
        }

        public static DateTime ConvertFromUTC(DateTime dateTime, string desiredOffset)
        {
            try
            {
                IReadOnlyCollection<TimeZoneInfo> timeZones = TimeZoneInfo.GetSystemTimeZones();
                TimeZoneInfo desiredTimeZone = null;
                foreach (TimeZoneInfo timeZone in timeZones)
                {
                    if (timeZone.Id == desiredOffset)
                    {
                        desiredTimeZone = timeZone;
                        break;
                    }
                }
                if (desiredTimeZone != null)
                {
                    TimeZoneInfo gmtTimeZone = TimeZoneInfo.FindSystemTimeZoneById(desiredOffset); // Replace with the desired time zone ID for GMT
                    DateTime gmtDateTime = TimeZoneInfo.ConvertTimeFromUtc(dateTime, gmtTimeZone);
                    return gmtDateTime;
                }
                else
                {
                    throw new Exception("Desired time zone not found.");
                }
            }
            catch
            {
                throw;
            }
        }

        public static string GetEncryptPassword(string password)
        {

            string hashed = EncryptionDecryption.Hash(EncryptionDecryption.GetEncrypt(password));
            string[] segments = hashed.Split(":");
            string EncryptedHash = EncryptionDecryption.GetEncrypt(segments[0]);
            string EncryptedSalt = EncryptionDecryption.GetEncrypt(segments[1]);

            return EncryptedHash +"||"+ EncryptedSalt;
        }
        public static string GetDisplayName(PropertyInfo propertyInfo)
        {
            if (propertyInfo != null)
            {
                DisplayNameAttribute? displayNameAttribute = propertyInfo.GetCustomAttribute<DisplayNameAttribute>();
                return displayNameAttribute?.DisplayName ?? propertyInfo.Name;
            }
            return "-";
        
        }
        public static string GetTimeZoneBaseOnDaylight(string Offset)
        {
            List<TimzoneModel> list = new List<TimzoneModel>
            {
                new TimzoneModel { TimezoneId = "Dateline Standard Time",Offset = "-12:00"},
                new TimzoneModel { TimezoneId = "UTC-11",Offset = "-11:00"},
                new TimzoneModel { TimezoneId = "Aleutian Standard Time",Offset = "-09:00"},
                new TimzoneModel { TimezoneId = "Hawaiian Standard Time",Offset = "-10:00"},
                new TimzoneModel { TimezoneId = "Marquesas Standard Time",Offset = "-9:30"},
                new TimzoneModel { TimezoneId = "Alaskan Standard Time",Offset = "-08:00"},
                new TimzoneModel { TimezoneId = "UTC-09",Offset = "-09:00"},
                new TimzoneModel { TimezoneId = "Pacific Standard Time (Mexico)",Offset = "-07:00"},
                new TimzoneModel { TimezoneId = "UTC-08",Offset = "-08:00"},
                new TimzoneModel { TimezoneId = "Pacific Standard Time",Offset = "-07:00"},
                new TimzoneModel { TimezoneId = "US Mountain Standard Time",Offset = "-07:00"},
                new TimzoneModel { TimezoneId = "Mountain Standard Time (Mexico)",Offset = "-07:00"},
                new TimzoneModel { TimezoneId = "Mountain Standard Time",Offset = "-06:00"},
                new TimzoneModel { TimezoneId = "Yukon Standard Time",Offset = "-07:00"},
                new TimzoneModel { TimezoneId = "Central America Standard Time",Offset = "-06:00"},
                new TimzoneModel { TimezoneId = "Central Standard Time",Offset = "-05:00"},
                new TimzoneModel { TimezoneId = "Easter Island Standard Time",Offset = "-06:00"},
                new TimzoneModel { TimezoneId = "Central Standard Time (Mexico)",Offset = "-06:00"},
                new TimzoneModel { TimezoneId = "Canada Central Standard Time",Offset = "-06:00"},
                new TimzoneModel { TimezoneId = "SA Pacific Standard Time",Offset = "-05:00"},
                new TimzoneModel { TimezoneId = "Eastern Standard Time (Mexico)",Offset = "-05:00"},
                new TimzoneModel { TimezoneId = "Eastern Standard Time",Offset = "-04:00"},
                new TimzoneModel { TimezoneId = "Haiti Standard Time",Offset = "-04:00"},
                new TimzoneModel { TimezoneId = "Cuba Standard Time",Offset = "-04:00"},
                new TimzoneModel { TimezoneId = "US Eastern Standard Time",Offset = "-04:00"},
                new TimzoneModel { TimezoneId = "Turks And Caicos Standard Time",Offset = "-04:00"},
                new TimzoneModel { TimezoneId = "Paraguay Standard Time",Offset = "-04:00"},
                new TimzoneModel { TimezoneId = "Atlantic Standard Time",Offset = "-03:00"},
                new TimzoneModel { TimezoneId = "Venezuela Standard Time",Offset = "-04:00"},
                new TimzoneModel { TimezoneId = "Central Brazilian Standard Time",Offset = "-04:00"},
                new TimzoneModel { TimezoneId = "SA Western Standard Time",Offset = "-04:00"},
                new TimzoneModel { TimezoneId = "Pacific SA Standard Time",Offset = "-04:00"},
                new TimzoneModel { TimezoneId = "Newfoundland Standard Time",Offset = "-2:30"},
                new TimzoneModel { TimezoneId = "Tocantins Standard Time",Offset = "-03:00"},
                new TimzoneModel { TimezoneId = "E. South America Standard Time",Offset = "-03:00"},
                new TimzoneModel { TimezoneId = "SA Eastern Standard Time",Offset = "-03:00"},
                new TimzoneModel { TimezoneId = "Argentina Standard Time",Offset = "-03:00"},
                new TimzoneModel { TimezoneId = "Greenland Standard Time",Offset = "-02:00"},
                new TimzoneModel { TimezoneId = "Montevideo Standard Time",Offset = "-03:00"},
                new TimzoneModel { TimezoneId = "Magallanes Standard Time",Offset = "-03:00"},
                new TimzoneModel { TimezoneId = "Saint Pierre Standard Time",Offset = "-02:00"},
                new TimzoneModel { TimezoneId = "Bahia Standard Time",Offset = "-03:00"},
                new TimzoneModel { TimezoneId = "UTC-02",Offset = "-02:00"},
                new TimzoneModel { TimezoneId = "Mid-Atlantic Standard Time",Offset = "-01:00"},
                new TimzoneModel { TimezoneId = "Azores Standard Time",Offset = "+00:00"},
                new TimzoneModel { TimezoneId = "Cape Verde Standard Time",Offset = "-01:00"},
                new TimzoneModel { TimezoneId = "UTC",Offset = "-00:00"},
                new TimzoneModel { TimezoneId = "UTC",Offset = "+00:00"},
                new TimzoneModel { TimezoneId = "GMT Standard Time",Offset = "+01:00"},
                new TimzoneModel { TimezoneId = "Greenwich Standard Time",Offset = "+00:00"},
                new TimzoneModel { TimezoneId = "Sao TOMe Standard Time",Offset = "+00:00"},
                new TimzoneModel { TimezoneId = "Morocco Standard Time",Offset = "+01:00"},
                new TimzoneModel { TimezoneId = "W. Europe Standard Time",Offset = "+02:00"},
                new TimzoneModel { TimezoneId = "Central Europe Standard Time",Offset = "+02:00"},
                new TimzoneModel { TimezoneId = "Romance Standard Time",Offset = "+02:00"},
                new TimzoneModel { TimezoneId = "Central European Standard Time",Offset = "+02:00"},
                new TimzoneModel { TimezoneId = "W. Central Africa Standard Time",Offset = "+01:00"},
                new TimzoneModel { TimezoneId = "GTB Standard Time",Offset = "+03:00"},
                new TimzoneModel { TimezoneId = "Middle East Standard Time",Offset = "+03:00"},
                new TimzoneModel { TimezoneId = "Egypt Standard Time",Offset = "+03:00"},
                new TimzoneModel { TimezoneId = "E. Europe Standard Time",Offset = "+03:00"},
                new TimzoneModel { TimezoneId = "Syria Standard Time",Offset = "+03:00"},
                new TimzoneModel { TimezoneId = "West Bank Standard Time",Offset = "+03:00"},
                new TimzoneModel { TimezoneId = "South Africa Standard Time",Offset = "+02:00"},
                new TimzoneModel { TimezoneId = "FLE Standard Time",Offset = "+03:00"},
                new TimzoneModel { TimezoneId = "Israel Standard Time",Offset = "+03:00"},
                new TimzoneModel { TimezoneId = "South Sudan Standard Time",Offset = "+02:00"},
                new TimzoneModel { TimezoneId = "Kaliningrad Standard Time",Offset = "+02:00"},
                new TimzoneModel { TimezoneId = "Sudan Standard Time",Offset = "+02:00"},
                new TimzoneModel { TimezoneId = "Libya Standard Time",Offset = "+02:00"},
                new TimzoneModel { TimezoneId = "Namibia Standard Time",Offset = "+02:00"},
                new TimzoneModel { TimezoneId = "Jordan Standard Time",Offset = "+03:00"},
                new TimzoneModel { TimezoneId = "Arabic Standard Time",Offset = "+03:00"},
                new TimzoneModel { TimezoneId = "Turkey Standard Time",Offset = "+03:00"},
                new TimzoneModel { TimezoneId = "Arab Standard Time",Offset = "+03:00"},
                new TimzoneModel { TimezoneId = "Belarus Standard Time",Offset = "+03:00"},
                new TimzoneModel { TimezoneId = "Russian Standard Time",Offset = "+03:00"},
                new TimzoneModel { TimezoneId = "E. Africa Standard Time",Offset = "+03:00"},
                new TimzoneModel { TimezoneId = "Volgograd Standard Time",Offset = "+03:00"},
                new TimzoneModel { TimezoneId = "Iran Standard Time",Offset = "+03:30"},
                new TimzoneModel { TimezoneId = "Arabian Standard Time",Offset = "+04:00"},
                new TimzoneModel { TimezoneId = "Astrakhan Standard Time",Offset = "+04:00"},
                new TimzoneModel { TimezoneId = "Azerbaijan Standard Time",Offset = "+04:00"},
                new TimzoneModel { TimezoneId = "Russia Time Zone 3",Offset = "+04:00"},
                new TimzoneModel { TimezoneId = "Mauritius Standard Time",Offset = "+04:00"},
                new TimzoneModel { TimezoneId = "Saratov Standard Time",Offset = "+04:00"},
                new TimzoneModel { TimezoneId = "Georgian Standard Time",Offset = "+04:00"},
                new TimzoneModel { TimezoneId = "Caucasus Standard Time",Offset = "+04:00"},
                new TimzoneModel { TimezoneId = "Afghanistan Standard Time",Offset = "+04:30"},
                new TimzoneModel { TimezoneId = "West Asia Standard Time",Offset = "+05:00"},
                new TimzoneModel { TimezoneId = "Ekaterinburg Standard Time",Offset = "+05:00"},
                new TimzoneModel { TimezoneId = "Pakistan Standard Time",Offset = "+05:00"},
                new TimzoneModel { TimezoneId = "Qyzylorda Standard Time",Offset = "+05:00"},
                new TimzoneModel { TimezoneId = "India Standard Time",Offset = "+05:30"},
                new TimzoneModel { TimezoneId = "Sri Lanka Standard Time",Offset = "+05:30"},
                new TimzoneModel { TimezoneId = "Nepal Standard Time",Offset = "+05:45"},
                new TimzoneModel { TimezoneId = "Central Asia Standard Time",Offset = "+06:00"},
                new TimzoneModel { TimezoneId = "Bangladesh Standard Time",Offset = "+06:00"},
                new TimzoneModel { TimezoneId = "Omsk Standard Time",Offset = "+06:00"},
                new TimzoneModel { TimezoneId = "Myanmar Standard Time",Offset = "+06:30"},
                new TimzoneModel { TimezoneId = "SE Asia Standard Time",Offset = "+07:00"},
                new TimzoneModel { TimezoneId = "Altai Standard Time",Offset = "+07:00"},
                new TimzoneModel { TimezoneId = "W. Mongolia Standard Time",Offset = "+07:00"},
                new TimzoneModel { TimezoneId = "North Asia Standard Time",Offset = "+07:00"},
                new TimzoneModel { TimezoneId = "N. Central Asia Standard Time",Offset = "+07:00"},
                new TimzoneModel { TimezoneId = "TOMsk Standard Time",Offset = "+07:00"},
                new TimzoneModel { TimezoneId = "China Standard Time",Offset = "+08:00"},
                new TimzoneModel { TimezoneId = "North Asia East Standard Time",Offset = "+08:00"},
                new TimzoneModel { TimezoneId = "Singapore Standard Time",Offset = "+08:00"},
                new TimzoneModel { TimezoneId = "W. Australia Standard Time",Offset = "+08:00"},
                new TimzoneModel { TimezoneId = "Taipei Standard Time",Offset = "+08:00"},
                new TimzoneModel { TimezoneId = "Ulaanbaatar Standard Time",Offset = "+08:00"},
                new TimzoneModel { TimezoneId = "Aus Central W. Standard Time",Offset = "+08:45"},
                new TimzoneModel { TimezoneId = "Transbaikal Standard Time",Offset = "+09:00"},
                new TimzoneModel { TimezoneId = "Tokyo Standard Time",Offset = "+09:00"},
                new TimzoneModel { TimezoneId = "North Korea Standard Time",Offset = "+09:00"},
                new TimzoneModel { TimezoneId = "Korea Standard Time",Offset = "+09:00"},
                new TimzoneModel { TimezoneId = "Yakutsk Standard Time",Offset = "+09:00"},
                new TimzoneModel { TimezoneId = "Cen. Australia Standard Time",Offset = "+09:30"},
                new TimzoneModel { TimezoneId = "AUS Central Standard Time",Offset = "+09:30"},
                new TimzoneModel { TimezoneId = "E. Australia Standard Time",Offset = "+10:00"},
                new TimzoneModel { TimezoneId = "AUS Eastern Standard Time",Offset = "+10:00"},
                new TimzoneModel { TimezoneId = "West Pacific Standard Time",Offset = "+10:00"},
                new TimzoneModel { TimezoneId = "Tasmania Standard Time",Offset = "+10:00"},
                new TimzoneModel { TimezoneId = "Vladivostok Standard Time",Offset = "+10:00"},
                new TimzoneModel { TimezoneId = "Lord Howe Standard Time",Offset = "+10:30"},
                new TimzoneModel { TimezoneId = "Bougainville Standard Time",Offset = "+11:00"},
                new TimzoneModel { TimezoneId = "Russia Time Zone 10",Offset = "+11:00"},
                new TimzoneModel { TimezoneId = "Magadan Standard Time",Offset = "+11:00"},
                new TimzoneModel { TimezoneId = "Norfolk Standard Time",Offset = "+11:00"},
                new TimzoneModel { TimezoneId = "Sakhalin Standard Time",Offset = "+11:00"},
                new TimzoneModel { TimezoneId = "Central Pacific Standard Time",Offset = "+11:00"},
                new TimzoneModel { TimezoneId = "Russia Time Zone 11",Offset = "+12:00"},
                new TimzoneModel { TimezoneId = "New Zealand Standard Time",Offset = "+12:00"},
                new TimzoneModel { TimezoneId = "UTC+12",Offset = "+12:00"},
                new TimzoneModel { TimezoneId = "Fiji Standard Time",Offset = "+12:00"},
                new TimzoneModel { TimezoneId = "Kamchatka Standard Time",Offset = "+13:00"},
                new TimzoneModel { TimezoneId = "Chatham Islands Standard Time",Offset = "+12:45"},
                new TimzoneModel { TimezoneId = "UTC+13",Offset = "+13:00"},
                new TimzoneModel { TimezoneId = "Tonga Standard Time",Offset = "+13:00"},
                new TimzoneModel { TimezoneId = "Samoa Standard Time",Offset = "+13:00"},
                new TimzoneModel { TimezoneId = "Line Islands Standard Time",Offset = "+14:00"}
            };

            if (list.Where(x => (x.Offset == Offset)).Count() > 0)
            {
                return list.Where(x => (x.Offset == Offset)).Select(x => x.TimezoneId).First();
            }
            else
            {
                return "";
            }
        }

        public static int GetRoundedValue(decimal value)
        {
            return (int)Math.Ceiling((double)value / 5) * 5;
        }
    }
}