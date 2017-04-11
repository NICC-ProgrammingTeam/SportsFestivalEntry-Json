using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Newtonsoft.Json;

namespace NYGSportsFestovalEntry
{
    public class EntryInput : IEntryHandler
    {
        private readonly string[] _common = {"中1", "中2", "中3", "高1", "高2", "高3" };
        private readonly string[] _junior = { "中1", "中2", "中3" };
        private readonly string[] _high = { "高1", "高2", "高3" };
        private readonly string[] _gender = {"共通", "男子", "女子"};
        private readonly string[] _grade = {"共通", "中学", "高校"};

        public void Handle(string[] args)
        {
            string definitionJson;
            using(var sr = new StreamReader("definition.json", Encoding.UTF8)){
                definitionJson = sr.ReadToEnd();
            }
            DefinitionJson define = JsonConvert.DeserializeObject<DefinitionJson>(definitionJson);
            ColorEntryJson json = new ColorEntryJson();
            json.全競技名簿 = new List<競技名簿>();
            string color;
            do
            {
                Console.WriteLine("色を入力してください 一覧:" + string.Join(",",define.色.ToArray()));
                color = Console.ReadLine();
            } while (!define.色.Contains(color));
            json.色 = color;
            for(int i = 1; i <= define.競技数; ++i)
            {
                var competition = define.競技[i-1];
                Console.WriteLine("競技 No." + competition.Id + " " + competition.Name +"(" + _gender[competition.Gender] + ")[" + _grade[competition.Grade] +"]のエントリー入力を開始します");
                string[] grades;
                switch (competition.Grade)
                {
                    case 0: grades = _common;break;
                    case 1: grades = _junior;break;
                    case 2: grades = _high;break;
                    default: Console.Error.WriteLine("ERROR: 競技情報定義Json \"definition.json\" 中のプロパティ \"" + competition.Name + "\"のgradeプロパティの値が不正です");return;
                }
                競技名簿 _競技名簿 = new 競技名簿();
                _競技名簿.ID = i;
                _競技名簿.競技名 = competition.Name;
                foreach(string s in grades)
                {
                    Console.WriteLine(s + "のエントリーを入力してください。人数は" + competition.Number + "人です。");
                    競技学年名簿 entry = new 競技学年名簿();
                    List<int> list = new List<int>();
                    for (int j = 1; j <= competition.Number; j++)
                    {
                        bool isCorrect = true;
                        do
                        {
                            try
                            {
                                Console.Write(j + ".");
                                var id = int.Parse(Console.ReadLine());
                                if (id < 999 || id > 9999)
                                {
                                    throw new Exception();
                                }
                                list.Add(id);
                                isCorrect = true;
                            }
                            catch (Exception e)
                            {
                                Console.Error.WriteLine("4桁の生徒番号(数字)を入力してください。");
                                isCorrect = false;
                            }
                        } while (!isCorrect);
                    }
                    switch (competition.Gender)
                    {
                        case 0: entry.共通 = list; break;
                        case 1: entry.男子 = list; break;
                        case 2: entry.女子 = list; break;
                    }
                    switch (s)
                    {
                        case "中1":
                            _競技名簿.中1 = entry; break;
                        case "中2":
                            _競技名簿.中2 = entry; break;
                        case "中3":
                            _競技名簿.中3 = entry; break;
                        case "高1":
                            _競技名簿.高1 = entry; break;
                        case "高2":
                            _競技名簿.高2 = entry; break;
                        case "高3":
                            _競技名簿.高3 = entry; break;
                    }
                }
                json.全競技名簿.Add(_競技名簿);
            }
            string output = JsonConvert.SerializeObject(json);
            using (var fstream = File.Create(color + ".json"))
            using (var sw = new StreamWriter(fstream, Encoding.UTF8))
            {
                sw.Write(output);
            }
            Console.WriteLine("エントリー入力が完了しました。お疲れ様です。");
            Console.WriteLine("出力された \"" + color +".json\"という名前のファイルを担当者に送信してください。");
        }
    }
}