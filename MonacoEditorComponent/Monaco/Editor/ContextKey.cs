﻿using Monaco.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monaco.Editor
{
    public sealed class ContextKey : IContextKey
    {
        [JsonIgnore]
        private CodeEditor _editor;

        [JsonProperty("key")]
        public string Key { get; private set; }
        [JsonProperty("defaultValue")]
        public bool DefaultValue { get; private set; }
        [JsonProperty("value")]
        public bool Value { get; private set; }

        public ContextKey(CodeEditor editor, string key, bool defaultValue)
        {
            _editor = editor;

            Key = key;
            DefaultValue = defaultValue;
        }

        private async void UpdateValueAsync()
        {
            await _editor.InvokeScriptAsync("updateContext", Key, JsonConvert.SerializeObject(Value));
        }

        public bool Get()
        {
            return Value;
        }

        public void Reset()
        {
            Value = DefaultValue;

            UpdateValueAsync();
        }

        public void Set(bool value)
        {
            Value = value;

            UpdateValueAsync();
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
