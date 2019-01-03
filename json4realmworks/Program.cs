using dndsanitizer.Json;
using dndsanitizer.RealmsWork;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace dndsanitizer
{
    public class Program
    {
        private const int DefaultTimeoutInMilliseconds = 5000;
        static void Main(string[] args)
        {
            var tasks = new [] {/*ConvertMonsters(),*/ ConvertSpells()};
            Task.WaitAll(tasks, DefaultTimeoutInMilliseconds);
            Console.WriteLine("Done. Press a key to end this program.");
            Console.ReadKey();
        }

        private static Task ConvertMonsters()
        {
            Console.WriteLine(new StringBuilder()
                .AppendLine("Converting JSON file containing D&D 5e Monsters from ")
                .AppendLine("https://dl.dropboxusercontent.com/s/iwz112i0bxp2n4a/5e-SRD-Monsters.json")
                .AppendLine()
                .AppendLine()
                .ToString()
            );

            return Task.Run(async () =>
            {
                CancellationTokenSource cts = new CancellationTokenSource(DefaultTimeoutInMilliseconds);
                var data = await ReadFileAsync("5e-SRD-Monsters.json", cts.Token);
                var monsters = JsonHelpers.ParseMonsters(data);
                var creatures = monsters.Select(monster => new Creature(monster));
                var serialized = JsonHelpers.Serialize(creatures);
                await WriteFileAsync("monsters_cleansed.json", serialized, cts.Token);
            });
        }

        private static Task ConvertSpells()
        {
            Console.WriteLine(new StringBuilder()
                .AppendLine("Converting JSON file containing D&D 5e Spells from ")
                .AppendLine("https://github.com/vorpalhex/srd_spells")
                .AppendLine()
                .AppendLine()
                .ToString()
            );

            return Task.Run(async () =>
            {
                CancellationTokenSource cts = new CancellationTokenSource(DefaultTimeoutInMilliseconds);
                var data = await ReadFileAsync("spells.json", cts.Token);
                var spells = JsonHelpers.ParseSpells(data);
                var rwSpells = spells.Select(spell => new Spell(spell));
                var serialized = JsonHelpers.Serialize(rwSpells);
                await WriteFileAsync("spells_cleansed.json", serialized, cts.Token);
            });
        }

        private static async Task<string> ReadFileAsync(string filename, CancellationToken ctx)
        {
            return await File.ReadAllTextAsync(filename, ctx);
        }
        private static async Task WriteFileAsync(string filename, string content, CancellationToken ctx)
        {
             await File.WriteAllTextAsync(filename, content, ctx);
        }
    }
}
