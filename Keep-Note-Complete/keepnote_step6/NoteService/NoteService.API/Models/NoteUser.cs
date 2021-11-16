using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace NoteService.API.Models
{
    public class NoteUser
    {
        [BsonId]
        [JsonProperty(PropertyName="userid")]
        public string UserId { get; set; }
        [JsonProperty(PropertyName = "notes")]
        public List<Note> Notes { get; set; }
    }
}
