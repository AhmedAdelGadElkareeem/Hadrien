﻿using System.Text.Json.Serialization;

namespace WytSky.Mobile.Maui.Hadrein.Models
{
    public class FacebookProfile
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public Picture Picture { get; set; }
        public string Locale { get; set; }
        public string Link { get; set; }
        public Cover Cover { get; set; }
        [JsonPropertyName("age_range")]
        public int AgeRange { get; set; }
        public Device[] Devices { get; set; }
        [JsonPropertyName("first_name")]
        public string FirstName { get; set; }
        [JsonPropertyName("last_name")]
        public string LastName { get; set; }
        [JsonPropertyName("mobile_phone")]
        public string phoneNumber { get; set; }
        public string Gender { get; set; }
        public bool IsVerified { get; set; }
        public string Id { get; set; }
    }

    public class Picture
    {
        public Data Data { get; set; }
    }

    public class Data
    {
        public bool IsSilhouette { get; set; }
        public string Url { get; set; }
    }

    public class Cover
    {
        public string Id { get; set; }
        public int OffsetY { get; set; }
        public string Source { get; set; }
    }

    public class AgeRange
    {
        public int Min { get; set; }
    }

    public class Device
    {
        public string Os { get; set; }
    }
}
