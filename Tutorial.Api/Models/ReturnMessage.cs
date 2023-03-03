using MongoDB.Bson.Serialization.Attributes;

namespace Tutorial.Api.Models;

public class ReturnMessage{
   
    [BsonElement("code")]
    public string? Code { get; set; }

    [BsonElement("message")]
    public string? Message { get; set; }

}