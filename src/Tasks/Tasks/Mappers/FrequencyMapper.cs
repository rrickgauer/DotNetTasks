using Tasks.Domain.Enums;

namespace Tasks.Mappers
{
    public sealed class FrequencyMapper
    {
        /// <summary>
        /// Map the given Frequency enum to text
        /// </summary>
        /// <param name="frequency"></param>
        /// <returns></returns>
        public static string? ToText(Frequency? frequency)
        {   
            if (frequency == null)
            {
                return null;
            }

            string frequencyText = string.Empty;

            switch (frequency)
            {
                case Frequency.ONCE:
                    frequencyText = FrequencyStrings.ONCE; break;
                case Frequency.DAILY:
                    frequencyText = FrequencyStrings.DAILY; break;
                case Frequency.WEEKLY:
                    frequencyText = FrequencyStrings.WEEKLY; break;
                case Frequency.MONTHLY:
                    frequencyText = FrequencyStrings.MONTHLY; break;
                case Frequency.YEARLY:
                    frequencyText = FrequencyStrings.YEARLY; break;
                default:
                    throw new ArgumentOutOfRangeException(frequencyText, "The given frequency is not valid");
            }

            return frequencyText;
        }

        /// <summary>
        /// Map the given text to a Frequency enum
        /// </summary>
        /// <param name="frequencyText"></param>
        /// <returns></returns>
        public static Frequency? ToEnum(string? frequencyText)
        {
            if (frequencyText == null)
            {
                return null;
            }

            Frequency? frequency = null;

            switch (frequencyText.ToUpper())
            {
                case FrequencyStrings.ONCE:
                    frequency = Frequency.ONCE; break;
                case FrequencyStrings.DAILY:
                    frequency = Frequency.DAILY; break;
                case FrequencyStrings.WEEKLY:
                    frequency = Frequency.WEEKLY; break;
                case FrequencyStrings.MONTHLY:
                    frequency = Frequency.MONTHLY; break;
                case FrequencyStrings.YEARLY:
                    frequency = Frequency.YEARLY; break;
                default:
                    throw new ArgumentOutOfRangeException(frequencyText, "The given frequency text is not valid");
            }

            return frequency;
        }

    }
}
