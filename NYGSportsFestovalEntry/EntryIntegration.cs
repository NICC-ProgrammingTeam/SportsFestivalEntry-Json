using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NYGSportsFestovalEntry
{
    class EntryIntegration : IEntryHandler
    {
        public void Handle(string[] args)
        {
            Console.WriteLine("定義の読み込み開始");
            string definitionJson;
            using (var sr = new StreamReader("definition.json", Encoding.UTF8))
            {
                definitionJson = sr.ReadToEnd();
            }
            DefinitionJson define = JsonConvert.DeserializeObject<DefinitionJson>(definitionJson);
            Console.WriteLine("定義の読み込み完了");
            EntryJson json = new EntryJson();
            json.色名簿 = new List<ColorEntryJson>();
            foreach (string color in define.色)
            {
                Console.WriteLine(color + "色の読み込み開始");
                ColorEntryJson entry;
                using (StreamReader sr = new StreamReader(color + ".json", Encoding.UTF8))
                {
                    entry = JsonConvert.DeserializeObject<ColorEntryJson>(sr.ReadToEnd());
                }
                json.色名簿.Add(entry);
                Console.WriteLine(color + "色の読み込み完了");
            }
            Console.WriteLine("Jsonの出力開始");
            using (var fstream = File.Create("entry.json"))
            using (var sw = new StreamWriter(fstream, Encoding.UTF8))
            {
                sw.Write(JsonConvert.SerializeObject(json));
            }
            Console.WriteLine("Jsonの出力完了");
        }
    }
}
