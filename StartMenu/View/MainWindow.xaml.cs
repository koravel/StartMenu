using System.Globalization;
using Microsoft.Speech.Recognition;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StartMenu.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ControlPanel controlPanel = new ControlPanel();
        

        private bool controlPanelToggle = false;
        public MainWindow()
        {
            InitializeComponent();
            InitVoice();

        }

        private void OpenCommandPanel(object sender, RoutedEventArgs e)
        {
            if (!controlPanelToggle)
            {
                controlPanel.Show();
            }
            else
            {
                controlPanel.Hide();
            }
            controlPanelToggle = !controlPanelToggle;
        }

        private void InitVoice()
        {
           /* GrammarBuilder grammarBuilder = new GrammarBuilder();
            grammarBuilder.Culture = culture;

            grammarBuilder.Append(new Choices("show", "hide"));

            recognizer.LoadGrammar(new Grammar(grammarBuilder));

            recognizer.SpeechRecognized +=
              new EventHandler<SpeechRecognizedEventArgs>((sender, e) => {
                  Console.WriteLine(e.Result.Text);
                  if (e.Result.Text == "show")
                  {
                      controlPanel.Show();
                  }
                  else if (e.Result.Text == "hide")
                  {
                      controlPanel.Hide();
                  }
              });*/

            
        }
    }
}