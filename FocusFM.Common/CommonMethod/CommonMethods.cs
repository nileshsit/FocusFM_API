using FocusFM.Common.Helpers;
using FocusFM.Model.ReqResponse;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace FocusFM.Common.CommonMethod
{
    public class CommonMethods
    {
        #region GetKeyValues
        /// <summary>
        /// Get key value pair result
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static ParamValue GetKeyValues(HttpContext context)
        {
            ParamValue paramValues = new ParamValue();
            var headerValue = string.Empty;
            var queryString = string.Empty;
            var jsonString = string.Empty;
            StringValues outValue = string.Empty;

            // for from header value
            if (context.Request.Headers.TryGetValue(Constants.RequestModel, out outValue))
            {
                headerValue = outValue.FirstOrDefault();
                JObject jsonobj = JsonConvert.DeserializeObject<JObject>(headerValue);
                if (jsonobj != null)
                {
                    Dictionary<string, string> keyValueMap = new Dictionary<string, string>();
                    foreach (KeyValuePair<string, JToken> keyValuePair in jsonobj)
                    {
                        keyValueMap.Add(keyValuePair.Key, keyValuePair.Value.ToString());
                    }
                    List<ReqResponseKeyValue> keyValueMapNew = keyValueMap.ToList().Select(i => new ReqResponseKeyValue
                    {
                        Key = i.Key,
                        Value = i.Value
                    }).ToList();
                    jsonString = JsonConvert.SerializeObject(keyValueMapNew);
                }
            }

            // for from query value
            if (context.Request.QueryString.HasValue)
            {
                var dict = HttpUtility.ParseQueryString(context.Request.QueryString.Value);
                queryString = System.Text.Json.JsonSerializer.Serialize(
                                    dict.AllKeys.ToDictionary(k => k, k => dict[k]));
            }


            paramValues.HeaderValue = jsonString;
            paramValues.QueryStringValue = queryString;
            return paramValues;

        }
        #endregion

        public static string GenerateNewRandom()
        {
            Random generator = new Random();
            String r = generator.Next(0, 1000000).ToString("D6");
            if (r.Distinct().Count() == 1)
            {
                r = GenerateNewRandom();
            }
            return r;
        }

        public static bool IsValidEmail(string email)
        {
            // Regular expression pattern for email validation
            const string pattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
            return Regex.IsMatch(email, pattern);
        }

        public static bool IsPasswordStrong(string CreatePassword)
        {
            //const string pattern = @"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{6,}$"
            const string pattern = @"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&\-_^])[A-Za-z\d@$!%*#?&\-_^]{6,}$";
            //const string pattern = @"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&\-\_^()\+=\{\}|\\/<>,.;:'])[A-Za-z\d@$!%*#?&\-\_^()\+=\{\}|\\/<>,.;:']{6,}$";

            return Regex.IsMatch(CreatePassword, pattern);
        }

        public static string GetHost(string password)
        {
            //string email = "chirag.p@shaligraminfotec.com";
            //string password = "admin@123";
            string hashed = EncryptionDecryption.Hash(EncryptionDecryption.GetEncrypt(password));

            string[] segments = hashed.Split(":");

            string EncryptedHash = EncryptionDecryption.GetEncrypt(segments[0]);
            string EncryptedSalt = EncryptionDecryption.GetEncrypt(segments[1]);

            string Hash = EncryptionDecryption.GetDecrypt(EncryptedHash);
            string Salt = EncryptionDecryption.GetDecrypt(EncryptedSalt);

            return EncryptedHash + " || " + EncryptedSalt;
        }

        public static async Task<string> UploadFile(IFormFile file, string destinationPath, bool returnFullPath = false, bool includeOriginalName = false)
        {
            Guid guidFile = Guid.NewGuid();
            string fileName = includeOriginalName
                ? Path.GetFileNameWithoutExtension(file.FileName) + "_" + guidFile + Path.GetExtension(file.FileName)
                : guidFile + Path.GetExtension(file.FileName);

            string basePath = Path.Combine(Directory.GetCurrentDirectory(), destinationPath);

            if (!Directory.Exists(basePath))
            {
                Directory.CreateDirectory(basePath);
            }

            string fullPath = Path.Combine(basePath, fileName);

            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return returnFullPath ? fullPath : fileName;
        }

        public static Task<string> UploadImage(IFormFile userProfile, string imagePath)
        {
            return UploadFile(userProfile, imagePath, returnFullPath: false, includeOriginalName: false);
        }

        public static Task<string> UploadDocument(IFormFile userDocument, string imagePath)
        {
            return UploadFile(userDocument, imagePath, returnFullPath: true, includeOriginalName: true);
        }

        public static string FormatNumber(decimal number)
        {
            return number % 1 == 0 ? number.ToString("0") : number.ToString("0.00");
        }
    }
}