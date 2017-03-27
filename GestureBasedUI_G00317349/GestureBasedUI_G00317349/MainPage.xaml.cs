﻿using System;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace GestureBasedUI_G00317349
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        int count = 0; /////////TEST COUNTER


        //private MainPage rootPage = MainPage.Current;

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

            LayoutGrid.Children.Add(rect);
        }


        public void pauseVideo()
        {
            this.mediaPlayerElement.MediaPlayer.Pause();
        }

        /*
          protected override void OnNavigatedFrom(NavigationEventArgs e)
          {
              MediaPlayerHelper.CleanUpMediaPlayerSource(mediaPlayerElement.MediaPlayer);
          }*/


        private  async void pickFileButton_Click(object sender, RoutedEventArgs e)
        {

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
            */


            //mediaPlayerElement.IsTabStop();
            //mediaPlayerElement.Source = null;
            


            count++;

            StorageFolder picturesFolder = KnownFolders.VideosLibrary;

            StorageFolderQueryResult queryResult = picturesFolder.CreateFolderQuery(Windows.Storage.Search.CommonFolderQuery.GroupByMonth);

            IReadOnlyList<StorageFolder> folderList = await queryResult.GetFoldersAsync();

            StringBuilder outputText = new StringBuilder();

            foreach (StorageFolder folder in folderList)
            {
                IReadOnlyList<StorageFile> fileList = await folder.GetFilesAsync();

                // Print the month and number of files in this group.
               // outputText.AppendLine(folder.Name + " (" + fileList.Count + ")");

                foreach (StorageFile file in fileList)
                {
                    // Print the name of the file.
                   /// outputText.AppendLine("   " + file.Name);
                    if (file.Name.StartsWith("test" + count.ToString()))
                    {
                        this.mediaPlayerElement.MediaPlayer.Source = MediaSource.CreateFromStorageFile(file);
                        this.mediaPlayerElement.MediaPlayer.Play();
                    }
                }
            }
        }

        

        public async void cortanaPickVideo(string videoName)
        {
            createRectangle(Colors.Green);


            StorageFolder picturesFolder = KnownFolders.VideosLibrary;

            StorageFolderQueryResult queryResult = picturesFolder.CreateFolderQuery(Windows.Storage.Search.CommonFolderQuery.GroupByMonth);

            IReadOnlyList<StorageFolder> folderList = await queryResult.GetFoldersAsync();

            StringBuilder outputText = new StringBuilder();

            foreach (StorageFolder folder in folderList)
            {
                IReadOnlyList<StorageFile> fileList = await folder.GetFilesAsync();

                // Print the month and number of files in this group.
                // outputText.AppendLine(folder.Name + " (" + fileList.Count + ")");

                foreach (StorageFile file in fileList)
                {
                    // Print the name of the file.
                    /// outputText.AppendLine("   " + file.Name);
                    if (file.Name.StartsWith(videoName))
                    {
                        this.mediaPlayerElement.MediaPlayer.Source = MediaSource.CreateFromStorageFile(file);
                        this.mediaPlayerElement.MediaPlayer.Play();
                    }
                }
            }
        }

    }


       

}
