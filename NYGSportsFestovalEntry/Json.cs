using Newtonsoft.Json;
using System.Collections.Generic;

namespace NYGSportsFestovalEntry
{
    //性別区分 0:共通,1:男子,2:女子
    //中高区分 0:全,1:中,2:高
    

    [JsonObject]
    public class DefinitionJson
    {
        [JsonProperty]
        public List<string> 色 { get; set; }
        [JsonProperty]
        public string 色数 { get; set; }
        [JsonProperty]
        public int 競技数 { get; set; }
        [JsonProperty]
        public List<競技> 競技 { get; set; }
    }

    [JsonObject]
    public class ColorEntryJson
    {
        [JsonProperty]
        public string 色 { get; set; }
        [JsonProperty]
        public List<競技名簿> 全競技名簿 { get; set; }
    }

    [JsonObject]
    public class EntryJson
    {
        [JsonProperty]
        public List<ColorEntryJson> 色名簿 { get; set; }
    }

    [JsonObject]
    public class 競技
    {
        [JsonProperty]
        public string Name { get; set; }
        [JsonProperty]
        public int Id { get; set; }
        [JsonProperty]
        public int Gender { get; set; }
        [JsonProperty]
        public int Grade { get; set; }
        [JsonProperty]
        public int Number { get; set; }
    }

    [JsonObject]
    public class 競技名簿
    {
        [JsonProperty]
        public string 競技名 { get; set; }
        [JsonProperty]
        public int ID { get; set; }
        [JsonProperty]
        public 競技学年名簿 中1 { get; set; }
        [JsonProperty]
        public 競技学年名簿 中2 { get; set; }
        [JsonProperty]
        public 競技学年名簿 中3 { get; set; }
        [JsonProperty]
        public 競技学年名簿 高1 { get; set; }
        [JsonProperty]
        public 競技学年名簿 高2 { get; set; }
        [JsonProperty]
        public 競技学年名簿 高3 { get; set; }
    }

    [JsonObject]
    public class 競技学年名簿
    {
        [JsonProperty]
        public List<int> 共通 { get; set; }
        [JsonProperty]
        public List<int> 男子 { get; set; }
        [JsonProperty]
        public List<int> 女子 { get; set; }
    }
}