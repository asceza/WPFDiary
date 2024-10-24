using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using WPFDiary.Models;

namespace WPFDiary.Services
{
    public class FileIOService
    {
        private readonly string _filepath;

        public FileIOService(string filepath)
        {
            _filepath = filepath;
        }

        public BindingList<DiaryModel> LoadData()
        {
            var isFileExist = File.Exists(_filepath);
            if (!isFileExist)
            {
                File.CreateText(_filepath).Dispose(); // освобождаем ресурсы
                return new BindingList<DiaryModel>();
            }
            else
            {
                using (var reader = File.OpenText(_filepath))
                {
                    var fileText = reader.ReadToEnd();
                    return JsonConvert.DeserializeObject<BindingList<DiaryModel>>(fileText);
                }
            }
        }

        public void SaveData(BindingList<DiaryModel> diaryDataList)
        {
            using (StreamWriter writer = File.CreateText(_filepath))
            {
                string JSONOut = JsonConvert.SerializeObject(diaryDataList);
                writer.WriteLine(JSONOut);
            }

        }
    }
}
