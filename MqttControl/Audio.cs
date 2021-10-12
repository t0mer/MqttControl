using AudioSwitcher.AudioApi.CoreAudio;
using System;

namespace MqttControl
{
    public class Audio
    {
        private CoreAudioDevice defaultPlaybackDevice = new CoreAudioController().DefaultPlaybackDevice;

        public string GetVolume()
        {
            string str;
            try
            {
                str = this.defaultPlaybackDevice.Volume + "%";
            }
            catch (Exception ex)
            {
                throw;
            }
            return str;
        }

        public bool isMuted()
        {
            bool isMuted;
            try
            {
                isMuted = this.defaultPlaybackDevice.IsMuted;
            }
            catch (Exception ex)
            {
                throw;
            }
            return isMuted;
        }

        public void Mute(bool Enable)
        {
            try
            {
                this.defaultPlaybackDevice.Mute(Enable);
            }
            catch (Exception ex)
            {
               // throw;
            }
        }

        public void Volume(int level)
        {
            try
            {
                this.defaultPlaybackDevice.Volume = Convert.ToDouble(level);
            }
            catch (Exception ex)
            {
                //throw;
            }
        }
    }
}
