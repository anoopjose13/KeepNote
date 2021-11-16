using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using MongoDB.Bson.Serialization.Attributes;

namespace NoteService.API.Models
{
    public class Reminder
    {
        [BsonId]
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }
        [JsonProperty(PropertyName = "createdby")]
        public string CreatedBy { get; set; }
        [JsonProperty(PropertyName = "createddate")]
        public DateTime CreationDate { get; set; }
    }
}
