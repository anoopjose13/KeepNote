using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace NoteService.API.Models
{
    public class NoteContext:INoteContext
    {
        private readonly IMongoDatabase database;
        MongoClient client;

        public NoteContext(IConfiguration configuration)
        {
            client = new MongoClient(configuration.GetSection("MongoDB:ConnectionString").Value);
            database = client.GetDatabase(configuration.GetSection("MongoDB:Database").Value);
        }

        public IMongoCollection<NoteUser> Notes => database.GetCollection<NoteUser>("Notes");
    }
}
