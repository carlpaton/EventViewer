using Newtonsoft.Json;
using Repository.PostgreSQL;
using System;
using System.Collections.Generic;
using System.IO;

namespace EvtxImporter
{
    public class CheckFileLocation
    {
        private string _path = Config.Default.LogPath;
        private List<string> _waterMark = new List<string>();

        public CheckFileLocation()
        {
            _waterMark = Deserialize();
        }

        public void Go()
        {
            foreach (var file in Directory.GetFiles(_path, "*.evtx"))
            {
                if (!_waterMark.Contains(file))
                {
                    Console.WriteLine("\nProcessing {0}", file);
                    Serialize(file);

                    var model = new ReadEvtxFile(file).Go();
                    new EventLogRepository(Config.Default.NpgsqlConnection).InsertBulk(model);
                    Console.WriteLine("\nInsertBulk OK");
                }
                else
                    Console.WriteLine("Already imported {0}", file);
            }
        }

        #region helpers
        private void Serialize(string file)
        {
            _waterMark.Add(file);
            var json = JsonConvert.SerializeObject(_waterMark);
            File.WriteAllText(Config.Default.WaterMarkFile, json);

        }
        private List<string> Deserialize()
        {
            if (!File.Exists(Config.Default.WaterMarkFile))
                return new List<string>();

            var json = File.ReadAllText(Config.Default.WaterMarkFile);
            if (json == null)
                return new List<string>();
            else
                return JsonConvert.DeserializeObject<List<string>>(json);
        }
        #endregion
    }
}
