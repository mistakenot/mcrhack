using System;
using System.Net;
using Clockwork;

namespace BookByText
{
    public class SmsService : ISmsService
    {
        public void Send(string number, string body)
        {
            try
            {
                Clockwork.API api = new API("e26d24050e1147c7f8a57bdeeb574581dd5ca5f1");
                SMSResult result = api.Send(
                    new SMS
                    {
                        To = number,
                        Message = body
                    });

                if (!result.Success)
                {
                    throw new Exception(result.ErrorMessage);
                }
            }
            catch (APIException ex)
            {
                // You’ll get an API exception for errors
                // such as wrong username or password
                Console.WriteLine("API Exception: " + ex.Message);
            }
            catch (WebException ex)
            {
                // Web exceptions mean you couldn’t reach the Clockwork server
                Console.WriteLine("Web Exception: " + ex.Message);
            }
            catch (ArgumentException ex)
            {
                // Argument exceptions are thrown for missing parameters,
                // such as forgetting to set the username
                Console.WriteLine("Argument Exception: " + ex.Message);
            }
            catch (Exception ex)
            {
                // Something else went wrong, the error message should help
                Console.WriteLine("Unknown Exception: " + ex.Message);
            }
        }
    }
}
