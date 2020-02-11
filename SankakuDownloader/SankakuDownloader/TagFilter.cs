using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace SankakuDownloader
{
    public class TagFilter
    {
        [JsonProperty(PropertyName = "foldername")]
        public String FolderName { get; set; }

        [JsonProperty(PropertyName = "method")]
        public String Method { get; set; }

        [JsonProperty(PropertyName = "tags")]
        public List<String> Tags { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(FolderName);
            sb.Append(" -> ");
            for (int index = 0; index <= Tags.Count - 1; index++)
            {
                sb.Append(index == Tags.Count - 1 ? $" {Tags[index]}" : $" {Tags[index]} {Method.ToUpper()}");
            }
            return sb.ToString();
        }

        public String ToTagString()
        {
            StringBuilder sb = new StringBuilder();
            for (int index = 0; index <= Tags.Count - 1; index++)
            {
                sb.Append(index == Tags.Count - 1 ? $" {Tags[index]}" : $" {Tags[index]} {Method.ToUpper()}");
            }
            return sb.ToString();
        }
    }
}
