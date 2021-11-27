using System.Text.Json.Serialization;

namespace WebClient
{
    public class Customer
    {
        [JsonPropertyName("id")]
        public long Id { get; init; }

        [JsonPropertyName("firstname")]
        public string Firstname { get; init; }

        [JsonPropertyName("lastname")]
        public string Lastname { get; init; }

        public override string ToString()
        {
            return $"Id: {Id}, Фамилия: {Lastname}, Имя: {Firstname}.";
        }
    }
}