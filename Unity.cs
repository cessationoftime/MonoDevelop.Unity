using System;

namespace UnityDevelop
{

    public class Unity
    {
        // 'Remote' Unity Reference Addresses
        public const string REMOTE_VIEW_MANUAL_URI = "http://unity3d.com/support/documentation/Manual/index.html";
        public const string REMOTE_VIEW_REFERENCE_URI = "http://unity3d.com/support/documentation/Components/index.html";
        public const string REMOTE_VIEW_SCRIPT_REFERENCE_URI = "http://unity3d.com/support/documentation/ScriptReference/index.html";
        public const string REMOTE_SEARCH_MANUAL_URI = "http://www.google.ca/search?hl=en&q=site%3Ahttp%3A%2F%2Funity3d.com%2Fsupport%2Fdocumentation%2FManual%2F+";
        public const string REMOTE_SEARCH_REFERENCE_URI = "http://www.google.com/search?hl=en&q=site%3Ahttp%3A%2F%2Funity3d.com%2Fsupport%2Fdocumentation%2FComponents%2F+";
        public const string REMOTE_SEARCH_SCRIPT_REFERENCE_URI = "http://unity3d.com/support/documentation/ScriptReference/30_search.html?q=";

        public enum Documentation
        {
            Manual,
            Reference,
            ScriptReference
        }
    }
}