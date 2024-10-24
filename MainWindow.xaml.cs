using System;
using System.ComponentModel;
using System.Windows;
using WPFDiary.Models;
using WPFDiary.Services;

namespace WPFDiary
{

    public partial class MainWindow : Window
    {
        private BindingList<DiaryModel> _diaryDataList;
        private FileIOService _fileIOService;
        private readonly string _fileName = $"{Environment.CurrentDirectory}\\notes.json";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _fileIOService = new FileIOService(_fileName);

            /*
            _diaryDataList = new BindingList<DiaryModel>()
            {
                new DiaryModel(){Note = "first"},
                new DiaryModel(){Note = "second", IsDone = true}
            };
            */

            try
            {
                _diaryDataList = _fileIOService.LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Close(); // this.Close();
            }

            dgDiary.ItemsSource = _diaryDataList;

            _diaryDataList.ListChanged += _diaryDataList_ListChanged;
        }

        // sender и _diaryDataList ссылаются на один объект
        private void _diaryDataList_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (e.ListChangedType == ListChangedType.ItemAdded ||
                e.ListChangedType == ListChangedType.ItemDeleted ||
                e.ListChangedType == ListChangedType.ItemChanged)
            {
                try
                {
                    _fileIOService.SaveData((BindingList<DiaryModel>)sender);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    Close();
                }
            }

        }
    }
}