using ConfigurationLib;
using Microsoft.Speech.Recognition;
using StartMenu.View;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace StartMenu.Speech
{
    internal class RecognitionService
    {
        private CultureInfo _defaultCulture;

        private SpeechRecognitionEngine recognizer;

        public RecognitionService(CultureInfo defaultCulture) {
            _defaultCulture = defaultCulture;

            recognizer = new SpeechRecognitionEngine(_defaultCulture);

            InitializeSpeechRecognition();
        }

        public void ChangeCulture() { 
        }

        public void SetBehaviour(string[] choices, CultureInfo culture, Action<object?, SpeechRecognizedEventArgs> handler) {
            GrammarBuilder grammarBuilder = new GrammarBuilder();
            grammarBuilder.Culture = culture;
            grammarBuilder.Append(new Choices(choices));

            recognizer.LoadGrammar(new Grammar(grammarBuilder));
            recognizer.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(handler);
        }

        public void SetBehaviour(string[] choices, Action<object?, SpeechRecognizedEventArgs> handler) {
            SetBehaviour(choices, _defaultCulture, handler);
        }

        public void SetAudioDevice() {
            throw new NotImplementedException();
        }



        private void InitializeSpeechRecognition() {
            recognizer.UnloadAllGrammars();
            recognizer.SetInputToDefaultAudioDevice();

            recognizer.RecognizeAsync(RecognizeMode.Multiple);
        }


    }
}
