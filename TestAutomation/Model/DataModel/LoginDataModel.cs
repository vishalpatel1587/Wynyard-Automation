
using System;

namespace TestAutomation.Model.DataModel
{
    public class LoginDataModel
    {
        public enum DataInstance
        {
            ValidDetails,
            InvalidPassword,
            InvalidUsername,
            InvalidDetails
        }

        public string Username { get; set; }

        public string Password { get; set; }

        public LoginDataModel BuildModel(DataInstance instance)
        {
            switch (instance)
            {
                case DataInstance.ValidDetails:
                    {
                        Username = "admin";
                        Password = "password";
                        return this;
                    }
                case DataInstance.InvalidPassword:
                    {
                        Username = "admin";
                        Password = "abc";
                        return this;
                    }
                case DataInstance.InvalidUsername:
                    {
                        Username = "abc";
                        Password = "password";
                        return this;
                    }
                case DataInstance.InvalidDetails:
                    {
                        Username = "abc";
                        Password = "abc";
                        return this;
                    }
                default:
                    throw new Exception("The instance you passed ' " + instance.ToString() +
                                        "' does not match any of the existng LoginDataModel instances.");
            }
        }
    }
}
