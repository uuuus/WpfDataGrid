using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfDataGrid.Models;
using WpfDataGrid.Mvvm;

namespace WpfDataGrid.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public MainViewModel()
        {
            MovieList = CreateList();
        }

        #region Commands
        #region LockApplicationCommand
        private ICommand dataGridMenuCommand;
        public ICommand DataGridMenuCommand
        {
            get
            {
                return dataGridMenuCommand ?? (dataGridMenuCommand = new RelayCommand<object>(ExecuteDataGridMenuCommand));
            }

        }
        private void ExecuteDataGridMenuCommand(object arg)
        {
            System.Collections.IList items = (System.Collections.IList)arg;
            var movies = items.Cast<Movie>();
            if (movies is not null && movies.Count() == 1)
            {
                var movie = movies.ToList()[0];
                Selected = $"{movie.Id}, {movie.Title}, {movie.Year}";
            }
        }
        #endregion LockApplicationCommand   
        #endregion Commands

        #region Mock
        private ObservableCollection<Movie> CreateList()
        {
            ObservableCollection<Movie> movies = new ObservableCollection<Movie>();
            for (int i = 0; i < 100; i++)
                movies.Add(new Movie { Id = i + 1, Title = $"Title {i + 1}", Year = $"{2000 + i}" });
            return movies;
        }
        #endregion Mock

        #region Properties
        private ObservableCollection<Movie> movieList;
        public ObservableCollection<Movie> MovieList
        {
            get { return movieList; }
            set
            {
                movieList = value;
                RaisePropertyChanged();
            }
        }
        private string selected;
        public string Selected { get { return selected; } 
            set
            { 
                if (value == selected)
                    return;
                selected = value;
                RaisePropertyChanged();
            }
        }
        #endregion Properties
    }
}
