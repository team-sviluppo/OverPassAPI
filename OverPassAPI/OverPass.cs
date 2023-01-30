using System.ComponentModel;
using NetTopologySuite.IO;
using Newtonsoft.Json;
using OverPass.Utility;

namespace OverPass
{
    public class OverPass
    {
		protected string QueryStart = "[out:json][timeout:25];(";
		protected string QueryEnd = ");out geom;>;out body qt;";
        public string? OverPassUrl { get; set; } = "https://overpass-api.de/api/interpreter";
        public string BBox { get; set; } = "";
        public Dictionary<string, List<string>>? Query;
        public TagType Type { get; set; } = TagType.PLACES;
        public Dictionary<TagType, List<OTag>>? Tags;

        public OverPass(string bbox)
        {
            this.BBox = bbox;
        }

        public OverPass(string bbox, Dictionary<string, List<string>>? query) : this(bbox)
        {
            this.Query = query;
        }

        public OverPass(string bbox, Dictionary<string, List<string>>? query, string? overpassUrl) : this(bbox, query)
        {
            this.OverPassUrl = overpassUrl;
        }

        /**
         * Get List of tag query
         */
        public List<string>? TagKeys
        {
            get
            {
                if (this.Tags != null)
                {
                    List<string>? q = new();
                    foreach (OTag t in this.Tags[this.Type])
                    {
                        if (!q.Contains(t.KeyTag))
                            q.Add(t.KeyTag);
                    }
                    return q;
                }
                else
                    return null;
            }
        }

        public List<string>? TagValues
        {
            get
            {
                if (this.Tags != null)
                {
                    List<string>? q = new();
                    foreach (OTag t in this.Tags[this.Type])
                    {
                        if (!q.Contains(t.ValueTag))
                            q.Add(t.ValueTag);
                    }
                    return q;
                }
                else
                    return null;
            }
        }

        private string? Body
        {
            get
            {
                if (this.Tags != null)
                {
                    string? q = null;

                    foreach (OTag t in this.Tags[this.Type])
                    {
                        if (this.Query == null && t.ValueTag == "*")
                            q += t.Query;
                        else if (this.Query != null &&
                                 this.Query.ContainsKey(t.KeyTag) &&
                                 this.Query[t.KeyTag].Contains(t.ValueTag))
                            q += t.Query;
                    }
                    return q;
                }
                else
                    return null;
            }
        }

        private async Task<string?> RunOverPassQuery()
        {
            using HttpClient client = new();
            string? url = $"{this.OverPassUrl}";
            string? b = this.Body;

            if (b != null)
            {
                var body = new Dictionary<string, string>
                {
                    {"data", $"{this.QueryStart}{b}{this.QueryEnd}"}
                };

                using HttpResponseMessage response = await client.PostAsync(url, new FormUrlEncodedContent(body));
                return await response.Content.ReadAsStringAsync();
            }
            else
                return null;
        }

        protected Root? Response
        {
            get
            {
                try
                {
                    Task<string?> r = this.RunOverPassQuery();
                    if (r != null)
                    {
                        string? res = r.Result;
                        if (res != null)
                            return OverPassUtility.JSonDeserializeResponse(res);
                        else
                            return null;
                    }
                    else
                        return null;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return null;
                }
            }
        }
    }
}

