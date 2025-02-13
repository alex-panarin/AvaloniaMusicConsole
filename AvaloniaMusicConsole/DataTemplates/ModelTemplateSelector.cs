using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Metadata;
using AvaloniaMusicConsole.Models;
using System;
using System.Collections.Generic;

namespace AvaloniaMusicConsole.DataTemplates
{
    public class ModelTemplateSelector 
        : IDataTemplate
    {
        [Content]
        public Dictionary<string, IDataTemplate> AvailableTemplates { get; } = [];
        
        public Control Build(object? param)
        {
            if (param is not null 
                && param is IDataModel model) 
            {
                if(AvailableTemplates.TryGetValue(model.Key, out var template) )
                    return template.Build(param)!;
            }
            
            throw new ArgumentNullException(nameof(param));
        }

        public bool Match(object? data)
        {
            return data is IDataModel model
                && AvailableTemplates.ContainsKey(model.Key);
        }
    }
}
