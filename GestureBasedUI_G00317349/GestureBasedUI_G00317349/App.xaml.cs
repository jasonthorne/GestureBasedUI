//Code adapted from Microsoft docs: https://docs.microsoft.com/en-us/cortana/voicecommands/launch-a-background-app-with-voice-commands-in-cortana
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.VoiceCommands;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Search;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace GestureBasedUI_G00317349
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            Debug.WriteLine("Hello!");
            this.Suspending += OnSuspending;
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override async void OnLaunched(LaunchActivatedEventArgs e)
        {
#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached)
            {
                this.DebugSettings.EnableFrameRateCounter = true;
            }
#endif
            Frame rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();
                //App.NavigationService = new NavigationService(rootFrame);

                // Use the RootFrameNavigationHelper to respond to keyboard and mouse shortcuts.
                //this.rootFrameNavigationHelper = new RootFrameNavigationHelper(rootFrame);

                rootFrame.NavigationFailed += OnNavigationFailed;

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            if (rootFrame.Content == null)
            {
                // Determine if we're being activated normally, or with arguments from Cortana.
                if (string.IsNullOrEmpty(e.Arguments))
                {
                    // Launching normally.
                    rootFrame.Navigate(typeof(MainPage), "");
                }
                else
                {
                    // Launching with arguments. We assume, for now, that this is likely
                    // to be in the form of "destination=<location>" from activation via Cortana.
                    rootFrame.Navigate(typeof(MainPage), e.Arguments);
                }
            }
            // Ensure the current window is active
            Window.Current.Activate();

            //Install VCD commands
            var storageFile = await Windows.Storage.StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///VoiceCommandDefinitions.xml"));
            await Windows.ApplicationModel.VoiceCommands.VoiceCommandDefinitionManager.InstallCommandDefinitionsFromStorageFileAsync(storageFile);

            //Update phraselist to include users videos
            await updateVideoPhraseList();


        }

        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }


       //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        public async Task updateVideoPhraseList()  //SetPhraseListAsync
        {
            try
            {
                // Update the video phrase list, so that Cortana voice commands can use videos found in system.
               
                VoiceCommandDefinition commandDefinitions;

              
               if (VoiceCommandDefinitionManager.InstalledCommandDefinitions.TryGetValue("ProjectCommandSet_en-us", out commandDefinitions))
               {

                    StorageFolder videoFolder = KnownFolders.VideosLibrary;

                    StorageFolderQueryResult queryResult = videoFolder.CreateFolderQuery(Windows.Storage.Search.CommonFolderQuery.GroupByMonth);

                    IReadOnlyList<StorageFolder> folderList = await queryResult.GetFoldersAsync();

                    ////StringBuilder outputText = new StringBuilder();

                    List<string> videoNamesList = new List<string>();

                    foreach (StorageFolder folder in folderList)
                    {
                        IReadOnlyList<StorageFile> fileList = await folder.GetFilesAsync();

                        foreach (StorageFile file in fileList)
                        {
                            videoNamesList.Add(Path.GetFileNameWithoutExtension(file.Name));    
                        }
                    }

                    ////////////////////////////////////
                    foreach (string word in videoNamesList)
                    {
                        Debug.WriteLine(word);
                    }
                    ////////////////////////////////////

                    await commandDefinitions.SetPhraseListAsync("video", videoNamesList);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error updating Phrase list for VCDs: " + ex.ToString());
            }
        }
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++



        protected override void OnActivated(IActivatedEventArgs args) 
        //protected override async void OnActivated(IActivatedEventArgs args)
        {

            
            Debug.WriteLine("-------------- onActivated called -------");
            base.OnActivated(args);

            Frame rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();
                //App.NavigationService = new NavigationService(rootFrame);

                rootFrame.NavigationFailed += OnNavigationFailed;

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }
            // Ensure the current window is active
            if (rootFrame.Content == null)
            {
                // Launching normally.
                rootFrame.Navigate(typeof(MainPage), "");
            }
            Window.Current.Activate();

            MainPage page = rootFrame.Content as MainPage;

            /*
            var commandArgs = args as VoiceCommandActivatedEventArgs;
            Windows.Media.SpeechRecognition.SpeechRecognitionResult speechRecognitionResult = commandArgs.Result;

            // Get the name of the voice command and the text spoken. 
            // See VoiceCommands.xml for supported voice commands.
            string voiceCommandName = speechRecognitionResult.RulePath[0];
            string textSpoken = speechRecognitionResult.Text;
            */


            ////////+++++++++++++++++++++++++++++++++++++++++++++++++
            // Was the app activated by a voice command?
            if (args.Kind == ActivationKind.VoiceCommand)
            {
                Debug.WriteLine("-------------- Activated via a voice command -------");

                var commandArgs = args as VoiceCommandActivatedEventArgs;
                Windows.Media.SpeechRecognition.SpeechRecognitionResult speechRecognitionResult = commandArgs.Result;

                // Get the name of the voice command and the text spoken. 
                // See VoiceCommands.xml for supported voice commands.
                string voiceCommandName = speechRecognitionResult.RulePath[0];
                string textSpoken = speechRecognitionResult.Text;

                Debug.WriteLine("-------------- Voice command name -------" + voiceCommandName);


                switch (voiceCommandName)
                {
                    case "playVideo":
                        string spokenVideo = speechRecognitionResult.SemanticInterpretation.Properties["video"][0];
                        page.playVideo(spokenVideo);
                        break;
                    case "pausePlayer":
                        page.pausePlayer();
                        break;
                    case "resumePlayer":
                        page.resumePlayer();
                        break;
                    case "fastForwardPlayer":
                        page.fastForwardPlayer();
                        break;
                    case "rewindPlayer":
                        page.rewindPlayer();
                        break;
                    case "increaseVolume":
                        page.increaseVolume();
                        break;
                    case "reduceVolume":
                        page.reduceVolume();
                        break;
                    case "makeFullScreen":
                        page.makeFullScreen();
                        break;
                    case "exitFullScreen":
                        page.exitFullScreen();
                        break;
                    case "stopPlayer":
                        page.stopPlayer();
                        break;
                    default:
                        break;
                }


                ///////////////////////////////////////////////
                if (voiceCommandName == "addRectangle")
                {
                    page.createRectangle(Colors.Red);
                }
               //////////////////////////////////////////////
            }
            else if (args.Kind == ActivationKind.Protocol)
            {

                /*
                 var commandArgs = args as Windows.ApplicationModel.Activation.VoiceCommandActivatedEventArgs;  //VoiceCommandActivatedEventArgs;
                 Windows.Media.SpeechRecognition.SpeechRecognitionResult speechRecognitionResult = commandArgs.Result;
                 //var speechRecognitionResult = commandArgs.Result;


                 // Get the name of the voice command and the text spoken. 
                 // See VoiceCommands.xml for supported voice commands.
                 string voiceCommandName = speechRecognitionResult.RulePath[0];
                 string textSpoken = speechRecognitionResult.Text;
              */
                /*
                   page.createRectangle(Colors.CornflowerBlue);
                   //////////////////
                   if (voiceCommandName == "playVideo")
                   {


                       string spokenVideo = "";

                       try
                       {
                           spokenVideo = speechRecognitionResult.SemanticInterpretation.Properties["video"][0];
                       }
                       catch
                       {
                           //
                       }

                       page.cortanaPickVideo(spokenVideo);
                   }
                   ///////////////////
                   */





                //  page.cortanaPickVideo("apple");
                /*
                //////////////+++++++++++++++++++++++++
                var commandArgs = args as Windows.ApplicationModel.Activation.VoiceCommandActivatedEventArgs;

               var speechRecognitionResult = commandArgs.Result;
               string voiceCommandName = speechRecognitionResult.RulePath[0];
               string textSpoken = speechRecognitionResult.Text;

               string spokenVideo = speechRecognitionResult.SemanticInterpretation.Properties["video"][0];

                page.cortanaPickVideo(spokenVideo);
               */





                //++++++++++++++++++++++++


                //if (voiceCommandName == "addRectangleFromBackground")
                // {
                // page.CreateRectangle(Colors.Yellow);
                //}
            }

        }

    }
}