using NetflixClone.Domain.Common;

namespace NetflixClone.Domain.ValueObjects
{
    public class VideoSource : ValueObject
    {
        public string Url { get; }
        public string Quality { get; }
        public string Format { get; }

        public VideoSource(string url, string quality, string format)
        {
            if (string.IsNullOrWhiteSpace(url))
                throw new ArgumentException("Video URL cannot be empty");
            
            if (string.IsNullOrWhiteSpace(quality))
                throw new ArgumentException("Video quality cannot be empty");

            Url = url;
            Quality = quality;
            Format = format;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Url;
            yield return Quality;
            yield return Format;
        }

        public override string ToString()
        {
            return $"{Quality} ({Format}) - {Url}";
        }
    }
}