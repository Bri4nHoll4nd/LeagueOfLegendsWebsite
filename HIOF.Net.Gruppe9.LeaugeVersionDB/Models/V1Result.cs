using System.Text.Json.Serialization;

namespace HIOF.Net.Gruppe9.LeaugeVersionDB.Models
{
    public class V1Result<T>
    {
        public V1Result(T value) 
        {
            Value = value;
        }

        public List<string> Errors { get; set; } = new List<string>();

        [JsonIgnore]
        public bool HasErrors => Errors.Any();

        public T Value { get; set; }
    }
}
