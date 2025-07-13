namespace Crolow.Cms.Core
{
    using System;

    public class Image
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Caption { get; set; }
    }

    public class ImagePreferenceSet
    {
        public int UserId { get; set; }
        public List<int> Preferences { get; set; } = new List<int>();
    }

    public class ImagePreferenceResult
    {
        public Image Image { get; set; }
        public int TotalScore { get; set; }
        public int NumberOfVotes { get; set; }
        public float FinalScore { get; set; }
    }

    public class ImageRankingEngineService
    {
        public static List<ImagePreferenceSet> _userPreferences => new List<ImagePreferenceSet>();
        public static List<Image> _images = new List<Image>();

        public ImageRankingEngineService(List<Image> images)
        {
            _images = images;
        }

        public List<Image> GetImages(int amountOfImages)
        {
            var imageIds = new List<int>();

            while (amountOfImages > 0)
            {
                var imageId = Random.Shared.Next(_images.Count);
                if (!imageIds.Contains(imageId))
                {
                    imageIds.Add(imageId);
                    amountOfImages--;
                }
            }

            return imageIds.Select(x => _images[x]).ToList();
        }

        public void Add(ImagePreferenceSet preferenceSet)
        {
            _userPreferences.Add(preferenceSet);
        }


        public List<ImagePreferenceResult> RankItemsByPreferences()
        {
            var scoreMap = new Dictionary<int, ImagePreferenceResult>();

            // Iterate through each user's preferences
            foreach (var set in _userPreferences)
            {
                for (int i = 0; i < set.Preferences.Count; i++)
                {
                    int itemId = set.Preferences[i];
                    int score = set.Preferences.Count - i;

                    ImagePreferenceResult result = null;

                    if (scoreMap.ContainsKey(itemId))
                    {
                        result = scoreMap[itemId];
                    }
                    else
                    {
                        result = new ImagePreferenceResult
                        {
                            Image = _images.FirstOrDefault(x => x.Id == itemId)
                        };

                        scoreMap.Add(itemId, result);
                    }

                    result.NumberOfVotes++;
                    result.TotalScore += score;
                    result.FinalScore = result.TotalScore / result.NumberOfVotes;

                }
            }

            return scoreMap
                .OrderByDescending(kvp => kvp.Value.FinalScore)
                .ThenBy(kvp => kvp.Key)
                .Select(x => x.Value).ToList();
        }
    }

}