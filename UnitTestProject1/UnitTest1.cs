using EmployeePayroll_RestApi;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        /// <summary>
        /// translates rest request to http requests for processing
        /// </summary>
        RestClient client;

        /// <summary>
        ///setting the base url given by the user
        /// </summary>
        [TestInitialize]
        public void setUp()
        {
            client = new RestClient("http://localhost:3000");
        }

        /// <summary>
        /// makes restrequest for getting data from json server
        /// </summary>
        /// <returns></returns>
        private IRestResponse GetEmployeeList()
        {
            /// in this /Employees is refering to the employees list 
            /// it will give us "http://localhost:3000/Employees"
            /// method is specified that what action we want to perform
            RestRequest request = new RestRequest("/Employees", Method.GET);
            ///executing the response 
            IRestResponse response = client.Execute(request);
            return response;
        }

        /// <summary>
        /// On Calling Get API Should Return Emmployee List
        /// UC1
        /// </summary>
        [TestMethod]
        public void OnCalling_GetAPI_ShouldReturnEmmployeeList()
        {
            ///getting irest response from the function
            IRestResponse response = GetEmployeeList();
            ///checking the status code or can be checked by status number
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            ///deserialising object response in the from of list
            List<EmployeeDetails> employeeList = JsonConvert.DeserializeObject<List<EmployeeDetails>>(response.Content);
            ///checking the number of contacts in the list
            Assert.AreEqual(4, employeeList.Count);
            foreach (EmployeeDetails employee in employeeList)
            {
                Console.WriteLine("id: " + employee.id + " name : " + employee.name + "  salary :" + employee.salary);
            }
        }
    }
}