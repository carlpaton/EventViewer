using SharedModels;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using AutoMapper;
using System.IO;
using System;

namespace EvtxImporter
{
    public class ReadEvtxFile
    {
        private string _filePath = "";
        private List<EventLogModel> _returnValue = new List<EventLogModel>();
        private int _counter = 0;
        private int _progressCount = 1;
        public delegate void EventLogReaderDelegate(EventRecord record);

        public ReadEvtxFile(string filePath)
        {
            _filePath = filePath;
        }

        public List<EventLogModel> Go()
        {
            using (var reader = new EventLogReader(_filePath, PathType.FilePath))
            {
                ProcessReader(reader, Count); //reader has no count property that I could find -_-
                ProcessReader(reader, Append);
            }
            return _returnValue;
        }

        private void ProcessReader(EventLogReader reader, EventLogReaderDelegate _delegate)
        {
            EventRecord record;
            while ((record = reader.ReadEvent()) != null)
            {
                using (record)
                {
                    _delegate(record);
                }
            }
            reader.Seek(new SeekOrigin(), 0); //Taylor Swift - Begin Again :D
        }

        private void Count(EventRecord record)
        {
            _counter++;
        }

        private void Append(EventRecord record)
        {
            if (_progressCount == 1)
                Console.WriteLine("Total Lines = {0}\n", _counter);

            if (_progressCount % 100 == 0)
                Console.WriteLine("Progress Count = " + _progressCount);

            Mapper.Initialize(cfg => cfg.CreateMap<EventRecord, EventLogModel>());
            var model = Mapper.Map<EventLogModel>(record);

            model.Description = record.FormatDescription();
            model.Keywords = GetKeyWords(record.KeywordsDisplayNames);
            model.TaskCategory = record.Task ?? 0;

            _returnValue.Add(model);
            _progressCount++;
        }

        private string GetKeyWords(IEnumerable<string> keywordsDisplayNames)
        {
            var r = "";
            foreach (var item in keywordsDisplayNames)
            {
                r += item + " ";
            }
            return r;
        }
    }
}
