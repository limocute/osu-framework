// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu-framework/master/LICENCE

using System.Diagnostics;

namespace osu.Framework.Audio.Sample
{
    public abstract class SampleChannel : AdjustableAudioComponent, IHasCompletedState
    {
        protected bool WasStarted;

        public Sample Sample { get; protected set; }

        public SampleChannel(Sample sample)
        {
            Debug.Assert(sample != null, "Can not use a null sample.");
            Sample = sample;
        }

        /// <summary>
        /// Makes this sample fire-and-forget (will be cleaned up automatically).
        /// </summary>
        public bool OneShot;

        public virtual void Play(bool restart = true)
        {
            WasStarted = true;
        }

        public virtual void Stop()
        {
        }

        protected override void Dispose(bool disposing)
        {
            WasStarted = true;
            Stop();
            base.Dispose(disposing);
        }

        public abstract bool Playing { get; }

        public virtual bool Played => WasStarted && !Playing;

        public bool HasCompleted => Played && (OneShot || IsDisposed);
    }
}
