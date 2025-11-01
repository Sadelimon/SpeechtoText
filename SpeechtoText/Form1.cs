
using System;
using System.Windows.Forms;


using System.Speech.Recognition;
using System.Globalization;     
using System.Diagnostics;       


namespace SpeechtoText 
{
    public partial class Form1 : Form
    {
       
        private SpeechRecognitionEngine speechEngine;

        public Form1()
        {
            InitializeComponent();
        }

      

        private void Form1_Load(object sender, EventArgs e)
        {
           
            BaslatDinleme();
        }
        
        public void BaslatDinleme()
        {
            try
            {
                
                CultureInfo cultureInfo = new CultureInfo("tr-TR");
                speechEngine = new SpeechRecognitionEngine(cultureInfo);

               
                speechEngine.SetInputToDefaultAudioDevice();

               
                speechEngine.LoadGrammar(new DictationGrammar());

                speechEngine.SpeechRecognized += SpeechEngine_SpeechRecognized;

                
                speechEngine.SpeechRecognitionRejected += SpeechEngine_SpeechRecognitionRejected;

                
                speechEngine.RecognizeAsync(RecognizeMode.Multiple);
            }
            catch (Exception ex)
            {
                
                MessageBox.Show("Konuşma tanıma başlatılamadı.\nHata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        
        private void SpeechEngine_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
           
            string algilananMetin = e.Result.Text;

            
            float guvenilirlik = e.Result.Confidence;

           
            Debug.WriteLine($"Algılanan Metin: {algilananMetin} (Güvenilirlik: {guvenilirlik:P0})");

            
        }

    
        private void SpeechEngine_SpeechRecognitionRejected(object sender, SpeechRecognitionRejectedEventArgs e)
        {
       
            Debug.WriteLine("Konuşma algılanamadı veya anlaşılamadı.");
        }
    }
}