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

        
        //holds video/music files
        List<StorageFile> fileList = new List<StorageFile>();
       

        public MainPage()
        {
            this.InitializeComponent();
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
            //this.mediaPlayerElement.TransportControls.IsFullWindowEnabled = true;
            //this.mediaPlayerElement.IsFullWindow = true;
            //this.mediaPlayerElement.IsFullWindow
        }

        public void exitFullScreen()
        {

        }

        public void stopPlayer()
        {
            mediaPlayerElement.MediaPlayer.Source = null;
        }



        private void pickVideoButton_Click(object sender, RoutedEventArgs e)
        {
            mediaListBox.Items.Clear();
            polulateListBox("videos");

        }


        public async void playMedia(string videoName) 
        {

            mediaPlayerElement.Source = null;

            StorageFolder videoFolder = KnownFolders.VideosLibrary;

            StorageFolderQueryResult queryResult = videoFolder.CreateFolderQuery(Windows.Storage.Search.CommonFolderQuery.GroupByMonth);

            IReadOnlyList<StorageFolder> folderList = await queryResult.GetFoldersAsync();

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
            polulateListBox("music");
        }

        public async void polulateListBox(string mediaType)
        {

            StorageFolder chosenFolder = null;

            if (mediaType == "videos")
            {
                chosenFolder = KnownFolders.VideosLibrary;
            }
            else if (mediaType == "music")
            {
                chosenFolder = KnownFolders.MusicLibrary; 
            }

            StorageFolderQueryResult queryResult = chosenFolder.CreateFolderQuery(Windows.Storage.Search.CommonFolderQuery.GroupByAlbumArtist); 
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
                   mySpiltView.IsPaneOpen = false;
                }
            }

        }

    }

}
