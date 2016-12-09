using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilationAttribute(XamlCompilationOptions.Compile)]

namespace QuestionGame
{
    public class App : Application
    {
        public static Game CurrentGame { get; private set; }

        public App()
        {
            // The root page of your application

            string questionsText = string.Empty;

            var assembly = typeof(App).GetTypeInfo().Assembly;

            using (var stream = assembly.GetManifestResourceStream("QuestionGame.Data.questions.json"))
            {
                questionsText = new StreamReader(stream).ReadToEnd();
            }

            var questions = JsonConvert.DeserializeObject<List<QuizQuestion>>(questionsText);


            CurrentGame = new Game(questions);


            MainPage = new NavigationPage(new QuestionPage());
        }

        protected async override void OnStart()
        {

                string questionsText = string.Empty;

                var assembly = typeof(App).GetTypeInfo().Assembly;

                using (var stream = assembly.GetManifestResourceStream("QuestionGame.Data.questions.json"))
                {
                    questionsText = new StreamReader(stream).ReadToEnd();
                }

                var questions = JsonConvert.DeserializeObject<List<QuizQuestion>>(questionsText);


            CurrentGame = new Game(questions);
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}