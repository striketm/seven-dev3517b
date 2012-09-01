using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework;

namespace pacpacman
{
    public class Sound
    {
        public SoundEffect soundE_pacdead, soundE_paceat, soundE_begin;
        public SoundEffectInstance soundI_pacdead, soundI_paceat, soundI_begin;
        public AudioEngine audioEngine;
        public WaveBank waveBank;
        public SoundBank soundBank;
        public Cue cue;
        public Sound(Game game)
        {
            soundE_paceat = game.Content.Load<SoundEffect>("paceat");
            soundE_pacdead = game.Content.Load<SoundEffect>("pacdead");
            soundE_begin = game.Content.Load<SoundEffect>("pacbegin");

        }
        public void dead()
        {
            soundI_pacdead = soundE_pacdead.Play();
        }
        public void eat()
        {
            soundI_paceat = soundE_paceat.Play();
        }
        public void begin()
        {
            soundI_begin = soundE_begin.Play();
        }

        public void ghostmove()
        {
            cue = soundBank.GetCue("pacghost");
            cue.Play();
        }
    }
}
