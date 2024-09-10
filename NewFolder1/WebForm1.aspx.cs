using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Linq;
using Newtonsoft.Json.Linq;


namespace IOCL_Internship_2nd.NewFolder1
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            userInput.Attributes.Add("onkeypress", "return clickButton(event,'" + submitButton.ClientID + "')");
        }
        public static string ExtractResultValue(string geminiResponse)
        {
            try
            {
                JObject responseObj = JObject.Parse(geminiResponse);
                if (responseObj.TryGetValue("result", out JToken resultToken))
                {
                    string resultValue = resultToken.Value<string>();
                    return resultValue;
                }
                else
                {
                    return "Result key not found in the response.";
                }
            }
            catch (Exception ex)
            {
                return $"Error parsing Gemini response: {ex.Message}";
            }
        }

        protected async void SubmitButton_Click(object sender, EventArgs e)
        {
            string userInputValue = userInput.Text;
            // take user input
            var apiResponse = await SendToAPI(userInputValue);//send to first api that'll return a simple SQL Query
            if (apiResponse.Contains("```"))//if it contains a sql code 
            {
                var databaseResponse = await Database_Function(apiResponse);//calling the database with the SQL Query
                apiResponse = ConvertResultsToString(databaseResponse);//making database query in String
            }
            apiResponse = await SecondAPI(apiResponse, userInputValue);//Send the user input and databse reply or first API response to make it presentable
            apiResponse = ExtractResultValue(apiResponse);
            resultlabel.InnerText = apiResponse;//sending response
        }



        public static async Task<string> SendToAPI(string input)
        {
            string apiEndpoint = "https://llamastudio.dev/api/clrm8ahih0001jo08lm3zg1jr";//Click on the api to get a overview how it work and see the context

            var apiResponse = await YourApiRequestFunction(apiEndpoint, input);

            return apiResponse;
        }

        private static async Task<string> YourApiRequestFunction(string apiUrl, string input)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);

                var data = new { input };

                var jsonContent = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(data), System.Text.Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync("", jsonContent);//if any error is here means api didn't respond

                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();

                return responseBody;
            }
        }

        private static async Task<IEnumerable<dynamic>> Database_Function(string sqlQuery)
        {
            string connectionString = @"Data Source=LAPTOP0586\SQLEXPRESS; Initial Catalog=IOCLhrDATA; Integrated Security=True; Trust Server Certificate=True";//Databse connection
            var connection = new SqlConnection(connectionString);

            string sql = sqlQuery.Substring(7);
            sql = sql.Substring(0, sql.Length - 4);
            sql = sql.Replace("\\n", " ");//cropped the sql code beacuse of API fixed response for the code"```sql...\\n...```"

            IEnumerable<dynamic> results = await connection.QueryAsync(sql);//got the database respond
            return results;
        }
        private static async Task<string> SecondAPI(string sqlQuery, string userInput)
        {
            string apiEndpoint = "https://llamastudio.dev/api/clrv2e2hv0001l2088r0dodex";
            string input = "USER INPUT: " + userInput + "DATABSE RESPONSE: " + sqlQuery;
            var apiResponse = await YourApiRequestFunction(apiEndpoint, input);

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string jsonResponse = serializer.Serialize(new { result = apiResponse });//making the api response in string

            return jsonResponse;
        }

        static string ConvertResultsToString(IEnumerable<dynamic> dynamicList) //making the databse respond to string
        {
            // Initialize an empty string
            string result = "";

            foreach (var item in dynamicList)
            {
                // Convert each dynamic property to a string and concatenate to the result
                result += "{";
                foreach (var KeyValuePair in item)
                {
                    //string value = KeyValuePair.Value;
                    //string name = KeyValuePair.Key;
                    //result += $"'{name}'= '{value}', ";
                    result += KeyValuePair;
                }
                result = result.TrimEnd(',', ' ') + "}\n";
            }

            return result;
        }
    }
}
