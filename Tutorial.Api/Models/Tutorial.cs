using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Tutorial.Api.Models;

public class Tutorial
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    //602aa1e04f3b51804eca6917

    [BsonElement("title")]
    public string? Title { get; set; }

    [BsonElement("description")]
    public string? Description { get; set; }

    [BsonElement("published")]
    public bool? Published { get; set; }

    [BsonElement("createdAt")]
    public DateTime CreatedAt { get; set; } //0001-01-01T00:00:00Z

    [BsonElement("updatedAt")]
    public DateTime UpdatedAt { get; set; } //0001-01-01T00:00:00Z

}
