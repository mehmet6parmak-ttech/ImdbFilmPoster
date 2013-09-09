using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace ImdbFilmPoster
{
    public class ImdbPoster : Control
    {
        private Image _image;
        private ProgressBar _progress;
        private bool _initialized;

        public ImdbPoster()
        {
            DefaultStyleKey = typeof(ImdbPoster);
            Loaded += ImdbPosterLoaded;
        }

        void ImdbPosterLoaded(object sender, RoutedEventArgs e)
        {
            OnApplyTemplate();
            RefreshPoster();
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _image = GetTemplateChild("_image") as Image;
            _progress = GetTemplateChild("_progress") as ProgressBar;

            if (_progress != null)
            {
                _progress.IsIndeterminate = true;
                _progress.Visibility = Visibility.Collapsed;
            }

            _initialized = true;
        }

        private string _imdbId;
        public string ImdbId
        {
            get { return _imdbId; }
            set
            {
                if (value != _imdbId)
                {
                    _imdbId = value;
                    if (_initialized)
                        RefreshPoster();
                }
            }
        }

        private async void RefreshPoster()
        {
            if (!string.IsNullOrEmpty(ImdbId))
            {
                _progress.Visibility = Visibility.Visible;
                var model = await ImdbApi.RetrieveFilmModel(ImdbId);
                if (model != null && model.ImdbPoster != null)
                    _image.Source = new BitmapImage(new Uri(model.ImdbPoster.ImdbPoster, UriKind.Absolute));

                _progress.Visibility = Visibility.Collapsed;
            }

        }
    }
}
