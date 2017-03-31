using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Media.Core;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Search;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace GestureBasedUI_G00317349
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        //private MainPage rootPage = MainPage.Current;

        //holds video/music files
        List<StorageFile> fileList = new List<StorageFile>();
       

        public MainPage()
        {
            this.InitializeComponent();
        }

        //for testing VCD file
        public void createRectangle(Color color)
        {
            Random random = new Random();
            var left = random.Next(0, 300);
            var top = random.Next(0, 300);

            var rect = new Windows.UI.Xaml.Shapes.Rectangle();
            rect.Height = 100;
            rect.Width = 100;
            rect.Margin = new Thickness(left, top, 0, 0);

            rect.Fill = new SolidColorBrush(color);

            layoutGrid.Children.Add(rect);
        }


        public void pausePlayer()
        {
            this.mediaPlayerElement.MediaPlayer.Pause();
        }

        public void resumePlayer()
        {
            this.mediaPlayerElement.MediaPlayer.Play();
        }

        public void fastForwardPlayer()
        {

        }

        public void rewindPlayer()
        {

        }

        public void increaseVolume()
        {

        }

        public void reduceVolume()
        {

        }

        public void makeFullScreen()
        {
            //this.mediaPlayerElement.MediaPlayer.f
        }

        public void exitFullScreen()
        {

        }

        public void stopPlayer()
        {

        }

        /*
          protected override void OnNavigatedFrom(NavigationEventArgs e)
          {
              MediaPlayerHelper.CleanUpMediaPlayerSource(mediaPlayerElement.MediaPlayer);
          }*/


        private void pickVideoButton_Click(object sender, RoutedEventArgs e)
        {
            mediaListBox.Items.Clear();
            addToMediaListBox("videos");

            /*
            // Clear previous returned file name, if it exists, between iterations of this scenario
            rootPage.NotifyUser("", NotifyType.StatusMessage);
            */

            /*
            //////
            StorageFolder VideoFolder = KnownFolders.VideosLibrary;

            StorageFolderQueryResult queryResult =
                VideoFolder.CreateFolderQuery(CommonFolderQuery.GroupByMonth);

            IReadOnlyList<StorageFolder> folderList =
                await queryResult.GetFoldersAsync();

            StringBuilder outputText = new StringBuilder();

            foreach (StorageFolder folder in folderList)
            {
                IReadOnlyList<StorageFile> fileList = await folder.GetFilesAsync();

                // Print the month and number of files in this group.
                outputText.AppendLine(folder.Name + " (" + fileList.Count + ")");

                foreach (StorageFile x in fileList)
                {
                    // Print the name of the file.
                    outputText.AppendLine("   " + x.Name);
                }
            }

            /////*/

            /*
            // Create and open the file picker
            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.ViewMode = PickerViewMode.Thumbnail;
            openPicker.SuggestedStartLocation = PickerLocationId.VideosLibrary;
            openPicker.FileTypeFilter.Add(".mp4");
            openPicker.FileTypeFilter.Add(".mkv");
            openPicker.FileTypeFilter.Add(".avi");
            openPicker.FileTypeFilter.Add(".mp3"); ///////////////////////////////////REMOVE LATER!!! 

            StorageFile file = await openPicker.PickSingleFileAsync();
            if (file != null)
            {
                //rootPage.NotifyUser("Picked video: " + file.Name, NotifyType.StatusMessage);
                this.mediaPlayerElement.MediaPlayer.Source = MediaSource.CreateFromStorageFile(file);
                this.mediaPlayerElement.MediaPlayer.Play();

            }
            else
            {
                //rootPage.NotifyUser("Operation cancelled.", NotifyType.ErrorMessage);
            }

            //mediaPlayerElement.IsTabStop();
            //mediaPlayerElement.Source = null;
            */

        }


        public async void playVideo(string videoName) /////////////?????????????
        {

            mediaPlayerElement.Source = null;

            StorageFolder videoFolder = KnownFolders.VideosLibrary;

            StorageFolderQueryResult queryResult = videoFolder.CreateFolderQuery(Windows.Storage.Search.CommonFolderQuery.GroupByMonth);

            IReadOnlyList<StorageFolder> folderList = await queryResult.GetFoldersAsync();

            ///StringBuilder outputText = new StringBuilder();

            foreach (StorageFolder folder in folderList)
            {
                IReadOnlyList<StorageFile> fileList = await folder.GetFilesAsync();

                foreach (StorageFile file in fileList)
                {
                    if (file.Name.StartsWith(videoName))
                    {
                        this.mediaPlayerElement.MediaPlayer.Source = MediaSource.CreateFromStorageFile(file);
                        this.mediaPlayerElement.MediaPlayer.Play();
                    }
                }
            }

        }

        private void pickMusicButton_Click(object sender, RoutedEventArgs e)
        {
            mediaListBox.Items.Clear();
            addToMediaListBox("music");

            /*
            mediaPlayerElement.Source = null;

            // Create and open the file picker
            FileOpenPicker sopenPicker = new FileOpenPicker();
            sopenPicker.ViewMode = PickerViewMode.Thumbnail;
            //sopenPicker.SuggestedStartLocation = PickerLocationId.MusicLibrary;
            sopenPicker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.MusicLibrary;
            sopenPicker.FileTypeFilter.Add(".mp3");

            StorageFile file = await sopenPicker.PickSingleFileAsync();
            if (file != null)
            {
              
                this.mediaPlayerElement.MediaPlayer.Source = MediaSource.CreateFromStorageFile(file);
                this.mediaPlayerElement.MediaPlayer.Play();
            }
            else
            {
                //rootPage.NotifyUser("Operation cancelled.", NotifyType.ErrorMessage);
            }

            //mediaPlayerElement.IsTabStop();
            //mediaPlayerElement.Source = null;
           */

            /*
             QueryOptions queryOption = new QueryOptions
             (CommonFileQuery.OrderByTitle, new string[] { ".mp3", ".mp4", ".wma" });

             queryOption.FolderDepth = FolderDepth.Deep;

             Queue<IStorageFolder> folders = new Queue<IStorageFolder>();

             var files = await KnownFolders.MusicLibrary.CreateFileQueryWithOptions
               (queryOption).GetFilesAsync();

             foreach (var file in files)
             {
                 Debug.WriteLine(file.Name);
                 mediaListBox.Items.Add(file.Name);
             }
             */
        }

        public async void addToMediaListBox(string mediaType)
        {

            StorageFolder chosenFolder = null;

            if (mediaType == "videos")
            {
                chosenFolder = KnownFolders.VideosLibrary;
            }
            else if (mediaType == "music")
            {
                chosenFolder = KnownFolders.MusicLibrary; /////////////////////NOT WORKING!!!!


                /*     
                 QueryOptions queryOption = new QueryOptions
                 (CommonFileQuery.OrderByTitle, new string[] { ".mp3", ".mp4", ".wma" });

                 queryOption.FolderDepth = FolderDepth.Deep;

                 Queue<IStorageFolder> folders = new Queue<IStorageFolder>();

                 var files = await KnownFolders.MusicLibrary.CreateFileQueryWithOptions
                   (queryOption).GetFilesAsync();

                 foreach (var file in files)
                 {
                     Debug.WriteLine(file.Name);
                     mediaListBox.Items.Add(file.Name);
                 }


                     chosenFolder = KnownFolders.MusicLibrary;
                     //musicLibrary = await StorageLibrary.GetLibraryAsync(KnownLibraryId.Music);*/

            }

            StorageFolderQueryResult queryResult = chosenFolder.CreateFolderQuery(Windows.Storage.Search.CommonFolderQuery.GroupByAlbumArtist); ////.GroupByMonth);
            IReadOnlyList<StorageFolder> tempFolderList = await queryResult.GetFoldersAsync();

            //clear fileList before populating
            fileList.Clear();

            mySpiltView.IsPaneOpen = true;

            foreach (StorageFolder folder in tempFolderList)
            {

                //create tempfileList for reading
                IReadOnlyList<StorageFile> tempFileList = await folder.GetFilesAsync();
               
                foreach (StorageFile file in tempFileList)
                {
                    //add to mediaListBox for displaying to user
                    mediaListBox.Items.Add(Path.GetFileNameWithoutExtension(file.Name));
                    //add to fileList for playing after selection
                    fileList.Add(file);
                }
            }

            

        }

        private void showMediaButton_Click(object sender, RoutedEventArgs e)
        {
            mySpiltView.IsPaneOpen = !mySpiltView.IsPaneOpen;
        }


        //list box event listener
        private void mediaListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            foreach (StorageFile file in fileList)
            {
                Debug.WriteLine(file.Name); 

                if ((string)mediaListBox.SelectedItem == Path.GetFileNameWithoutExtension(file.Name))
                {             
                   this.mediaPlayerElement.MediaPlayer.Source = MediaSource.CreateFromStorageFile(file);
                   this.mediaPlayerElement.MediaPlayer.Play();
                }
            }

        }

    }

}
