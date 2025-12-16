using System.Text.Json.Serialization;

namespace FoodNet.Entity.Enum;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum OrderStatus
{
    Pending,
    InPreparation,
    Ready,
    Completed,
    Canceled
}