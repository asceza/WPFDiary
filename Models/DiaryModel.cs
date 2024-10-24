using Newtonsoft.Json;
using System;
using System.ComponentModel;


namespace WPFDiary.Models
{
    public class DiaryModel : INotifyPropertyChanged
    {
        private bool _isDone;
        private string _note;

        //public DateTime CreationDate { get; set; }

        [JsonProperty(PropertyName = "creationDate")]
        public DateTime CreationDate { get; set; } = DateTime.Now;

        [JsonProperty(PropertyName = "isDone")]
        public bool IsDone
        {
            get { return _isDone; }
            set
            {
                if (_isDone == value)
                {
                    return;
                }
                else // если были изменения
                {
                    _isDone = value;
                    OnPropertyChanged("IsDone");
                }
                ;
            }
        }

        [JsonProperty(PropertyName = "note")]
        public string Note
        {
            get { return _note; }
            set
            {
                if (_note == value)
                {
                    return;
                }
                else // если были изменения
                {
                    _note = value;
                    OnPropertyChanged("Note");
                }
                ;
            }
        }

        // отслеживать изменение в Datagrid в case ItemChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = "")
        {
            // проверка на null
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
