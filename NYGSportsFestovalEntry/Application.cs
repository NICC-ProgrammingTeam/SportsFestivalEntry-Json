using System;

namespace NYGSportsFestovalEntry
{
    public class Application
    {
        public static void Main(string[] args)
        {
            if (args.Length <= 0)
            {
                Console.Error.WriteLine("引数が足りません");
                Console.WriteLine("入力モード: -input");
                Console.WriteLine("統合モード: -integration");
                Console.WriteLine("注意: 統合モードは競技名簿管理担当以外は実行しないでください");
                Console.ReadKey();
                return;
            }
            IEntryHandler handler;
            switch(args[0])
            {
                case "-input": handler = new EntryInput();break;
                case "-integration": handler = new EntryIntegration();break;
                default: throw new ArgumentException("第一引数が不正です。 -input もしくは -integrationを使ってください。");
            }
            handler.Handle(args);
            Console.WriteLine("終了するには何かキーを推してください");
            Console.ReadKey();
        }
    }

    public interface IEntryHandler
    {
        void Handle(string[] args);
    }
}