using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace SurviveTogether.Data
{
    [System.Serializable]
    public struct ConvertableJsonDateTime
    {
        public long value;

        public static implicit operator DateTime(ConvertableJsonDateTime jsonDateTime)
        {
            return DateTime.FromFileTimeUtc(jsonDateTime.value);
        }

        public static implicit operator ConvertableJsonDateTime(DateTime dateTime)
        {
            ConvertableJsonDateTime jsonDateTime = new ConvertableJsonDateTime();
            jsonDateTime.value = dateTime.ToFileTimeUtc();
            return jsonDateTime;
        }
    }
}