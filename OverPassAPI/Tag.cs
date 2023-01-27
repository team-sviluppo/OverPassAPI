using System;

namespace OverPass
{
    public interface ITagInterface
    {
        string KeyTag { get; set; }
        string ValueTag { get; set; }
        string BBox { get; set; }
        string? Query { get; }
    }

    public class OTag : ITagInterface
    {
        public string KeyTag { get; set; } = "";
        public string ValueTag { get; set; } = "*";
        public string BBox { get; set; } = "";

        public OTag(string bbox, string key, string value)
        {
            this.BBox = bbox;
            this.KeyTag = key;
            this.ValueTag = value;
        }

        public string Tag
        {
            get
            {
                if (this.ValueTag == "*")
                    return $"{this.KeyTag}";
                else
                    return $"{this.KeyTag}={this.ValueTag}";
            }
        }

        public string Query
        {
            get
            {
                return $"node[{this.Tag}]({this.BBox});way[{this.Tag}]({this.BBox});relation[{this.Tag}]({this.BBox});";
            }
        }
    }
}

