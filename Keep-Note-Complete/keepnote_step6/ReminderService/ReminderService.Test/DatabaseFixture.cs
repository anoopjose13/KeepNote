using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using ReminderService.API.Models;

namespace ReminderService.Test
{
    public class DatabaseFixture:IDisposable
    {
        private IConfigurationRoot configuration;
        public IReminderContext context;
        public DatabaseFixture()
        {
            var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");

            configuration = builder.Build();
            context = new ReminderContext(configuration);
            var filter = new BsonDocument();
            context.Reminders.DeleteMany(filter);
            context.Reminders.InsertMany(new List<Reminder>
            {
                new Reminder{Id=201, Name="Sports", CreatedBy="Mukesh", Description="sports reminder", CreationDate=new DateTime(), Type="email" },
                 new Reminder{Id=202, Name="Politics", CreatedBy="Mukesh", Description="politics reminder", CreationDate=new DateTime(),Type="email" }
            });
        }
        public void Dispose()
        {
            context = null;
        }
    }
}
