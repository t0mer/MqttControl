using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;

namespace MqttControl
{
    public class TTS
    {
        public void Speek(string Message)
        {
            SpeechSynthesizer synthesizer = new SpeechSynthesizer();
            synthesizer.Volume = 100;
            synthesizer.SpeakAsync(Message);
        }
    }
}
