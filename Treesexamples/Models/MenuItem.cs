using System;
using System.Collections.Generic;
using System.Text;

namespace TreesExamples.Models
{
    public sealed class MenuItem
    {
        public required string Title { get; init; }
        public Action? Action { get; init; }
        public List<MenuItem> Children { get; init; } = [];
    }
}
