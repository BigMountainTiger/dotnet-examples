using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Amazon.SimpleSystemsManagement;
using CsvHelper;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using MlgMailingService.Api.Domain.OrderMailers.Dto;
using MlgMailingService.Api.Nuget;

namespace console_app
{
    class Program
    {
        private static List<MailerDto> ParseCSV()
        {
            using (var reader = new StreamReader("/home/song/Data/144494_AppendYearBuilt.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Read();
                csv.ReadHeader();

                // Total 65
                string[] header = csv.Context.HeaderRecord;
                var records = new List<MailerDto>();

                Func<string, DateTime?> ParseDataTime = (s) =>
                {
                    DateTime? result = null;

                    DateTime dateValue;
                    if (DateTime.TryParse(s, out dateValue))
                    {
                        result = dateValue;
                    }

                    return result;
                };

                Func<string, int?> ParseInt = (s) =>
                {
                    int? result = null;

                    int dateValue;
                    if (int.TryParse(s, out dateValue))
                    {
                        result = dateValue;
                    }

                    return result;
                };

                Func<string, decimal?> ParseDecimal = (s) =>
                {
                    decimal? result = null;

                    decimal dateValue;
                    if (decimal.TryParse(s, out dateValue))
                    {
                        result = dateValue;
                    }

                    return result;
                };

                while (csv.Read())
                {
                    var record = new MailerDto
                    {
                        Sequence = csv.GetField<int>("Number"),
                        //"Full Name"
                        FirstName = csv.GetField("first"),
                        LastName = csv.GetField("last"),
                        MailingStreet = csv.GetField("address"),
                        //PROPERTY ADDRESS ALL CAPS
                        MailingCity = csv.GetField("city"),
                        MailingState = csv.GetField("st"),
                        MailingZip = csv.GetField("zip"),
                        //Zip 4

                        //Phone
                        //Monthy Premium
                        //Yearly Premium
                        InsuranceQuote = csv.GetField("Yearly Premium"),
                        HomeValue = decimal.Parse(csv.GetField("Home Value").Trim(), NumberStyles.AllowThousands | NumberStyles.AllowDecimalPoint | NumberStyles.AllowCurrencySymbol),
                        //Dwelling Prem
                        //Building Limits
                        //Building Prem
                        //Other Limits
                        //Other Prem
                        //Personal Limits

                        //Personal Prem
                        //Pers Reimburse Limits
                        //Pers Reimburse Prem
                        //Family Limits
                        //Family Prem
                        //Guest Limits
                        //Guest Prem
                        //Living Exp Limits
                        //Living Exp Prem
                        //Deductibles

                        //Ded Amount
                        //Discounts
                        //Coverages Total
                        //Total Premium
                        //Control Number
                        //Agency ID
                        //Agency Fn
                        //Agency Ln
                        //Agency Addr
                        //Agency Addr 2

                        //Agency City
                        //Agency State
                        //Agency Zip
                        //Agency Phone
                        //Agency Email
                        //Agency Web
                        //Agent Title
                        //Agent Desig
                        //Address 2
                        //Agency License Num

                        //Ta Gsent
                        //Email Address
                        //Photo URL
                        HalfBathrooms = csv.GetField<int>("Half Bath"),
                        SaleDate = ParseDataTime(csv.GetField("Sale Date")),
                        //Quote Month
                        Bedrooms = ParseInt(csv.GetField("Bedrooms Allstate")),
                        FullBathrooms = ParseInt(csv.GetField("Baths Allstate")),
                        ActualSquareFootage = ParseDecimal(csv.GetField("Allstate Living Square Feet")),
                        //seedordernr

                        ExpirationDate = DateTime.Parse(csv.GetField("exp_date")),
                        ReferenceNr = csv.GetField("ref"),
                        //tollfree
                        PropertyCity = csv.GetField("propcity"),
                        PropertyState = csv.GetField("propst"),
                        YearBuilt = ParseInt(csv.GetField("Year Built").Trim() == "0"? null: csv.GetField("Year Built").Trim()),

                        // Mandatory fields
                        Created = DateTime.Now,
                        OrderId = 77035,

                    };

                    records.Add(record);
                }

                return records;
            }
        }

        private static void Dump()
        {
            var records = ParseCSV();
            System.Console.WriteLine("Parse the csv file\n");

            HostingEnvironment hostingEnvironment = new HostingEnvironment();
            hostingEnvironment.EnvironmentName = "production";

            MailingService service = new MailingService(new AmazonSimpleSystemsManagementClient(), hostingEnvironment);
            System.Console.WriteLine("Created the service client\n");

            //service.AddAsync(records).Wait();
            System.Console.WriteLine("Called the MailingService\n");

        }
        
        private static void Receive()
        {
            HostingEnvironment hostingEnvironment = new HostingEnvironment();
            hostingEnvironment.EnvironmentName = "production";

            MailingService service = new MailingService(new AmazonSimpleSystemsManagementClient(), hostingEnvironment);
            System.Console.WriteLine("Created the service client");

            var result = service.GetForOrderAsync(77035).Result;
            System.Console.WriteLine($"Total {result.Count} lines received");

            return;
        }

        static void Main(string[] args)
        {
            //Dump();

            Receive();
        }
    }

    public sealed class HostingEnvironment : IHostingEnvironment
    {
        public string EnvironmentName { get; set; }
        public string ApplicationName
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }
        public string ContentRootPath
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        IFileProvider IHostingEnvironment.ContentRootFileProvider {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException(); 
        }
    }
}
