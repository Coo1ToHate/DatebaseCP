using System.Collections.ObjectModel;
using System.Windows;
using DatebaseCP.Command;
using DatebaseCP.Models;
using DatebaseCP.Utils;
using DatebaseCP.View;
using DatebaseCP.ViewModel.Base;

namespace DatebaseCP.ViewModel
{
    internal class ListPostWindowViewModel : BaseViewModel
    {
        private ADO ado = new ADO();

        private string _title;
        private ObservableCollection<Post> _posts;
        private Post _selectedPost;

        public ListPostWindowViewModel()
        {
            _title = "Должности";
            _posts = ado.GetAllPosts();
        }

        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Post> Posts
        {
            get => _posts;
            set
            {
                _posts = value;
                OnPropertyChanged();
            }
        }

        public Post SelectedPost
        {
            get => _selectedPost;
            set
            {
                _selectedPost = value;
                OnPropertyChanged();
            }
        }

        #region Commands

        #region AddPostCommand

        private RelayCommand _addPostCommand;

        public RelayCommand AddPostCommand
        {
            get
            {
                return _addPostCommand ??= new RelayCommand(obj =>
                {
                    Post newPost = new Post();

                    ListPostEditWindow listPostEditWindow = new ListPostEditWindow
                    {
                        DataContext = new ListPostEditWindowViewModel(newPost),
                        Owner = obj as Window,
                        WindowStartupLocation = WindowStartupLocation.CenterOwner
                    };

                    listPostEditWindow.ShowDialog();

                    if (listPostEditWindow.DialogResult.Value)
                    {
                        ado.InsertPost(newPost);
                        Posts = ado.GetAllPosts();
                    }
                });
            }
        }

        #endregion

        #region EditPostCommand

        private RelayCommand _editPostCommand;

        public RelayCommand EditPostCommand
        {
            get
            {
                return _editPostCommand ??= new RelayCommand(obj =>
                {
                    Post editPost = SelectedPost;

                    ListPostEditWindow listPostEditWindow = new ListPostEditWindow
                    {
                        DataContext = new ListPostEditWindowViewModel(editPost),
                        Owner = obj as Window,
                        WindowStartupLocation = WindowStartupLocation.CenterOwner
                    };

                    listPostEditWindow.ShowDialog();

                    if (listPostEditWindow.DialogResult.Value)
                    {
                        ado.UpdatePost(editPost);
                        Posts = ado.GetAllPosts();
                    }
                },
                    obj => SelectedPost != null);
            }
        }

        #endregion

        #region DeletePostCommand

        private RelayCommand _deletePostCommand;

        public RelayCommand DeletePostCommand
        {
            get
            {
                return _deletePostCommand ??= new RelayCommand(obj =>
                {
                    ado.DeletePost(SelectedPost);
                    Posts = ado.GetAllPosts();
                },
                    obj => SelectedPost != null && ado.CountTeacherWithPost(SelectedPost.Id) == 0);
            }
        }

        #endregion

        #endregion
    }
}
