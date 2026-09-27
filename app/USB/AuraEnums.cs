using GHelper.Properties;

namespace GHelper.USB
{
    public enum AuraMode : int
    {
        AuraStatic = 0,
        AuraBreathe = 1,
        AuraColorCycle = 2,
        AuraRainbow = 3,
        Star = 4,
        Rain = 5,
        Highlight = 6,
        Laser = 7,
        Ripple = 8,
        AuraStrobe = 10,
        Comet = 11,
        Flash = 12,
        HEATMAP = 20,
        GPUMODE = 21,
        AMBIENT = 22,
        BATTERY = 23,
        GRADIENT = 24,
        ZONETEST = 25,
        AUDIO = 26,
        AUDIOPULSE = 27,
    }

    public enum AuraSpeed : int
    {
        Slow = 0,
        Normal = 1,
        Fast = 2,
    }

    /// <summary>
    /// Minimal aura helpers used by the peripheral settings windows. The
    /// laptop keyboard aura engine has been removed from this build; only the
    /// shared mode/speed definitions remain.
    /// </summary>
    public static class Aura
    {
        public static Dictionary<AuraSpeed, string> GetSpeeds()
        {
            return new Dictionary<AuraSpeed, string>
            {
                { AuraSpeed.Slow, Strings.AuraSlow },
                { AuraSpeed.Normal, Strings.AuraNormal },
                { AuraSpeed.Fast, Strings.AuraFast }
            };
        }
    }
}
