using System;
using System.Speech.Synthesis;

namespace Coursework.Models.Classes.User.WordCollections.WordPair
{
    static class SpeechSynth
    {
        static public void Speech(string Text, int Volume = 100)
        {
            SpeechSynthesizer speechSynth = new SpeechSynthesizer();

            speechSynth.Volume = Volume;
            speechSynth.SelectVoiceByHints(VoiceGender.Male, VoiceAge.Adult);
            speechSynth.SpeakAsync(Text);
        }
    }
}
