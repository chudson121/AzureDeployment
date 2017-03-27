using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AzureDeployment.CommandLine;
using AzureDeployment.Logging.Interfaces;
using AzureDeployment.Logging.Services;
using Microsoft.Azure.Management.Fluent;
using Microsoft.Azure.Management.Resource.Fluent;
using Microsoft.Azure.Management.Resource.Fluent.Authentication;
using Microsoft.Azure.Management.Resource.Fluent.Core;
using Microsoft.Rest;
using Microsoft.Rest.Azure.Authentication;

namespace AzureDeployment
{
    public class Program
    {
        private static readonly ILogger Logger = LoggingService.GetLoggingService();
        private static CommandLineProcessor _clp;
        public static string AppName = Assembly.GetExecutingAssembly().GetName(true).Name;

        public static void Main(string[] args)
        {
            Logger.Info("Program startup");
            Logger.Info($"Program Name: {AppName}");

            _clp = new CommandLineProcessor(args, Logger);
            var AppId = "60c73d93-d5ab-42e8-b4d5-1b12c6ec4192";
            var AuthKey = "afhuxg33dlxpu/V0lHtLMN9lhpurkCU6jENxtQ7b4uc=";
            var TenantID = "373afff1-2527-439e-a138-0cd72c9325e4";
            var SubscriptionID = "cf8611e4-c7d7-4eac-a7cf-aa5d6c117499";
            var SubscriptionName = "Pay-As-You-Go";
            var ResGroups = "PROD-SVC, PROD-WEB, PROD-WEBJOB, Default-Web-EastUS";


            var credentials = AzureCredentials.FromServicePrincipal(AppId, AuthKey, TenantID, AzureEnvironment.AzureGlobalCloud);

            var azure = Azure
                .Configure()
                .WithLogLevel(HttpLoggingDelegatingHandler.Level.BASIC)
                .Authenticate(credentials)
                .WithDefaultSubscription();

            //GetResourceGroups(azure);
            
            var fullenchalada = new List<WebAppDto>();

            foreach (var grp in ResGroups.Split(','))
            {
                fullenchalada.AddRange(GetWebApps(azure, grp.Trim()));
            }

            foreach (WebAppDto dto in fullenchalada)
            {
                Logger.Info($"App {dto.AppName} URL: {dto.Url}");
            }



            //var resourceGroupName = "PROD-SVC"; //TEST-SVC";
            

            // var apigatewayresgroup = azure.ResourceGroups.GetByName("Api-Default-East-US-2");
            //var export=  apigatewayresgroup.ExportTemplate(ResourceGroupExportTemplateOptions.INCLUDE_BOTH);
            //var e = apigatewayresgroup.Inner.Name;
            //foreach (var gateway in azure.ResourceGroups.GetByName(["Api-Default-East-US-2"]))
            //{
            //    Console.WriteLine($"name: {gateway.Name}");
            //}

            //azure.WebApps.Define()



             Logger.Info("Program End");

            //            Logger.Error("Something screwed up");
            //            Exception e = new ArgumentNullException();
            //            Logger.Error("Something screwed up and here is the exception", e);

            Console.ReadLine();
        }

        private static List<WebAppDto> GetWebApps(IAzure azure, string resourceGroupName)
        {
            var retval = new List<WebAppDto>();


            foreach (var webapp in azure.WebApps.ListByGroup(resourceGroupName))
            {
                Console.WriteLine($"{resourceGroupName} webAppName: {webapp.Name} url: {webapp.HostNames.FirstOrDefault()} ");

                retval.Add(new WebAppDto
                {
                    ResourceGroup = resourceGroupName,
                    AppName = webapp.Name,
                    Url = string.Join(",", webapp.HostNames)
                });

            }

            return retval;
        }

        private static void GetResourceGroups(IAzure azure)
        {
            foreach (var sAccount in azure.ResourceGroups.List())
            {
                Console.WriteLine("Resource group name: " + sAccount.Name);
            }
        }
    }
}