using HunnyPomodoro.Maui.Custom;
using HunnyPomodoro.Maui.View;
using System.Diagnostics;
using System.Runtime.ExceptionServices;

namespace HunnyPomodoro.Maui
{
    public partial class App : Application
    {
        public App(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            AppDomain.CurrentDomain.FirstChanceException += CurrentDomain_FirstChanceException;
        
            SecureStorage.RemoveAll();
            MainPage = new AppShell();

            Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping(nameof(BorderlessEntry), (handler, view) =>
            {
                #if __ANDROID__
                    handler.PlatformView.SetBackgroundColor(Android.Graphics.Color.Transparent);
                #endif
            });
        }

        private void CurrentDomain_FirstChanceException(object? sender, FirstChanceExceptionEventArgs e)
        {
            Debug.WriteLine($"***** Handling Unhandled Exception *****: {e.Exception.Message}");
            // TODO
            // log Unhandled Exception
        }
    }
}
