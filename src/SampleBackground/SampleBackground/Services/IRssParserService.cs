using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Toolkit.Parsers.Rss;

namespace SampleBackground.Services
{
    public interface IRssParserService
    {
        Task<IEnumerable<RssSchema>> Parse(string url);
    }
}