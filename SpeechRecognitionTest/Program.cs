using Microsoft.Speech.Recognition;
using System.Collections.ObjectModel;
using static SpeechRecognitionTest.RegistryDataKey;

namespace SpeechRecognitionTest {
    class MainClass {
        static void Main(string[] args)
        {
            
            var q = InstalledRecognizers();
            {
                System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("ru-RU");
                Microsoft.Speech.Recognition.SpeechRecognitionEngine sre = new Microsoft.Speech.Recognition.SpeechRecognitionEngine(ci);
                sre.SetInputToDefaultAudioDevice();

                sre.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(recognizer_SpeechRecognized);


                Choices numbers = new Choices();
                numbers.Add(new string[] { "one" }); //наши слова для 
                                                     //функций


                GrammarBuilder gb = new GrammarBuilder();
                gb.Culture = ci;
                gb.Append(numbers);


                Grammar g = new Grammar(gb);
                sre.LoadGrammar(g);

                sre.RecognizeAsync(RecognizeMode.Multiple);

                // Keep the console window open.  
                while (true)
                {
                    Console.ReadLine();
                }
            }
        }

        static void recognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            Console.WriteLine("Recognized text: " + e.Result.Text);
        }


        static ReadOnlyCollection<RecognizerInfo> InstalledRecognizers()
        {
            List<RecognizerInfo> list = new List<RecognizerInfo>();
            using (ObjectTokenCategory objectTokenCategory = ObjectTokenCategory.Create("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Speech Server\\v11.0\\Recognizers"))
            {
                if (objectTokenCategory != null)
                {
                    foreach (ObjectToken item in (IEnumerable<ObjectToken>)objectTokenCategory)
                    {
                        RecognizerInfo recognizerInfo = RecognizerInfo.Create(item);
                        if (recognizerInfo != null)
                        {
                            list.Add(recognizerInfo);
                        }
                    }
                }
            }

            return new ReadOnlyCollection<RecognizerInfo>(list);
        }
    }
}