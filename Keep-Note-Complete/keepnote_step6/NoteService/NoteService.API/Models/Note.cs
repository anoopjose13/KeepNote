using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace NoteService.API.Models
{
    /// <summary>
    /// Used for saving note date
    /// </summary>
    public class Note
    {
        [BsonId]
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }
        [JsonProperty(PropertyName = "content")]
        public string Content { get; set; }
        [JsonProperty(PropertyName = "creationdate")]
        public DateTime CreationDate { get; set; }
        [JsonProperty(PropertyName = "createdby")]
        public string CreatedBy { get; set; }
        [JsonProperty(PropertyName = "state")]
        public string State { get; set; }
        [JsonProperty(PropertyName = "category")]
        public Category Category { get; set; }
        [JsonProperty(PropertyName = "reminders")]
        public List<Reminder> Reminders { get; set; }
    }
}
